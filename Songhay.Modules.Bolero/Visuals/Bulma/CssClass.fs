namespace Songhay.Modules.Bolero.Visuals.Bulma

open Songhay.Modules.Models
open Songhay.Modules.Bolero.Models

///<summary>
/// Bulma CSS class-name functions and literals.
///</summary>
module CssClass =

    ///<summary>
    /// Bulma CSS class-name literal for Bulma box elements.
    ///</summary>
    ///<remarks>
    /// “The box element is a simple container with a white background, some padding, and a box shadow…”
    /// 📖 https://bulma.io/documentation/elements/box/
    ///</remarks>
    [<Literal>]
    let box = "box"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma button element.
    ///</summary>
    ///<remarks>
    /// “The button is an essential element of any design. It’s meant to look and behave as an interactive element of your page…”
    /// 📖 https://bulma.io/documentation/elements/button/
    ///</remarks>
    [<Literal>]
    let buttonClass = "button"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma card component.
    ///</summary>
    ///<remarks>
    /// “The card component comprises several elements that you can mix and match…”
    /// 📖 https://bulma.io/documentation/components/card/
    ///</remarks>
    [<Literal>]
    let card = "card"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma card element.
    ///</summary>
    ///<remarks>
    /// This is the container/wrapper for the <see cref="content" /> block.
    /// 📖 https://bulma.io/documentation/components/card/
    ///</remarks>
    [<Literal>]
    let cardContent = "card-content"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma card element.
    ///</summary>
    ///<remarks>
    /// This is the container/wrapper for the Bulma <see cref="image" />.
    /// 📖 https://bulma.io/documentation/components/card/
    ///</remarks>
    [<Literal>]
    let cardImage = "card-image"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    ///</remarks>
    [<Literal>]
    let checkbox = "checkbox"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma content.
    ///</summary>
    ///<remarks>
    /// Both the card component and the media layout use this class name.
    ///</remarks>
    [<Literal>]
    let content = "content"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma layout.
    ///</summary>
    ///<remarks>
    /// “…a simple utility element that allows you to center content on larger viewports.
    /// It can be used in any context, but mostly as a direct child
    /// of one of the following:
    /// • <c>.navbar</c>
    /// • <c>.hero</c>
    /// • <c>.section</c>
    /// • <c>.footer</c>
    /// ”
    /// 📖 https://bulma.io/documentation/layout/container/
    ///</remarks>
    let container (width: BulmaContainerWidth) = $"container {width.CssClass}"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    ///</remarks>
    [<Literal>]
    let control = "control"

    ///<summary>
    /// Bulma CSS class-name function for flex content alignment.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/flexbox-helpers/#align-content
    ///</remarks>
    let elementFlexContentAlignment (wrap: CssBoxAlignment) =
        let suffix =
            match wrap with
            | InheritBoxAlignment _ -> None
            | FlexStart
            | FlexEnd
            | Center
            | SpaceAround
            | SpaceBetween
            | SpaceEvenly
            | Stretch
            | End
            | BaseLine
                -> Some wrap.Value
            | _ -> Some Start.Value

        if suffix.IsNone then System.String.Empty
        else $"is-align-content-{suffix.Value}"

    ///<summary>
    /// Bulma CSS class-name function for flex direction.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/flexbox-helpers/#flex-direction
    ///</remarks>
    let elementFlexDirection (direction: CssFlexDirection) =
        $"is-flex-direction-{direction.Value}"

    ///<summary>
    /// Bulma CSS class-name function for flex grow.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/flexbox-helpers/#flex-grow-and-flex-shrink
    ///</remarks>
    let elementFlexGrow (level: BulmaValueSuffix) =
        let value =
            match level with
            | L0 | L1 | L2 | L3 | L4 | L5 -> level.Value
            | _ -> L0.Value

        $"is-flex-grow-{value}"

    ///<summary>
    /// Bulma CSS class-name function for justifying content.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/flexbox-helpers/#justify-content
    ///</remarks>
    let elementFlexJustifyContent (boxAlignment: CssBoxAlignment) =
        let suffix =
            match boxAlignment with
            | InheritBoxAlignment _ -> None
            | FlexStart
            | FlexEnd
            | Center
            | SpaceAround
            | SpaceBetween
            | SpaceEvenly
            | Start
            | End
            | Right
                -> Some boxAlignment.Value
            | _ -> Some Left.Value

        if suffix.IsNone then System.String.Empty
        else $"is-justify-content-{suffix.Value}"

    ///<summary>
    /// Bulma CSS class-name function for flex items alignment.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/flexbox-helpers/#align-items
    ///</remarks>
    let elementFlexItemsAlignment (wrap: CssBoxAlignment) =
        let suffix =
            match wrap with
            | InheritBoxAlignment _ -> None
            | Stretch
            | FlexStart
            | FlexEnd
            | Center
            | BaseLine
            | End
            | SelfStart
            | SelfEnd
                -> Some wrap.Value
            | _ -> Some Start.Value

        if suffix.IsNone then System.String.Empty
        else $"is-align-items-{suffix.Value}"

    ///<summary>
    /// Bulma CSS class-name function for flex “self” alignment.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/flexbox-helpers/#align-items
    ///</remarks>
    let elementFlexSelfAlignment (wrap: CssBoxAlignment) =
        let suffix =
            match wrap with
            | FlexStart
            | FlexEnd
            | Center
            | BaseLine
            | Stretch
                -> wrap.Value
            | _ -> "auto"

        $"is-align-self-{suffix}"

    ///<summary>
    /// Bulma CSS class-name function for flex shrink.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/flexbox-helpers/#flex-grow-and-flex-shrink
    ///</remarks>
    let elementFlexShrink (level: BulmaValueSuffix) =
        let value =
            match level with
            | L0 | L1 | L2 | L3 | L4 | L5 -> level.Value
            | _ -> L0.Value

        $"is-flex-shrink-{value}"

    ///<summary>
    /// Bulma CSS class-name function for flex wrapping.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/flexbox-helpers/#flex-wrap
    ///</remarks>
    let elementFlexWrap (wrap: CssFlexWrap) =
        $"is-flex-wrap-{wrap.Value}"

    ///<summary>
    /// Bulma CSS class-name function for typography.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/typography-helpers/#font-family
    ///</remarks>
    let elementFontFamily (family: CssFontFamily) =
        let suffix =
            match family with
            | SansSerif | Monospace | Primary | Secondary -> family.Value
            | _ -> "code"

        $"is-family-{suffix}"

    ///<summary>
    /// Bulma CSS class-name function for typography.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/typography-helpers/#text-weight
    ///</remarks>
    let elementFontWeight (weight: CssFontWeight) = $"has-text-weight-{weight.Value}"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    [<Literal>]
    let elementIsActive = "is-active"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// Applies <c>cursor: pointer !important</c> to the element.
    ///</remarks>
    [<Literal>]
    let elementIsClickable = "is-clickable"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    [<Literal>]
    let elementIsBlock = "is-block"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// Adds overflow hidden
    ///</remarks>
    [<Literal>]
    let elementIsClipped = "is-clipped"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// Adds overflow hidden
    ///</remarks>
    [<Literal>]
    let elementIsFlex = "is-flex"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls
    /// and the Bulma tab component.
    ///</summary>
    ///<remarks>
    /// “If you want a full width…” child element of the <see cref="control"/> container.
    /// 📖 https://bulma.io/documentation/form/general/#form-addons
    ///
    /// “If you want the tabs to take up the whole width available, use is-fullwidth.”
    /// 📖 https://bulma.io/documentation/components/tabs/#styles
    ///</remarks>
    [<Literal>]
    let elementIsFullWidth = "is-fullwidth"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    [<Literal>]
    let elementIsHidden = "is-hidden"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// Hide elements visually but keep the element available to be announced by a screen reader
    ///</remarks>
    [<Literal>]
    let elementIsHiddenVisually = "is-sr-only"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    [<Literal>]
    let elementIsInlineBlock = "is-inline-block"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// Adds visibility hidden
    ///</remarks>
    [<Literal>]
    let elementIsInvisible = "is-invisible"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// Completely covers the first positioned parent
    ///</remarks>
    [<Literal>]
    let elementIsOverlay = "is-overlay"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    [<Literal>]
    let elementIsRelative = "is-relative"

    ///<summary>
    /// Bulma CSS class-name function for typography.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/typography-helpers/#alignment
    ///</remarks>
    let elementTextAlign (alignment: BulmaAlignment) =
        let suffix =
            match alignment with
            | AlignCentered | AlignJustified | AlignRight -> alignment.AlignmentName
            | _ -> AlignLeft.AlignmentName

        $"has-text-{suffix}"

    ///<summary>
    /// Bulma CSS class-name function for typography.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/helpers/typography-helpers/#text-transformation
    ///</remarks>
    let elementTextTransformation (transformation: CssTextTransformation) =
        let suffix =
            match transformation with
            | TitleCase -> "capitalized"
            | _ -> transformation.Value

        $"is-{suffix}"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// Prevents the text from being selectable
    ///</remarks>
    [<Literal>]
    let elementTextIsUnselectable = "is-unselectable"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    ///</remarks>
    [<Literal>]
    let field = "field"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “…to append a static button…”
    /// 📖 https://bulma.io/documentation/form/general/#form-addons
    ///</remarks>
    [<Literal>]
    let fieldControlIsStatic = "is-static"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “If you want to attach controls together, use the <c>has-addons</c> modifier on the <c>field</c> container…”
    /// 📖 https://bulma.io/documentation/form/general/#form-addons
    ///</remarks>
    [<Literal>]
    let fieldHasAddOns = "has-addons"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “Use the <c>is-expanded</c> modifier on the element you want to fill up the remaining space…”
    /// 📖 https://bulma.io/documentation/form/general/#form-addons
    ///</remarks>
    [<Literal>]
    let fieldIsExpanded = "is-expanded"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “If you want to group controls together, use the <c>is-grouped</c> modifier on the <c>field</c> container…”
    /// 📖 https://bulma.io/documentation/form/general/#form-group
    ///</remarks>
    let fieldIsGrouped (alignment: BulmaAlignment) =
        let isGrouped = "is-grouped"
        match alignment with
        | AlignRight -> [ isGrouped; "is-grouped-right" ]
        | AlignCentered -> [ isGrouped; "is-grouped-centered" ]
        | _ -> [ isGrouped ]

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “Add the <c>is-grouped-multiline</c> modifier to allow controls to fill up multiple lines…”
    /// 📖 https://bulma.io/documentation/form/general/#form-group
    ///</remarks> 
    [<Literal>]
    let fieldIsGroupedMultiline = "is-grouped-multiline"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “…use the <c>is-horizontal</c> modifier on the <c>field</c> container…”
    /// 📖 https://bulma.io/documentation/form/general/#horizontal-form
    ///</remarks>
    [<Literal>]
    let fieldIsHorizontal = "is-horizontal"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “If you want a horizontal form control, use… <c>field-body</c> for the input/select/textarea container…”
    /// 📖 https://bulma.io/documentation/form/general/#horizontal-form
    /// <seealso cref="label"/>
    ///</remarks>
    [<Literal>]
    let fieldBody = "field-body"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “If you want a horizontal form control, use… <c>field-label</c> for the side label…”
    /// 📖 https://bulma.io/documentation/form/general/#horizontal-form
    /// <seealso cref="label"/>
    ///</remarks>
    [<Literal>]
    let fieldLabel = "field-label"

    ///<summary>
    /// Bulma CSS class-name function.
    ///</summary>
    let fontSize (size: BulmaFontSize) = $"is-size-{size.Value}"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/form/general/#with-icons
    /// <seealso cref="formIconAlign"/>
    ///</remarks>
    let formControlIconAlign (alignment: BulmaAlignment) =
        let suffix =
            match alignment with
            | AlignLeft | AlignRight -> alignment.AlignmentName
            | _ -> AlignLeft.AlignmentName

        $"has-icons-{suffix}"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/form/general/#with-icons
    /// <seealso cref="formControlIconAlign"/>
    ///</remarks>
    let formIconAlign (alignment: BulmaAlignment) =
        let suffix =
            match alignment with
            | AlignLeft | AlignRight -> alignment.AlignmentName
            | _ -> AlignLeft.AlignmentName

        $"is-{suffix}"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    ///</remarks>
    [<Literal>]
    let help = "help"

    ///<summary>
    /// Bulma CSS class-name function.
    ///</summary>
    let hidden (breakpoint: BulmaBreakpoint) = $"{elementIsHidden}-{breakpoint.Value}"

    ///<summary>
    /// Bulma CSS class-name for Bulma elements.
    ///</summary>
    ///<remarks>
    /// “A container for responsive images…”
    /// 📖 https://bulma.io/documentation/elements/image/
    ///</remarks>
    [<Literal>]
    let image = "image"

    ///<summary>
    /// Bulma CSS class-name function for Bulma elements.
    ///</summary>
    ///<remarks>
    /// Returns the <see cref="image" /> CSS class name
    /// with <see cref="BulmaRatioDimension.CssClass" />.
    /// 📖 https://bulma.io/documentation/elements/image/
    ///</remarks>
    let imageContainer (dimension: BulmaRatioDimension) = [ image; dimension.CssClass ]

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// “You can also make rounded images, using is-rounded class…”
    /// 📖 https://bulma.io/documentation/elements/image/#rounded-images
    ///</remarks>
    [<Literal>]
    let imageIsRounded = "is-rounded"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    ///</remarks>
    [<Literal>]
    let input = "input"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/overview/modifiers/
    ///</remarks>
    [<Literal>]
    let isLoadingModifier = "is-loading"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// “By default, columns are only activated on tablet and desktop.
    /// If you want to use columns on mobile too,
    /// add the is-mobile modifier on the columns container.”
    /// 📖 https://github.com/jgthms/bulma/blob/master/docs/_posts/2016-02-09-blog-launched-new-responsive-columns-new-helpers.md
    ///</remarks>
    [<Literal>]
    let isMobileModifier = "is-mobile"

    ///<summary>
    /// Bulma CSS class-name literal.
    ///</summary>
    ///<remarks>
    /// 📖 https://bulma.io/documentation/overview/modifiers/
    ///</remarks>
    [<Literal>]
    let isOutlinedModifier = "is-outlined"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    /// <seealso cref="fieldLabel"/>
    ///</remarks>
    [<Literal>]
    let label = "label"

    ///<summary>
    /// Bulma CSS class-name function for layout.
    ///</summary>
    ///<remarks>
    /// “A multi-purpose horizontal level, which can contain almost any other element…”
    /// 📖 https://bulma.io/documentation/layout/level/
    ///</remarks>
    [<Literal>]
    let levelContainer = "level"

    ///<summary>
    /// Bulma CSS class-name function for layout.
    ///</summary>
    ///<remarks>
    /// Either <c>level-left</c> or <c>level-right</c>.
    /// 📖 https://bulma.io/documentation/layout/level/
    ///</remarks>
    let level (alignment: BulmaAlignment) =
        match alignment with
        | AlignLeft | AlignRight -> $"level-{alignment.AlignmentName}"
        | _ -> "level-left"

    ///<summary>
    /// Bulma CSS class-name function for layout.
    ///</summary>
    ///<remarks>
    /// “In a level-item, you can then insert almost anything you want:
    /// a title, a button, a text input, or just simple text.
    /// No matter what elements you put inside a Bulma level,
    /// they will always be vertically centered.”
    /// 📖 https://bulma.io/documentation/layout/level/
    ///</remarks>
    [<Literal>]
    let levelItem = "level-item"

    ///<summary>
    /// Bulma CSS class-name function for Bulma spacing helpers.
    ///</summary>
    ///<remarks>
    /// This is the margin spacing helper.
    /// “There are 112 spacing helpers to choose from… To use these classes, simply append them to any HTML element…”
    /// 📖 https://bulma.io/documentation/helpers/spacing-helpers/#list-of-all-spacing-helpers
    ///</remarks>
    let m (box: CssBoxModel, suffix: BulmaValueSuffix) = $"m{box.Value}-{suffix.Value}"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma layout.
    ///</summary>
    ///<remarks>
    /// “The famous media object prevalent in social media interfaces, but useful in any context…”
    /// 📖 https://bulma.io/documentation/layout/media-object/
    ///</remarks>
    [<Literal>]
    let media = "media"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma layout.
    ///</summary>
    ///<remarks>
    /// Indicates the leftmost container aside the <c>media-content</c> block
    /// usually containing an avatar, ‘branding’ the media content.
    /// 📖 https://bulma.io/documentation/layout/media-object/
    ///</remarks>
    [<Literal>]
    let mediaLeft = "media-left"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma layout.
    ///</summary>
    ///<remarks>
    /// The container for “any other Bulma element, like inputs, textareas, icons, buttons…”
    /// 📖 https://bulma.io/documentation/layout/media-object/
    ///</remarks>
    let mediaContent ="media-content"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma message component.
    ///</summary>
    ///<remarks>
    /// “Colored message blocks, to emphasize part of your page…”
    /// 📖 https://bulma.io/documentation/components/message/
    ///</remarks>
    [<Literal>]
    let message = "message"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma message component.
    ///</summary>
    ///<remarks>
    /// “Colored message blocks, to emphasize part of your page…”
    /// 📖 https://bulma.io/documentation/components/message/
    ///</remarks>
    [<Literal>]
    let messageBody = "message-body"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma message component.
    ///</summary>
    ///<remarks>
    /// “Colored message blocks, to emphasize part of your page…”
    /// 📖 https://bulma.io/documentation/components/message/
    ///</remarks>
    [<Literal>]
    let messageHeader = "message-header"

    /// <summary>
    /// “A responsive horizontal navbar that can support images, links, buttons, and dropdowns…
    /// <c>navbar-item</c>: each single item of the <c>navbar</c>,
    /// which can either be an <c>a</c> or a <c>div</c>…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/components/navbar/
    /// </remarks>
    let navbarItem = "navbar-item"

    /// <summary>
    /// “A responsive horizontal navbar that can support images, links, buttons, and dropdowns…
    /// <c>navbar-link</c>: a link as the sibling of a dropdown, with an arrow…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/components/navbar/
    /// </remarks>
    let navbarLink = "navbar-link"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma elements.
    ///</summary>
    ///<remarks>
    /// “The notification is a simple colored block meant
    /// to draw the attention to the user about something.
    /// As such, it can be used as a pinned notification
    /// in the corner of the viewport.
    /// That's why it supports the use of the delete element…”
    /// 📖 https://bulma.io/documentation/elements/notification/
    ///</remarks>
    [<Literal>]
    let notification = "notification"

    ///<summary>
    /// Bulma CSS class-name function for Bulma spacing helpers.
    ///</summary>
    ///<remarks>
    /// This is the padding spacing helper.
    /// “There are 112 spacing helpers to choose from… To use these classes, simply append them to any HTML element…”
    /// 📖 https://bulma.io/documentation/helpers/spacing-helpers/#list-of-all-spacing-helpers
    ///</remarks>
    let p (box: CssBoxModel, suffix: BulmaValueSuffix) = $"p{box.Value}-{suffix.Value}"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma panels.
    ///</summary>
    ///<remarks>
    /// “A composable panel, for compact controls…”
    /// 📖 https://bulma.io/documentation/components/panel/
    ///</remarks>
    [<Literal>]
    let panel = "panel"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    ///</remarks>
    [<Literal>]
    let radio = "radio"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    ///</remarks>
    [<Literal>]
    let select = "select"

    ///<summary>
    /// Bulma CSS class-name function for Bulma elements.
    ///</summary>
    ///<remarks>
    /// “Simple headings to add depth to your page… There are 6 sizes available…”
    /// 📖 https://bulma.io/documentation/elements/title/
    ///</remarks>
    let subtitle (size: BulmaFontSizeOrDefault) =
        match size with
        | DefaultBulmaFontSize -> [ "subtitle" ]
        | HasFontSize Size7 -> [ "subtitle"; $"is-{Size6.Value}" ]
        | _ -> ["subtitle"; $"is-{size.Value}"]

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma tab component.
    ///</summary>
    ///<remarks>
    /// If you want a more classic style with borders, just append the <c>is-boxed</c> modifier…”
    /// 📖 https://bulma.io/documentation/components/tabs/#styles
    ///</remarks>
    [<Literal>]
    let tabsElementIsBoxed = "is-boxed"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma tab component.
    ///</summary>
    ///<remarks>
    /// “If you want mutually exclusive tabs (like radio buttons where clicking one deselects all other ones),
    /// use the <c>is-toggle</c> modifier…”
    /// 📖 https://bulma.io/documentation/components/tabs/#styles
    ///</remarks>
    [<Literal>]
    let tabsElementIsToggle = "is-toggle"

    ///<summary>
    /// Bulma CSS class-name literal for the Bulma tab component.
    ///</summary>
    ///<remarks>
    /// “If you use both <c>is-toggle</c> and <c>is-toggle-rounded</c>, the first and last items will be rounded…”
    /// 📖 https://bulma.io/documentation/components/tabs/#styles
    ///</remarks>
    [<Literal>]
    let tabsElementIsToggleRounded = "is-toggle-rounded"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma Form controls.
    ///</summary>
    ///<remarks>
    /// “All generic form controls, designed for consistency…”
    /// 📖 https://bulma.io/documentation/form/general/
    ///</remarks>
    [<Literal>]
    let textarea = "textarea"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma tiles.
    ///</summary>
    ///<remarks>
    /// “To build intricate 2-dimensional layouts, you only need a single element: the tile…”
    /// 📖 https://bulma.io/documentation/layout/tiles/
    ///</remarks>
    [<Literal>]
    let tile = "tile"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma tiles.
    ///</summary>
    ///<remarks>
    /// “Start with an ancestor tile that will wrap all other tiles…”
    /// 📖 https://bulma.io/documentation/layout/tiles/
    ///</remarks>
    [<Literal>]
    let tileIsAncestor = "is-ancestor"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma tiles.
    ///</summary>
    ///<remarks>
    /// “As soon as you want to add content to a tile, just:
    /// - add any class you want, like <c>box</c>
    /// - add the <c>is-child</c> modifier on the tile
    /// - add the <c>is-parent</c> modifier on the parent tile”
    ///
    /// 📖 https://bulma.io/documentation/layout/tiles/
    ///</remarks>
    [<Literal>]
    let tileIsChild = "is-child"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma tiles.
    ///</summary>
    ///<remarks>
    /// “As soon as you want to add content to a tile, just:
    /// - add any class you want, like <c>box</c>
    /// - add the <c>is-child</c> modifier on the tile
    /// - add the <c>is-parent</c> modifier on the parent tile”
    ///
    /// 📖 https://bulma.io/documentation/layout/tiles/
    ///</remarks>
    [<Literal>]
    let tileIsParent = "is-parent"

    ///<summary>
    /// Bulma CSS class-name literal for Bulma tiles.
    ///</summary>
    ///<remarks>
    /// “If you want to stack tiles vertically, add is-vertical on the parent tile…”
    /// 📖 https://bulma.io/documentation/layout/tiles/
    ///</remarks>
    [<Literal>]
    let tileIsVertical = "is-vertical"

    ///<summary>
    /// Bulma CSS class-name function for Bulma elements.
    ///</summary>
    ///<remarks>
    /// “Simple headings to add depth to your page… There are 6 sizes available…”
    /// 📖 https://bulma.io/documentation/elements/title/
    ///</remarks>
    let title (size: BulmaFontSizeOrDefault) =
        match size with
        | DefaultBulmaFontSize -> [ "title" ]
        | HasFontSize Size7 -> [ "title"; $"is-{Size6.Value}" ]
        | _ -> ["title"; $"is-{size.Value}"]
