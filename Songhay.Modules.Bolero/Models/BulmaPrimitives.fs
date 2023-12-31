namespace Songhay.Modules.Bolero.Models

open System

open Songhay.Modules.Models
open Songhay.Modules.StringUtility

///<summary>
/// Defines Bulma alignment.
///</summary>
/// <remarks>
/// This naming of alignment is used whole or in part in the following:
/// 
/// 📖 https://bulma.io/documentation/columns/options/#vertical-alignment
/// 📖 https://bulma.io/documentation/components/breadcrumb/#alignment
/// 📖 https://bulma.io/documentation/components/card/
/// 📖 https://bulma.io/documentation/components/dropdown/#right-aligned
/// 📖 https://bulma.io/documentation/components/tabs/#alignment
/// 📖 https://bulma.io/documentation/form/general/#with-icons
/// 📖 https://bulma.io/documentation/form/file/#alignment
/// 📖 https://bulma.io/documentation/helpers/typography-helpers/#alignment
/// </remarks>
type BulmaAlignment =
    /// <summary> Bulma alignment </summary>
    | AlignCentered
    /// <summary> Bulma alignment </summary>
    | AlignJustified
    /// <summary> Bulma alignment </summary>
    | AlignLeft
    /// <summary> Bulma alignment </summary>
    | AlignRight
    /// <summary> Bulma alignment </summary>
    | AlignVerticalCenter

    ///<summary>Returns the CSS Bulma alignment name.</summary>
    member this.AlignmentName =
        match this with
        | AlignVerticalCenter -> "vcentered"
        | _ -> this.ToString().Replace("Align", String.Empty).ToLowerInvariant()

    ///<summary>Returns the CSS class name of the Bulma alignment.</summary>
    member this.CssClass = $"is-{this.AlignmentName}"

///<summary>
/// Defines Bulma breadcrumb separators.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/components/breadcrumb/#alternative-separators
/// </remarks>
type BulmaBreadcrumbSeparators =
    /// <summary> a Bulma breadcrumb separator </summary>
    | BreadcrumbSeparatorDefault
    /// <summary> a Bulma breadcrumb separator </summary>
    | BreadcrumbSeparatorArrow
    /// <summary> a Bulma breadcrumb separator </summary>
    | BreadcrumbSeparatorBullet
    /// <summary> a Bulma breadcrumb separator </summary>
    | BreadcrumbSeparatorDot
    /// <summary> a Bulma breadcrumb separator </summary>
    | BreadcrumbSeparatorSucceeds

    ///<summary>Returns the CSS class name of the Bulma breadcrumb separator.</summary>
    member this.CssClass =
        this.ToString().Replace("BreadcrumbSeparator", String.Empty).ToLowerInvariant() |> fun s -> $"has-{s}-separator"

///<summary>
/// Defines Bulma breadcrumb sizes.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/components/breadcrumb/#sizes
/// </remarks>
type BulmaBreadcrumbSize =
    /// <summary> a Bulma breadcrumb size </summary>
    | BreadcrumbSmall
    /// <summary> a Bulma breadcrumb size </summary>
    | BreadcrumbMedium
    /// <summary> a Bulma breadcrumb size </summary>
    | BreadcrumbLarge

    ///<summary>Returns the CSS class name of the Bulma breadcrumb size.</summary>
    member this.CssClass = this.ToString().Replace("Breadcrumb", String.Empty).ToLowerInvariant() |> fun s -> $"is={s}"

///<summary>
/// Defines Bulma responsive breakpoints for Bulma visibility helpers.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/helpers/visibility-helpers/
/// </remarks>
type BulmaBreakpoint =
    ///<summary>up to 768px</summary>
    | Mobile
    ///<summary>between 769px and 1023px</summary>
    | Tablet
    ///<summary>up to 1023px</summary>
    | Touch
    ///<summary>between 1024px and 1215px</summary>
    | Desktop
    ///<summary>between 1216px and 1407px</summary>
    | WideScreen
    ///<summary>1408px and above</summary>
    | FullHD

    ///<summary>Returns the <see cref="string" /> representation of the breakpoint name.</summary>
    member this.Value = this.ToString().ToLowerInvariant()

/// <summary>
/// Defines Bulma color classifications.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/helpers/color-helpers/
/// </remarks>
type BulmaColor =
    /// <summary> a Bulma color classification </summary>
    | ColorEmpty
    // <summary> a color modifier </summary>
    | ColorGhost
    /// <summary> a Bulma color classification </summary>
    | ColorGrey
    /// <summary> a Bulma color classification </summary>
    | ColorWhite
    /// <summary> a Bulma color classification </summary>
    | ColorBlack
    /// <summary> a Bulma color classification </summary>
    | ColorPrimary
    /// <summary> a Bulma color classification </summary>
    | ColorLink
    /// <summary> a Bulma color classification </summary>
    | ColorInfo
    /// <summary> a Bulma color classification </summary>
    | ColorSuccess
    /// <summary> a Bulma color classification </summary>
    | ColorWarning
    /// <summary> a Bulma color classification </summary>
    | ColorDanger

    member private this.backgroundCssClass (isDark: bool option) =
        let hasBackground = "has-background"
        let state =
            if isDark.IsNone then String.Empty
            else if isDark.Value then "-dark" else "-light"
        if String.IsNullOrWhiteSpace(this.ColorName) then
            if isDark.IsNone then String.Empty else $"{hasBackground}{state}"
        else $"{hasBackground}-{this.ColorName}{state}"

    ///<summary>Returns the CSS class color name of the Bulma color classification.</summary>
    member this.ColorName =
        match this with
        | ColorEmpty -> String.Empty
        | _ -> this.ToString().Replace("Color", String.Empty).ToLowerInvariant()

    ///<summary>Returns the Bulma background-color CSS class name of the Bulma color classification.</summary>
    member this.BackgroundCssClass = this.backgroundCssClass None

    ///<summary>Returns the Bulma background-color CSS class name of the Bulma color classification.</summary>
    member this.BackgroundCssClassDark = this.backgroundCssClass <| Some true

    ///<summary>Returns the Bulma background-color CSS class name of the Bulma color classification.</summary>
    member this.BackgroundCssClassLight =  this.backgroundCssClass <| Some false

    ///<summary>Returns the CSS class name of the Bulma color classification.</summary>
    member this.CssClass =
        if String.IsNullOrWhiteSpace(this.ColorName) then String.Empty else $"is-{this.ColorName}"

    ///<summary>Returns the CSS class name of the Bulma color classification.</summary>
    member this.CssClassDark =
        match this with
        | ColorEmpty -> String.Empty
        | ColorBlack | ColorWhite -> this.CssClass
        | _ -> $"is-{this.ColorName}-dark"

    ///<summary>Returns the CSS class name of the Bulma color classification.</summary>
    member this.CssClassLight =
        match this with
        | ColorEmpty -> String.Empty
        | ColorBlack | ColorWhite -> this.CssClass
        | _ -> $"is-{this.ColorName}-light"

    ///<summary>Returns the Bulma text-color CSS class name of the Bulma color classification.</summary>
    member this.TextCssClass =
        if String.IsNullOrWhiteSpace(this.ColorName) then String.Empty else $"has-text-{this.ColorName}"

///<summary>
/// Defines Bulma widths for the Bulma <c>column</c>.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/columns/sizes/
/// </remarks>
type BulmaColumnSize =
    /// <summary> a Bulma <c>column</c> width (<c>0.20</c>) </summary>
    | OneFifth
    /// <summary> a Bulma <c>column</c> width (<c>0.25</c>) </summary>
    | OneQuarter
    /// <summary> a Bulma <c>column</c> width (<c>~0.33</c>) </summary>
    | OneThird
    /// <summary> a Bulma <c>column</c> width  (<c>0.40</c>)</summary>
    | TwoFifths
    /// <summary> a Bulma <c>column</c> width (<c>0.50</c>) </summary>
    | Half
    /// <summary> a Bulma <c>column</c> width  (<c>~0.66</c>)</summary>
    | TwoThirds
    /// <summary> a Bulma <c>column</c> width (<c>0.60</c>) </summary>
    | ThreeFifths
    /// <summary> a Bulma <c>column</c> width (<c>0.75</c>) </summary>
    | ThreeQuarters
    /// <summary> a Bulma <c>column</c> width (<c>0.80</c>) </summary>
    | FourFifths
    /// <summary> a Bulma <c>column</c> width (<c>1.00</c>) </summary>
    | Full
    /// <summary> a Bulma <c>column</c> width for when “you want a column to only take the space it needs”</summary>
    /// <remarks> 📖 https://bulma.io/documentation/columns/sizes/#narrow-column </remarks>
    | Narrow
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size1over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size2over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size3over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size4over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size5over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size6over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size7over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size8over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size9over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size10over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size11over12
    /// <summary> a Bulma <c>column</c> width for 12 columns system</summary>
    | Size12over12

    member private this.getCssClass prefix =
        let over12 = "over12"
        match this.ToString() with
        | s when s.Contains(over12) ->
            [$"{prefix}-"; s.Replace("Size", String.Empty).Replace(over12, String.Empty)] |> String.concat String.Empty
        | s -> s |> toKabobCase |> Option.get |> fun s -> $"{prefix}-{s}"

    ///<summary>Returns the CSS class name of the Bulma <c>column</c> width.</summary>
    member this.CssClass = this.getCssClass "is"

    ///<summary>Returns the CSS class name of the Bulma <c>column</c> breakpoint.</summary>
    member this.CssBreakpointClass (breakpoint: BulmaBreakpoint) = $"is-narrow-{breakpoint.Value}"

    ///<summary>Returns the CSS class name of the Bulma <c>column</c> offset.</summary>
    member this.CssOffsetClass = this.getCssClass "is-offset"

///<summary>
/// Defines Bulma widths for the Bulma <c>container</c>.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/layout/container/
/// </remarks>
type BulmaContainerWidth =
    /// <summary> a Bulma <c>container</c> width </summary>
    | ContainerWidthFluid
    /// <summary> a Bulma <c>container</c> width </summary>
    | ContainerWidthFullHD
    /// <summary> a Bulma <c>container</c> width </summary>
    | ContainerWidthMaxDesktop
    /// <summary> a Bulma <c>container</c> width </summary>
    | ContainerWidthMaxWidescreen
    /// <summary> a Bulma <c>container</c> width </summary>
    | ContainerWidthWidescreen

    ///<summary>Returns the CSS class name of the Bulma <c>container</c> width.</summary>
    member this.CssClass =
        match this with
        | ContainerWidthFluid -> "is-fluid"
        | ContainerWidthFullHD -> "is-fullhd"
        | ContainerWidthMaxDesktop -> "is-max-desktop"
        | ContainerWidthMaxWidescreen -> "is-max-widescreen"
        | ContainerWidthWidescreen -> "is-widescreen"

///<summary>
/// Defines the seven Bulma font sizes in <c>rem</c>.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/helpers/typography-helpers/
/// </remarks>
type BulmaFontSize =
    ///<summary>3rem</summary>
    | Size1
    ///<summary>2.5rem</summary>
    | Size2
    ///<summary>2rem</summary>
    | Size3
    ///<summary>1.5rem</summary>
    | Size4
    ///<summary>1.25rem</summary>
    | Size5
    ///<summary>1rem</summary>
    | Size6
    ///<summary>0.75rem</summary>
    | Size7

    ///<summary>Returns the <see cref="string" /> representation of the Bulma font size.</summary>
    member this.Value = this.ToString() |> toNumericString |> Option.get

///<summary>
/// Defines a rule for matching <see cref="BulmaFontSize" /> or a default/inherited size.
///</summary>
type BulmaFontSizeOrDefault =
    /// <summary> match a default/inherited size </summary>
    | DefaultBulmaFontSize
    /// <summary> match <see cref="BulmaFontSize" /> </summary>
    | HasFontSize of BulmaFontSize

    ///<summary>Returns the <see cref="string" /> representation of the Bulma font size or <see cref="String.Empty" />.</summary>
    member this.Value =
        match this with
        | DefaultBulmaFontSize -> String.Empty
        | HasFontSize size -> size.Value

///<summary>
/// Defines the sizes of the Bulma elements,
/// specifically in the hero layout.
///</summary>
///<remarks>
/// 📖 https://bulma.io/documentation/layout/hero/#sizes
///</remarks>
type BulmaElementSize =
    /// <summary> a Bulma hero size </summary>
    | BulmaElementSmall
    /// <summary> a Bulma hero size </summary>
    | BulmaElementMedium
    /// <summary> a Bulma hero size </summary>
    | BulmaElementLarge
    /// <summary> a Bulma hero size </summary>
    | BulmaElementHalfHeight
    /// <summary> a Bulma hero size </summary>
    | BulmaElementFullHeight
    /// <summary> a Bulma hero size </summary>
    | HeroFullHeightWithNavbar

    ///<summary>Returns the CSS class name of the Bulma hero size.</summary>
    member this.CssClass =
        match this with
        | HeroFullHeightWithNavbar -> "is-fullheight-with-navbar"
        | _ -> this.ToString().Replace("BulmaElement", String.Empty).ToLowerInvariant() |> fun s -> $"is-{s}"

///<summary>
/// Defines the 12 Bulma horizontal-space sizes.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/columns/sizes/#12-columns-system
/// 📖 https://bulma.io/documentation/layout/tiles/
/// </remarks>
type BulmaHorizontalSize =
    ///<summary>the available horizontal space</summary>
    | HSizeAuto
    ///<summary>1/12 of the horizontal space</summary>
    | HSize1
    ///<summary>2/12 of the horizontal space</summary>
    | HSize2
    ///<summary>3/12 of the horizontal space</summary>
    | HSize3
    ///<summary>4/12 of the horizontal space</summary>
    | HSize4
    ///<summary>5/12 of the horizontal space</summary>
    | HSize5
    ///<summary>6/12 of the horizontal space</summary>
    | HSize6
    ///<summary>7/12 of the horizontal space</summary>
    | HSize7
    ///<summary>8/12 of the horizontal space</summary>
    | HSize8
    ///<summary>9/12 of the horizontal space</summary>
    | HSize9
    ///<summary>10/12 of the horizontal space</summary>
    | HSize10
    ///<summary>11/12 of the horizontal space</summary>
    | HSize11
    ///<summary>12/12 of the horizontal space</summary>
    | HSize12

    ///<summary>Returns the <see cref="string" /> representation of the horizontal space.</summary>
    member this.Size = this.ToString() |> toNumericString |> Option.get

    ///<summary>Returns the CSS class name of the Bulma horizontal-space size.</summary>
    member this.CssClass =
        match this with
        | HSizeAuto -> String.Empty 
        | _ -> this.Size |> fun s -> $"is-{s}"

    /// <summary>Returns the CSS class name of the Bulma horizontal-offset size.</summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/columns/sizes/#offset
    /// </remarks>
    member this.CssOffsetClass =
        match this with
        | HSizeAuto -> String.Empty 
        | _ -> this.Size |> fun s -> $"is-offset-{s}"

/// <summary>
/// Defines modifiers for the Bulma <c>navbar-dropdown</c>.
/// </summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/components/navbar/#dropdown-menu
/// </remarks>
type BulmaNavbarDropdownModifier =
    /// <summary> a Bulma <c>navbar-dropdown</c> modifier </summary>
    | NavbarDropdownAlignRight
    /// <summary> a Bulma <c>navbar-dropdown</c> modifier </summary>
    | NavbarDropdownBoxed
    /// <summary> a Bulma <c>navbar-dropdown</c> modifier </summary>
    | NavbarDropdownHoverable
    /// <summary> a Bulma <c>navbar-dropdown</c> modifier </summary>
    | NavbarDropUp

    ///<summary>Returns the Bulma CSS class name of the Bulma dropdown modifier.</summary>
    member this.CssClass =
        match this with
        | NavbarDropdownAlignRight -> AlignRight.CssClass
        | NavbarDropdownBoxed -> "is-boxed"
        | NavbarDropdownHoverable -> "is-hoverable"
        | NavbarDropUp -> "has-dropdown-up"

/// <summary>
/// Defines modifiers for the Bulma <c>navbar</c>.
/// </summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/components/navbar/#transparent-navbar
/// 📖 https://bulma.io/documentation/components/navbar/#fixed-navbar
/// 📖 https://bulma.io/documentation/components/navbar/#navbar-helper-classes
/// </remarks>
type BulmaNavbarModifier =
    /// <summary> a Bulma <c>navbar</c> modifier </summary>
    | NavbarAncestorHasFixedTop
    /// <summary> a Bulma <c>navbar</c> modifier </summary>
    | NavbarAncestorHasFixedBottom
    /// <summary> a Bulma <c>navbar</c> modifier </summary>
    | NavbarTransparent
    /// <summary> a Bulma <c>navbar</c> modifier </summary>
    | NavbarFixedTop
    /// <summary> a Bulma <c>navbar</c> modifier </summary>
    | NavbarFixedBottom
    /// <summary> a Bulma <c>navbar</c> modifier </summary>
    | NavbarHasShadow
    /// <summary> a Bulma <c>navbar</c> modifier </summary>
    | NavbarIsSpaced

    ///<summary>Returns the Bulma CSS class name of the Bulma <c>navbar</c> modifier.</summary>
    member this.CssClass =
        match this with
        | NavbarAncestorHasFixedTop -> "has-navbar-fixed-top"
        | NavbarAncestorHasFixedBottom -> "has-navbar-fixed-bottom"
        | _ ->
            let name = this.ToString().Replace("Navbar", String.Empty) |> toKabobCase |> Option.get
            $"is-{name}"

///<summary>
/// Defines all Bulma ordered list alternatives.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/elements/content/#ordered-lists-alternatives
/// </remarks>
type BulmaOrderedListAlternative =
    /// <summary> a Bulma ordered list alternative </summary>
    | LowerAlpha
    /// <summary> a Bulma ordered list alternative </summary>
    | LowerRoman
    /// <summary> a Bulma ordered list alternative </summary>
    | UpperAlpha
    /// <summary> a Bulma ordered list alternative </summary>
    | UpperRoman

    ///<summary>Returns the CSS class color name of the Bulma color classification.</summary>
    member this.CssClass =
        let name = this.ToString() |> toKabobCase |> Option.get
        $"is-{name}"

/// <summary>
/// Defines Bulma shade classifications.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/helpers/color-helpers/
///
/// An explanation of the <c>*Bis</c> and <c>*Ter</c> suffixes:
/// 📰 https://github.com/jgthms/bulma/issues/1756#issuecomment-376188013
/// </remarks>
type BulmaShade =
    /// <summary> a Bulma shade classification </summary>
    | ShadeBlackBis
    /// <summary> a Bulma shade classification </summary>
    | ShadeBlackTer
    /// <summary> a Bulma shade classification </summary>
    | ShadeGreyDarker
    /// <summary> a Bulma shade classification </summary>
    | ShadeGreyDark
    /// <summary> a Bulma shade classification </summary>
    | ShadeGrey
    /// <summary> a Bulma shade classification </summary>
    | ShadeGreyLight
    /// <summary> a Bulma shade classification </summary>
    | ShadeGreyLighter
    /// <summary> a Bulma shade classification </summary>
    | ShadeWhiteTer
    /// <summary> a Bulma shade classification </summary>
    | ShadeWhiteBis

    ///<summary>Returns the CSS class color name of the Bulma color classification.</summary>
    member this.ShadeName = this.ToString().Replace("Shade", String.Empty) |> toKabobCase |> Option.get

    ///<summary>Returns the Bulma background-color CSS class name of the Bulma color classification.</summary>
    member this.BackgroundCssClass = $"has-background-{this.ShadeName}"

    ///<summary>Returns the Bulma text-color CSS class name of the Bulma color classification.</summary>
    member this.TextCssClass = $"has-text-{this.ShadeName}"

///<summary>
/// Defines all Bulma size modifiers.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/components/pagination/#sizes
/// 📖 https://bulma.io/documentation/elements/button/#sizes
/// 📖 https://bulma.io/documentation/form/general/#horizontal-form
/// 📖 https://bulma.io/documentation/layout/section/#sizes
/// </remarks>
type BulmaSizeModifier =
    /// <summary> a Bulma size modifier </summary>
    | SizeSmall
    /// <summary> a Bulma size modifier </summary>
    | SizeNormal
    /// <summary> a Bulma size modifier </summary>
    | SizeMedium
    /// <summary> a Bulma size modifier </summary>
    | SizeLarge

    ///<summary>Returns the Bulma CSS class name of the Bulma size modifier.</summary>
    member this.CssClass = this.ToString().Replace("Size", String.Empty).ToLowerInvariant() |> fun s -> $"is-{s}"

///<summary>
/// Defines all Bulma square dimensions.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/elements/image/#fixed-square-images
/// </remarks>
type BulmaSquareDimension =
    /// <summary> a Bulma square dimension </summary>
    | Square16
    /// <summary> a Bulma square dimension </summary>
    | Square24
    /// <summary> a Bulma square dimension </summary>
    | Square32
    /// <summary> a Bulma square dimension </summary>
    | Square48
    /// <summary> a Bulma square dimension </summary>
    | Square64
    /// <summary> a Bulma square dimension </summary>
    | Square96
    /// <summary> a Bulma square dimension </summary>
    | Square128

    ///<summary>Returns the CSS class name of the Bulma square dimension.</summary>
    member this.CssClass = this.ToString() |> toNumericString |> Option.get |> fun n -> $"is-{n}x{n}"

    ///<summary>Returns the integer representation of the Bulma square dimension.</summary>
    member this.ToWidthOrHeight = this.ToString() |> toNumericString |> Option.get |> Int32.Parse

///<summary>
/// Defines all Bulma ratios of dimensions for Bulma responsive images.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/elements/image/#responsive-images-with-ratios
/// </remarks>
type BulmaRatioDimension =
    /// <summary> <see cref="BulmaSquareDimension" /> </summary>
    | Square of BulmaSquareDimension
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | FiveByFour
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | FourByThree
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | ThreeByTwo
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | FiveByThree
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | SixteenByNine
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | TwoByOne
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | ThreeByOne
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | FourByFive
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | ThreeByFour
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | TwoByThree
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | ThreeByFive
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | NineBySixteen
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | OneByTwo
    /// <summary> a Bulma ratio of dimensions for a Bulma responsive image</summary>
    | OneByThree

    ///<summary>Returns the CSS class name of the Bulma ratio of dimensions.</summary>
    member this.CssClass =
        match this with
        | Square value -> value.CssClass
        | FiveByFour -> $"is-{Five.Value}by{Four.Value}"
        | FourByThree -> $"is-{Four.Value}by{Three.Value}"
        | ThreeByTwo -> $"is-{Three.Value}by{Two.Value}"
        | FiveByThree -> $"is-{Five.Value}by{Three.Value}"
        | SixteenByNine -> $"is-{Sixteen.Value}by{Nine.Value}"
        | TwoByOne -> $"is-{Two.Value}by{One.Value}"
        | ThreeByOne -> $"is-{Three.Value}by{One.Value}"
        | FourByFive -> $"is-{Four.Value}by{Five.Value}"
        | ThreeByFour -> $"is-{Three.Value}by{Four.Value}"
        | TwoByThree -> $"is-{Two.Value}by{Three.Value}"
        | ThreeByFive -> $"is-{Three.Value}by{Five.Value}"
        | NineBySixteen -> $"is-{Nine.Value}by{Sixteen.Value}"
        | OneByTwo -> $"is-{One.Value}by{Two.Value}"
        | OneByThree -> $"is-{One.Value}by{Three.Value}"

///<summary>
/// Defines all Bulma value suffixes for Bulma <c>property-direction</c> combinations.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/helpers/spacing-helpers/
/// </remarks>
type BulmaValueSuffix =
    /// <summary> a Bulma value suffix </summary>
    | L0
    /// <summary> a Bulma value suffix </summary>
    | L1
    /// <summary> a Bulma value suffix </summary>
    | L2
    /// <summary> a Bulma value suffix </summary>
    | L3
    /// <summary> a Bulma value suffix </summary>
    | L4
    /// <summary> a Bulma value suffix </summary>
    | L5
    /// <summary> a Bulma value suffix </summary>
    | L6
    /// <summary> a Bulma value suffix </summary>
    | AutoSuffix

    ///<summary>Returns the <see cref="string" /> representation of the Bulma value suffix.</summary>
    member this.Value =
        match this with
        | AutoSuffix -> "auto"
        | _ -> this.ToString() |> toNumericString |> Option.get

///<summary>
/// Defines the value of a Bulma spacing helper.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/helpers/spacing-helpers/
/// </remarks>
type BulmaSpacing =
    /// <summary> a Bulma spacing helper value </summary>
    | BulmaSpacing of CssBoxModel * BulmaValueSuffix

    /// <summary> unwraps the underlying <c>CssBoxModel * BulmaValueSuffix</c> </summary>
    member this.Value = match this with | BulmaSpacing (b, s) -> b, s

///<summary>
/// Defines the Bulma visibility-helper CSS classes.
///</summary>
/// <remarks>
/// 📖 https://bulma.io/documentation/helpers/visibility-helpers/
/// </remarks>
type BulmaVisibility =
    /// <summary> a Bulma visibility-helper CSS class </summary>
    | DisplayBlock
    /// <summary> a Bulma visibility-helper CSS class </summary>
    | DisplayFlex
    /// <summary> a Bulma visibility-helper CSS class </summary>
    | DisplayInline
    /// <summary> a Bulma visibility-helper CSS class </summary>
    | DisplayInlineBlock
    /// <summary> a Bulma visibility-helper CSS class </summary>
    | DisplayInlineFlex
    /// <summary> a Bulma visibility-helper CSS class </summary>
    | ScreenReaderOnly
    /// <summary> a Bulma visibility-helper CSS class </summary>
    | NonDisplayInvisible
    /// <summary> a Bulma visibility-helper CSS class </summary>
    | NonDisplayHidden

    ///<summary>Returns the CSS class name of the Bulma visibility helper.</summary>
    member this.CssClass =
        let s = this.ToString()
        let display = "Display"
        let nonDisplay = "NonDisplay"
        let getCssClassName ov = (s.Replace(ov, String.Empty) |> toKabobCase |> Option.get |> fun s -> $"is-{s}")

        match this with
        | DisplayBlock | DisplayFlex | DisplayInline | DisplayInlineBlock | DisplayInlineFlex
            -> display |> getCssClassName
        | NonDisplayHidden | NonDisplayInvisible
            -> nonDisplay |> getCssClassName
        | ScreenReaderOnly -> "is-sr-only"
