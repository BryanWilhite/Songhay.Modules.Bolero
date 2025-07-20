namespace Songhay.Modules.Bolero.Models

open System
open System.Collections.Generic
open System.Text.RegularExpressions
open Microsoft.Extensions.Configuration
open Microsoft.FSharp.Collections

open Songhay.Modules.Bolero.BoleroUtility

type APiBase =
    | APiBase of string

    static member fromConfiguration (input: IConfiguration) (name :string) =
        match input.GetValue $"{RestApiMetadata}:{name}:ApiBase" with
        | null ->  APiBase String.Empty
        | s -> APiBase s

    member this.Value = let (APiBase v) = this in v

    override this.ToString() = this.Value

type ClaimsSet =
    | ClaimsSet of Dictionary<string, string>

    static member fromConfiguration (input: IConfiguration) (name :string) =
        let claimSet = Dictionary<string, string>()

        try
            (input.GetSection $"{RestApiMetadata}:{name}:ClaimsSet").Bind claimSet
        with | _ -> ()

        ClaimsSet claimSet

    member this.Value = let (ClaimsSet v) = this in v

    override this.ToString() = this.Value.ToString()

type RestApiMetadata =
    | RestApiMetadata of APiBase * ClaimsSet

    static member fromConfiguration (input: IConfiguration) (name :string)=
        let apiBase = name |> APiBase.fromConfiguration input
        let claimSet = name |> ClaimsSet.fromConfiguration input

        RestApiMetadata (apiBase, claimSet)

    member this.Value = let (RestApiMetadata (apiBase, claimsSet)) = this in (apiBase, claimsSet)

    member this.GetApiBase =
        let apiBase = (fst this.Value).Value
        apiBase

    member this.GetClaim (key: string) =
        let claimSet = (snd this.Value).Value
        match claimSet.TryGetValue key with
        | false, _ -> None
        | true, d -> Some d

    member this.ToUriFromClaim (key: string, [<ParamArray>] args: string[]) =
        let regex = Regex("\{[^}]+\}")
        let builder = UriBuilder(this.GetApiBase)
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

    override this.ToString() = $"( {fst this.Value}, {snd this.Value} )"
