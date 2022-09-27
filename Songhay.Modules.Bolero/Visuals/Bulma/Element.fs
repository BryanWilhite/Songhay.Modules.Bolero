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
