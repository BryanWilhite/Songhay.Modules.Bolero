namespace Songhay.Modules.Bolero.Visuals.Bulma

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.SvgUtility

///<summary>
/// Bulma Elements
/// “Essential interface elements that only require a single CSS class…”
/// — https://bulma.io/documentation/elements/
///</summary>
module Element =
    /// <summary>
    /// “A single class to handle WYSIWYG generated content, where only HTML tags are available…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/elements/content/
    /// </remarks>
    let bulmaContent (moreClasses: CssClassesOrEmpty) (childNode: Node) =
        div {
            CssClasses [ CssClass.content ] |> moreClasses.ToHtmlClassAttribute

            childNode
        }

    ///<summary>
    /// Returns the HTML <c>details</c> element and its child <c>summary</c> element, adorned with any CSS classes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/details
    /// </remarks>
    let bulmaDetailsElement (cssClasses: CssClassesOrEmpty) (summaryNode: Node) (detailsNode: Node) =
        details {
            cssClasses.Value
            summary {
                CssClass.elementIsClickable |> CssClasses.toHtmlClass
                summaryNode
            }
            detailsNode
        }

    /// <summary>
    /// “…a container for any type of icon font…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/elements/icon/
    /// </remarks>
    let bulmaIcon (visualNode: Node) = span { "icon" |> CssClasses.toHtmlClass; AriaHidden.ToAttrWithTrueValue; visualNode }

    /// <summary>
    /// Calls <see cref="svgViewBox" /> with the specified <see cref="BulmaSquareDimension" />.
    /// </summary>
    let bulmaIconSvgViewBox (square: BulmaSquareDimension) =
        svgViewBox (0,0) (square.ToWidthOrHeight, square.ToWidthOrHeight)

    /// <summary>
    /// “You can combine an icon with text, using the icon-text wrapper…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/elements/icon/#icon-text
    /// </remarks>
    let bulmaIconText (moreClasses: CssClassesOrEmpty) (attr: HtmlAttributeOrEmpty) (childNode: Node) =
        span {
            CssClasses [ "icon-text" ] |> moreClasses.ToHtmlClassAttribute
            attr.Value
            span { childNode }
        }

    /// <summary>
    /// “A container for responsive images…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/elements/image/
    /// </remarks>
    let bulmaImageContainer (size: BulmaRatioDimension) (attributes: HtmlAttributeOrEmpty) (visualNode: Node) =
        figure {
            size |> CssClass.imageContainer |> CssClasses.toHtmlClassFromList
            attributes.Value

            visualNode
        }

    /// <summary>
    /// “Bold notification blocks, to alert your users of something…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/elements/notification/
    /// </remarks>
    let bulmaNotification (moreClasses: CssClassesOrEmpty) (childNode: Node) =
        div {
             CssClasses [ CssClass.notification ] |> moreClasses.ToHtmlClassAttribute

             childNode
        }

    ///<summary>
    /// Returns the HTML <c>progress</c> element, adorned with any CSS classes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/progress
    /// </remarks>
    let bulmaProgressElement (moreClasses: CssClassesOrEmpty) ((value, max): int * int) (childNode: Node) =
        progress {
            CssClasses [ "progress" ] |> moreClasses.ToHtmlClassAttribute

            attr.value value
            attr.max max

            childNode

        }
