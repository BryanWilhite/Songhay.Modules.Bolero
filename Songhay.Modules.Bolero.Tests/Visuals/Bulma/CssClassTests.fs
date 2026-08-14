module Songhay.Modules.Bolero.Tests.Visuals.Bulma.CssClassTests

open Xunit

open Songhay.Modules.Models
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass

type CssClassTestsData =
    static member titleTestData : seq<obj[]> =
        seq {
            yield [| [ "title"; "is-1" ]; HasFontSize Size1 |]
            yield [| [ "title"; "is-3" ]; HasFontSize Size3 |]
            yield [| [ "title" ]; DefaultBulmaFontSize |]
        }

    static member flexAlignContentTestData : seq<obj[]> =
        seq {
            yield [| "is-align-content-space-between"; SpaceBetween |]
            yield [| ""; InheritBoxAlignment Inherit |]
            yield [| "is-align-content-start"; Top |]
            yield [| "is-align-content-baseline"; BaseLine |]
        }

    static member flexDirectionTestData : seq<obj[]> =
        seq {
            yield [| "is-flex-direction-row-reverse"; RowReverse |]
            yield [| "is-flex-direction-column"; Column |]
        }

    static member flexGrowTestData : seq<obj[]> =
        seq {
            yield [| "is-flex-grow-0"; L0 |]
            yield [| "is-flex-grow-0"; L6 |]
        }

    static member flexItemsAlignmentTestData : seq<obj[]> =
        seq {
            yield [| "is-align-items-self-start"; SelfStart |]
            yield [| ""; InheritBoxAlignment Initial |]
            yield [| "is-align-items-start"; Top |]
            yield [| "is-align-items-baseline"; BaseLine |]
        }

    static member flexSelfAlignmentTestData : seq<obj[]> =
        seq {
            yield [| "is-align-self-auto"; InheritBoxAlignment RevertLayer |]
            yield [| "is-align-self-auto"; Top |]
            yield [| "is-align-self-baseline"; BaseLine |]
            yield [| "is-align-self-stretch"; Stretch |]
        }

    static member flexShrinkTestData : seq<obj[]> =
        seq {
            yield [| "is-flex-shrink-0"; L0 |]
            yield [| "is-flex-shrink-0"; L6 |]
        }

    static member flexJustifyTestData : seq<obj[]> =
        seq {
            yield [| "is-justify-content-left"; Left |]
            yield [| "is-justify-content-left"; Top |]
            yield [| "is-justify-content-right"; Right |]
            yield [| "is-justify-content-space-evenly"; SpaceEvenly |]
        }

    static member flexWrapTestData : seq<obj[]> =
        seq {
            yield [| "is-flex-wrap-wrap-reverse"; WrapReverse |]
            yield [| "is-flex-wrap-nowrap"; NoWrap |]
        }

[<Theory>]
[<MemberData(nameof CssClassTestsData.titleTestData, MemberType = typeof<CssClassTestsData>)>]
let ``title test`` (expected: string list, size: BulmaFontSizeOrDefault) =
    let actual = title size
    Assert.Equal<string>((CssClasses expected).Value, (CssClasses actual).Value)

[<Theory>]
[<MemberData(nameof CssClassTestsData.flexAlignContentTestData, MemberType = typeof<CssClassTestsData>)>]
let ``flex align content test`` (expected: string, input: CssBoxAlignment) =
    let actual = elementFlexContentAlignment input
    Assert.Equal<string>(expected, actual)

[<Theory>]
[<MemberData(nameof CssClassTestsData.flexDirectionTestData, MemberType = typeof<CssClassTestsData>)>]
let ``flex direction test`` (expected: string, input: CssFlexDirection) =
    let actual = elementFlexDirection input
    Assert.Equal<string>(expected, actual)

[<Theory>]
[<MemberData(nameof CssClassTestsData.flexGrowTestData, MemberType = typeof<CssClassTestsData>)>]
let ``flex grow test`` (expected: string, input: BulmaValueSuffix) =
    let actual = elementFlexGrow input
    Assert.Equal<string>(expected, actual)

[<Theory>]
[<MemberData(nameof CssClassTestsData.flexItemsAlignmentTestData, MemberType = typeof<CssClassTestsData>)>]
let ``flex align items test`` (expected: string, input: CssBoxAlignment) =
    let actual = elementFlexItemsAlignment input
    Assert.Equal<string>(expected, actual)

[<Theory>]
[<MemberData(nameof CssClassTestsData.flexShrinkTestData, MemberType = typeof<CssClassTestsData>)>]
let ``flex shrink test`` (expected: string, input: BulmaValueSuffix) =
    let actual = elementFlexShrink input
    Assert.Equal<string>(expected, actual)

[<Theory>]
[<MemberData(nameof CssClassTestsData.flexSelfAlignmentTestData, MemberType = typeof<CssClassTestsData>)>]
let ``flex align self test`` (expected: string, input: CssBoxAlignment) =
    let actual = elementFlexSelfAlignment input
    Assert.Equal<string>(expected, actual)

[<Theory>]
[<MemberData(nameof CssClassTestsData.flexJustifyTestData, MemberType = typeof<CssClassTestsData>)>]
let ``flex justify test`` (expected: string, input: CssBoxAlignment) =
    let actual = elementFlexJustifyContent input
    Assert.Equal<string>(expected, actual)

[<Theory>]
[<MemberData(nameof CssClassTestsData.flexWrapTestData, MemberType = typeof<CssClassTestsData>)>]
let ``flex wrap test`` (expected: string, input: CssFlexWrap) =
    let actual = elementFlexWrap input
    Assert.Equal<string>(expected, actual)
