module Songhay.Modules.Bolero.Tests.Models.CssPrimitivesTests

open Xunit

open Songhay.Modules.Bolero.Models

let CssClassesToAttributeValueTestData : seq<obj[]> =
    seq {
        yield [| "a b c"; CssClasses [ "a"; "b"; "c" ] |]
        yield [| "e f g"; (CssClasses [ "e"; "f" ]).Append "g" |]
        yield [| "e f g"; (CssClasses [ "e"; "f" ]).AppendList [ "g" ] |]
        yield [| "h i j"; "h" |> (CssClasses [ "i"; "j" ]).Prepend |]
        yield [| "h i j"; [ "h" ] |> (CssClasses [ "i"; "j" ]).PrependList |]
    }

[<Theory>]
[<MemberData(nameof CssClassesToAttributeValueTestData)>]
let ``CssClasses ToAttributeValue test`` (expected: string, classes: CssClasses) =
    let actual = classes.ToAttributeValue
    Assert.Equal(expected, actual)

let CssInheritanceTestData : seq<obj[]> =
    seq {
        yield [| "inherit"; Inherit |]
        yield [| "revert-layer"; RevertLayer |]
    }

[<Theory>]
[<MemberData(nameof CssInheritanceTestData)>]
let ``CssInheritance test`` (expected: string, inheritance: CssInheritance) =
    let actual = inheritance.Value
    Assert.Equal<string>(expected, actual)
