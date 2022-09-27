module Songhay.Modules.Bolero.Tests.Visuals.CssDeclarationTests

open Xunit
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.CssDeclaration

let fontVariantTestData : seq<obj[]> =
    seq {
        yield [| "font-variant: small-caps slashed-zero;"; [ SmallCaps; Numeric FontVariantNumericSlashedZero ] |]
        yield [| "font-variant: common-ligatures tabular-nums;"; [ Ligatures FontVariantLigaturesCommon; Numeric FontVariantTabular ] |]
        yield [| "font-variant: no-common-ligatures proportional-nums;"; [ Ligatures FontVariantLigaturesNoCommon; Numeric FontVariantProportional ] |]
    }

[<Theory>]
[<MemberData(nameof fontVariantTestData)>]
let ``fontVariant test`` (expected: string, input: CssFontVariant list) =
    let actual = fontVariant input
    Assert.Equal(expected, actual)
