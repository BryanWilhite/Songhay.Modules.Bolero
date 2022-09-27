module Songhay.Modules.Bolero.Tests.Visuals.Bulma.CssClassTests

open Xunit

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass

let titleTestData : seq<obj[]> =
    seq {
        yield [| [ "title"; "is-1" ]; HasFontSize Size1 |]
        yield [| [ "title"; "is-3" ]; HasFontSize Size3 |]
        yield [| [ "title" ]; DefaultBulmaFontSize |]
    }

[<Theory>]
[<MemberData(nameof titleTestData)>]
let ``title test`` (expected: string list, size: BulmaFontSizeOrDefault) =
    let actual = title size
    Assert.Equal<string>((CssClasses expected).Value, (CssClasses actual).Value)
