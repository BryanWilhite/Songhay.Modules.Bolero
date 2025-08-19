namespace Songhay.Modules.Bolero.Models

open System
open System.Collections.Generic
open System.Linq
open System.Text.RegularExpressions
open Microsoft.Extensions.Configuration
open Microsoft.FSharp.Collections
open FsToolkit.ErrorHandling

open Songhay.Modules.Bolero.BoleroUtility

///<summary>
/// Defines the “base” conventional uniform identifier of an API.
///</summary>
/// <remarks>
/// The name of this type is inspired by the phrase “base URL”
/// used for the description of the `base` HTML element
/// (https://developer.mozilla.org/en-US/docs/Web/HTML/Reference/Elements/base).
/// </remarks>
type ApiBase =
    //<summary> the “base” conventional uniform identifier of an API </summary>
    | ApiBase of string

    //<summary> returns <see cref="ApiBase" /> from the conventional <see cref="IConfiguration" /> </summary>
    static member fromConfiguration (input: IConfiguration) (name :string) =
        let key = input.GetValue $"{RestApiMetadata}:{name}:ApiBase"
        match key with
        | null ->  Error <| exn $"The expected {nameof IConfiguration} value for key `{key}` is not here."
        | s -> Ok <| ApiBase s

    //<summary> returns the underlying <see cref="string" /> of the DU case </summary>
    member this.Value = let (ApiBase v) = this in v

    //<summary> Returns a string that represents the current object. </summary>
    override this.ToString() = this.Value

///<summary>
/// Defines the conventional “claims” of an API.
///</summary>
/// <remarks>
/// The use of the word “claims” in this context goes beyond “claims challenges”
/// for authentication and includes the routes of the API itself.
/// </remarks>
type ClaimsSet =
    //<summary> the conventional “claims” of an API </summary>
    | ClaimsSet of Dictionary<string, string>

    //<summary> returns <see cref="ClaimsSet" /> from the conventional <see cref="IConfiguration" /> </summary>
    static member fromConfiguration (input: IConfiguration) (name :string) =
        let key = $"{RestApiMetadata}:{name}:ClaimsSet"
        let claimSet = Dictionary<string, string>()

        try
            (input.GetSection key).Bind claimSet
        with | _ -> ()

        if claimSet.Count() = 0 then Error <| exn $"The expected {nameof IConfiguration} value for key `{key}` is not here."
        else Ok <| ClaimsSet claimSet

    //<summary> returns the underlying dictionary of the DU case </summary>
    member this.Value = let (ClaimsSet v) = this in v

    //<summary> Returns a string that represents the current object. </summary>
    override this.ToString() = this.Value.ToString()

///<summary>
/// Defines all the information needed to access an API.
///</summary>
type RestApiMetadata =
    //<summary> all the information needed to access an API </summary>
    | RestApiMetadata of ApiBase * ClaimsSet

    //<summary> returns <see cref="RestApiMetadata" /> from the conventional <see cref="IConfiguration" /> </summary>
    static member fromConfiguration (input: IConfiguration) (name :string)=
        let apiBase = name |> ApiBase.fromConfiguration input
        let claimSet = name |> ClaimsSet.fromConfiguration input

        if Result.isOk apiBase && Result.isOk claimSet then
            Ok <| RestApiMetadata (apiBase |> Result.valueOr raise, claimSet |> Result.valueOr raise)
        else
            Error <| exn ""

    //<summary> returns the underlying tuple of the DU case </summary>
    member this.Value = let (RestApiMetadata (apiBase, claimsSet)) = this in (apiBase, claimsSet)

    //<summary> returns the underlying <see cref="ApiBase.Value" /> of this type </summary>
    member this.GetApiBase() =
        let apiBase = (fst this.Value).Value
        apiBase

    //<summary> returns a claim from the <see cref="ClaimsSet" /> of this type with the specified dictionary key </summary>
    member this.GetClaim (key: string) =
        let claimSet = (snd this.Value).Value
        match claimSet.TryGetValue key with
        | false, _ -> None
        | true, d -> Some d

    //<summary> builds and returns a <see cref="Uri" /> with the specified <see cref="ClaimsSet" /> dictionary key </summary>
    member this.ToUriFromClaim (key: string, [<ParamArray>] args: string[]) =
        let regex = Regex("\{[^}]+\}")
        let builder = UriBuilder(this.GetApiBase())
        let prefix = this.GetClaim "endpoint-prefix"
        let routeTemplate = this.GetClaim key

        if prefix.IsNone || routeTemplate.IsNone then None
        else
            let routeData = routeTemplate.Value.Split '|'
            let mutable route = routeData |> Array.head
            let matches = regex.Matches route

            if matches.Count <> args.Length then None
            else
                ( matches |> Array.ofSeq, args ) ||> Array.iter2(fun m arg -> route <- route.Replace(m.Value, arg))

                builder.Path <- $"{prefix.Value.Trim '/'}/{route.Trim '/'}"

                let code = routeData |> Array.tryLast
                if code.IsSome then builder.Query <- $"code={code.Value}"

                Some builder.Uri

    //<summary> Returns a string that represents the current object. </summary>
    override this.ToString() = $"( {fst this.Value}, {snd this.Value} )"
