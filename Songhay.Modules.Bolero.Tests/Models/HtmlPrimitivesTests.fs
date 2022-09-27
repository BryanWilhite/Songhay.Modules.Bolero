module Songhay.Modules.Bolero.Tests.Models.HtmlPrimitivesTests

open Xunit

open Songhay.Modules.Bolero.Models

let HtmlPrimitivesTestData : seq<obj[]> =
    seq {
        yield [| "modulepreload"; RelModulePreload |]
    }

[<Theory>]
[<MemberData(nameof HtmlPrimitivesTestData)>]
let ``HtmlLinkedDocumentRelationship.Value test`` (expected: string, input: HtmlLinkedDocumentRelationship) =
    let actual = input.Value
    Assert.Equal(expected, actual)
