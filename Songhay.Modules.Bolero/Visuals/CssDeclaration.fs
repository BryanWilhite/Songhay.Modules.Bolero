namespace Songhay.Modules.Bolero.Visuals

open System
open Songhay.Modules.Bolero.Models

///<summary>
/// Selected functions for CSS declaration.
///</summary>
/// <remarks>
/// “When a property is paired with a value, this pairing is called a CSS declaration.
/// CSS declarations are found within CSS Declaration Blocks.”
/// 📖 https://developer.mozilla.org/en-US/docs/Learn/CSS/First_steps/How_CSS_is_structured#properties_and_values
/// </remarks>
module CssDeclaration =
    ///<summary>
    /// A function for CSS declaration.
    ///</summary>
    /// <remarks>
    /// “The <c>font-variant-alternates</c> CSS property controls the usage of alternate glyphs.
    /// These alternate glyphs may be referenced by alternative names defined in <c>@font-feature-values</c>.”
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant-alternates
    /// </remarks>
    let fontVariantAlternates (variant: CssFontVariantAlternates) = $"font-variant-alternates: {variant.Value};"

    ///<summary>
    /// A function for CSS declaration.
    ///</summary>
    /// <remarks>
    /// “The <c>font-variant-caps</c> CSS property controls the use of alternate glyphs for capital letters.”
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant-caps
    /// </remarks>
    let fontVariantCaps (variant: CssFontVariantCaps) = $"font-variant-caps: {variant.Value};"

    ///<summary>
    /// A function for CSS declaration.
    ///</summary>
    /// <remarks>
    /// “The <c>font-variant-ligatures</c> CSS property controls which ligatures and contextual forms are used
    /// in textual content of the elements it applies to.
    /// This leads to more harmonized forms in the resulting text.”
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant-ligatures
    /// </remarks>
    let fontVariantLigatures (variant: CssFontVariantLigatures) = $"font-variant-ligatures: {variant.Value};"

    ///<summary>
    /// A function for CSS declaration.
    ///</summary>
    /// <remarks>
    /// “The <c>font-variant-numeric</c> CSS property controls the usage of alternate glyphs
    /// for numbers, fractions, and ordinal markers.”
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant-numeric
    /// </remarks>
    let fontVariantNumeric (variant: CssFontVariantNumeric) = $"font-variant-numeric: {variant.Value};"

    ///<summary>
    /// A function for CSS declaration.
    ///</summary>
    /// <remarks>
    /// “The <c>font-variant</c> CSS shorthand property allows you to set all the font variants for a font.”
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// </remarks>
    let fontVariant (variants: CssFontVariant list) =
        match variants with
        | [ v ] -> $"font-variant: {v.Value};"
        | _ ->
            let s = (String.Empty, variants)
                    ||> List.fold (fun a i -> if String.IsNullOrWhiteSpace a then i.Value else $"{a} {i.Value}")
            $"font-variant: {s};"
