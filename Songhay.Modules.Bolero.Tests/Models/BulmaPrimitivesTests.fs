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

let BulmaColumnsOptionTestData : seq<obj[]> =
    seq {
        yield [| "is-gapless"; BulmaColumnsOption.Gapless.CssClass |]
        yield [| "is-multiline"; BulmaColumnsOption.MultiLine.CssClass |]
        yield [| "is-8"; BulmaColumnsOption.Gap8.CssClass |]
        yield [| "is-variable"; BulmaColumnsOption.Variable.CssClass |]
        yield [| "is-1-fullhd"; BulmaColumnsOption.Gap1.CssBreakpointClass BulmaBreakpoint.FullHD |]
        yield [| "is-vcentered"; BulmaColumnsOption.VCentered.CssClass |]
        yield [| "is-centered"; BulmaColumnsOption.Centered.CssClass |]
    }

[<Theory>]
[<MemberData(nameof BulmaColumnsOptionTestData)>]
let ``BulmaColumnsOption test`` (expected: string, actual: string) =
    Assert.Equal(expected, actual)

let BulmaColumnSizeTestData : seq<obj[]> =
    seq {
        yield [| "is-three-quarters"; BulmaColumnSize.ThreeQuarters.CssClass |]
        yield [| "is-one-third"; BulmaColumnSize.OneThird.CssClass |]
        yield [| "is-four-fifths"; BulmaColumnSize.FourFifths.CssClass |]
        yield [| "is-9"; BulmaColumnSize.Size9over12.CssClass |]
        yield [| "is-offset-one-quarter"; BulmaColumnSize.OneQuarter.CssOffsetClass |]
        yield [| "is-offset-8"; BulmaColumnSize.Size8over12.CssOffsetClass |]
        yield [| "is-narrow"; BulmaColumnSize.Narrow.CssClass |]
        yield [| "is-narrow-widescreen"; BulmaColumnSize.Narrow.CssBreakpointClass BulmaBreakpoint.WideScreen |]
        yield [| "is-desktop"; BulmaBreakpoint.Desktop.CssClass |]
        yield [| "is-two-thirds-tablet"; BulmaColumnSize.TwoThirds.CssBreakpointClass BulmaBreakpoint.Tablet |]
        yield [| "is-one-quarter-fullhd"; BulmaColumnSize.OneQuarter.CssBreakpointClass BulmaBreakpoint.FullHD |]
    }

[<Theory>]
[<MemberData(nameof BulmaColumnSizeTestData)>]
let ``BulmaColumnSize test`` (expected: string, actual: string) =
    Assert.Equal(expected, actual)

let BulmaShadeBackgroundTestData : seq<obj[]> =
    seq {
        yield [| "has-background-grey-dark"; ShadeGreyDark |]
        yield [| "has-background-grey-darker"; ShadeGreyDarker |]
        yield [| "has-background-white-bis"; ShadeWhiteBis |]
    }

[<Theory>]
[<MemberData(nameof BulmaShadeBackgroundTestData)>]
let ``BulmaShade.BackgroundCssClass test`` (expected: string, input: BulmaShade) =
    let actual = input.BackgroundCssClass
    Assert.Equal(expected, actual)

let BulmaBackgroundColorTestData : seq<obj[]> =
    seq {
        yield [| "has-background-primary"; ColorPrimary; None |]
        yield [| "has-background-primary-dark"; ColorPrimary; Some true |]
        yield [| "has-background-primary-light"; ColorPrimary; Some false |]
        yield [| "has-background-dark"; ColorEmpty; Some true |]
    }

[<Theory>]
[<MemberData(nameof BulmaBackgroundColorTestData)>]
let ``BulmaColor.BackgroundCssClass test`` (expected: string, input: BulmaColor, isDark: bool option) =
    let actual =
        if isDark.IsNone then input.BackgroundCssClass
        else if isDark.Value then input.BackgroundCssClassDark
        else input.BackgroundCssClassLight
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
