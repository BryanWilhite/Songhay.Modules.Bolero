module Songhay.Modules.Bolero.Tests.Models.BulmaPrimitivesTests

open System

open Xunit

open Songhay.Modules.Bolero.Models

let BulmaColorTestData : seq<obj[]> =
    seq {
        yield [| "black"; ColorBlack |]
    }

[<Theory>]
[<MemberData(nameof BulmaColorTestData)>]
let ``BulmaColor.ColorName test`` (expected: string, input: BulmaColor) =
    let actual = input.ColorName
    Assert.Equal(expected, actual)

let BulmaFontSizeOrDefaultTestData : seq<obj[]> =
    seq {
        yield [| String.Empty; DefaultBulmaFontSize |]
        yield [| "1"; HasFontSize Size1 |]
    }

[<Theory>]
[<MemberData(nameof BulmaFontSizeOrDefaultTestData)>]
let ``BulmaFontSizeOrDefault.Value test`` (expected: string, input: BulmaFontSizeOrDefault) =
    let actual = input.Value
    Assert.Equal(expected, actual)

let BulmaRatioDimensionTestData : seq<obj[]> =
    seq {
        yield [| "is-128x128"; Square Square128 |]
        yield [| "is-16by9"; SixteenByNine |]
    }

let BulmaShadeTestData : seq<obj[]> =
    seq {
        yield [| "black-bis"; ShadeBlackBis |]
    }

[<Theory>]
[<MemberData(nameof BulmaShadeTestData)>]
let ``BulmaShade.ShadeName test`` (expected: string, input: BulmaShade) =
    let actual = input.ShadeName
    Assert.Equal(expected, actual)

[<Theory>]
[<MemberData(nameof BulmaRatioDimensionTestData)>]
let ``BulmaRatioDimension.CssClass test`` (expected: string, input: BulmaRatioDimension) =
    let actual = input.CssClass
    Assert.Equal(expected, actual)

let BulmaValueSuffixTestData : seq<obj[]> =
    seq {
        yield [| "auto"; AutoSuffix |]
        yield [| "3"; L3 |]
    }

[<Theory>]
[<MemberData(nameof BulmaValueSuffixTestData)>]
let ``BulmaValueSuffix.Value test`` (expected: string, input: BulmaValueSuffix) =
    let actual = input.Value
    Assert.Equal(expected, actual)

let BulmaTileHorizontalSizeTestData : seq<obj[]> =
    seq {
        yield [| String.Empty; HSizeAuto |]
        yield [| "is-7"; HSize7 |]
    }

[<Theory>]
[<MemberData(nameof BulmaTileHorizontalSizeTestData)>]
let ``BulmaTileHorizontalSize.CssClass test`` (expected: string, input: BulmaHorizontalSize) =
    let actual = input.CssClass
    Assert.Equal(expected, actual)

let BulmaVisibilityTestData : seq<obj[]> =
    seq {
        yield [| "is-inline"; DisplayInline |]
        yield [| "is-inline-block"; DisplayInlineBlock |]
        yield [| "is-invisible"; NonDisplayInvisible |]
        yield [| "is-sr-only"; ScreenReaderOnly |]
    }

[<Theory>]
[<MemberData(nameof BulmaVisibilityTestData)>]
let ``BulmaVisibility.CssClass test`` (expected: string, input: BulmaVisibility) =
    let actual = input.CssClass
    Assert.Equal(expected, actual)
