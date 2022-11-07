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

let flexAlignContentTestData : seq<obj[]> =
    seq {
        yield [| "is-align-content-space-between"; SpaceBetween |]
        yield [| ""; InheritBoxAlignment Inherit |]
        yield [| "is-align-content-start"; Top |]
        yield [| "is-align-content-baseline"; BaseLine |]
    }

[<Theory>]
[<MemberData(nameof flexAlignContentTestData)>]
let ``flex align content test`` (expected: string, input: CssBoxAlignment) =
    let actual = elementFlexContentAlignment input
    Assert.Equal<string>(expected, actual)

let flexDirectionTestData : seq<obj[]> =
    seq {
        yield [| "is-flex-direction-row-reverse"; RowReverse |]
        yield [| "is-flex-direction-column"; Column |]
    }

[<Theory>]
[<MemberData(nameof flexDirectionTestData)>]
let ``flex direction test`` (expected: string, input: CssFlexDirection) =
    let actual = elementFlexDirection input
    Assert.Equal<string>(expected, actual)

let flexGrowTestData : seq<obj[]> =
    seq {
        yield [| "is-flex-grow-0"; L0 |]
        yield [| "is-flex-grow-0"; L6 |]
    }

[<Theory>]
[<MemberData(nameof flexGrowTestData)>]
let ``flex grow test`` (expected: string, input: BulmaValueSuffix) =
    let actual = elementFlexGrow input
    Assert.Equal<string>(expected, actual)

let flexItemsAlignmentTestData : seq<obj[]> =
    seq {
        yield [| "is-align-items-self-start"; SelfStart |]
        yield [| ""; InheritBoxAlignment Initial |]
        yield [| "is-align-items-start"; Top |]
        yield [| "is-align-items-baseline"; BaseLine |]
    }

[<Theory>]
[<MemberData(nameof flexItemsAlignmentTestData)>]
let ``flex align items test`` (expected: string, input: CssBoxAlignment) =
    let actual = elementFlexItemsAlignment input
    Assert.Equal<string>(expected, actual)

let flexShrinkTestData : seq<obj[]> =
    seq {
        yield [| "is-flex-shrink-0"; L0 |]
        yield [| "is-flex-shrink-0"; L6 |]
    }

[<Theory>]
[<MemberData(nameof flexShrinkTestData)>]
let ``flex shrink test`` (expected: string, input: BulmaValueSuffix) =
    let actual = elementFlexShrink input
    Assert.Equal<string>(expected, actual)

let flexSelfAlignmentTestData : seq<obj[]> =
    seq {
        yield [| "is-align-self-auto"; InheritBoxAlignment RevertLayer |]
        yield [| "is-align-self-auto"; Top |]
        yield [| "is-align-self-baseline"; BaseLine |]
        yield [| "is-align-self-stretch"; Stretch |]
    }

[<Theory>]
[<MemberData(nameof flexSelfAlignmentTestData)>]
let ``flex align self test`` (expected: string, input: CssBoxAlignment) =
    let actual = elementFlexSelfAlignment input
    Assert.Equal<string>(expected, actual)

let flexJustifyTestData : seq<obj[]> =
    seq {
        yield [| "is-justify-content-left"; Left |]
        yield [| "is-justify-content-left"; Top |]
        yield [| "is-justify-content-right"; Right |]
        yield [| "is-justify-content-space-evenly"; SpaceEvenly |]
    }

[<Theory>]
[<MemberData(nameof flexJustifyTestData)>]
let ``flex justify test`` (expected: string, input: CssBoxAlignment) =
    let actual = elementFlexJustifyContent input
    Assert.Equal<string>(expected, actual)

let flexWrapTestData : seq<obj[]> =
    seq {
        yield [| "is-flex-wrap-wrap-reverse"; WrapReverse |]
        yield [| "is-flex-wrap-nowrap"; NoWrap |]
    }

[<Theory>]
[<MemberData(nameof flexWrapTestData)>]
let ``flex wrap test`` (expected: string, input: CssFlexWrap) =
    let actual = elementFlexWrap input
    Assert.Equal<string>(expected, actual)
