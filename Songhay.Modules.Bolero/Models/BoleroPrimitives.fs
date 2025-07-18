namespace Songhay.Modules.Bolero.Models

open System.Collections.Generic
open Microsoft.Extensions.Configuration

open Songhay.Modules.Bolero.BoleroUtility

type APiBase =
    | APiBase of string

    static member fromConfiguration (input: IConfiguration) (name :string) =
        match input.GetValue $"{RestApiMetadata}:{name}:ApiBase" with
        | null ->  APiBase System.String.Empty
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

    override this.ToString() = $"( {fst this.Value}, {snd this.Value} )"
