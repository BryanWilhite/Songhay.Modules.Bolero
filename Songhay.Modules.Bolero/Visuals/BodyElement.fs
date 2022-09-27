namespace Songhay.Modules.Bolero.Visuals

open System
open Microsoft.AspNetCore.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.BoleroUtility
open Songhay.Modules.Bolero.SvgUtility

///<summary>
/// Shared functions for generating selected HTML elements as a child of <c>body</c>.
///</summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/body
/// </remarks>
module BodyElement =

    ///<summary>
    /// Returns the HTML anchor element, <c>a</c>, adorned with any CSS classes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/a
    /// </remarks>
    let anchorElement
        (moreClasses: CssClassesOrEmpty)
        (href: Uri)
        (target: HtmlTargetOrEmpty)
        (moreAttributes: HtmlAttributeOrEmpty)
        (childNode: Node) =
        a {
            moreClasses.Value

            attr.href href.OriginalString
            target.Value
            moreAttributes.Value

            childNode
        }

    ///<summary>
    /// Returns the HTML <c>button</c> element,
    /// declaring an eventing 🗲🐎 callback and adorned with any CSS classes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/button
    /// </remarks>
    let buttonElement (cssClasses: CssClassesOrEmpty) (callback: Web.MouseEventArgs -> unit) =
        button {
            cssClasses.Value

            on.click callback
        }

    ///<summary>
    /// Returns the HTML <c>footer</c> element, adorned with any CSS classes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/footer
    /// </remarks>
    let footerElement (moreClasses: CssClassesOrEmpty) (childNode: Node) =
        footer {
            moreClasses.Value

            childNode
        }

    ///<summary>
    /// Returns an HTML comment.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Learn/HTML/Introduction_to_HTML/Getting_started#html_comments
    /// </remarks>
    let htmlComment (comment: string) = rawHtml $"<!-- {comment} -->"

    ///<summary>
    /// Returns the HTML image embed element, <c>img</c>, adorned with any CSS classes and any attributes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/img
    /// </remarks>
    let imageElement
        (cssClasses: CssClassesOrEmpty)
        (moreAttrs: HtmlAttributeOrEmpty)
        (alt: string)
        (src: Uri) =
        img {
            cssClasses.Value

            attr.src src.OriginalString
            attr.alt alt
            moreAttrs.Value
        }

    ///<summary>
    /// Returns the HTML list item element, <c>li</c>, adorned with any CSS classes and any attributes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/li
    /// </remarks>
    let listItemElement (cssClasses: CssClassesOrEmpty) (moreAttrs: HtmlAttributeOrEmpty) (childNode: Node) =
        li {
            cssClasses.Value
            moreAttrs.Value

            childNode
        }

    ///<summary>
    /// Returns the HTML ordered list element, <c>ol</c>, adorned with any CSS classes and any attributes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/ol
    /// </remarks>
    let orderedList (cssClasses: CssClassesOrEmpty) (liNodes: Node[]) =
        ol {
            cssClasses.Value

            forEach liNodes <| id
        }

    ///<summary>
    /// Returns the HTML paragraph element, <c>p</c>, adorned with any CSS classes and any attributes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/p
    /// 
    /// Remember that the specified child <see cref="Node" /> can be <see cref="rawHtml" />.
    ///</remarks>
    let paragraphElement
        (cssClasses: CssClassesOrEmpty)
        (moreAttrs: HtmlAttributeOrEmpty)
        (childNode: Node) =
        p {
            cssClasses.Value
            moreAttrs.Value

            childNode
        }

    ///<summary>
    /// Returns the HTML <c>svg</c> element, with default attributes,
    /// based on the specified <see cref="StreamGeometry" />.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/svg
    ///</remarks>
    let svgElement (viewBox: string) (pathData: StreamGeometry) =
        svg {
            "xmlns" => SvgUri
            "fill" => "currentColor"
            "preserveAspectRatio" => "xMidYMid meet"
            nameof viewBox => viewBox

            elt "path" { "d" => pathData.Value }
        }

    ///<summary>
    /// Returns the HTML <c>svg</c> element, with default attributes,
    /// declaring <c>use</c> and <c>xlink:href</c>.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/svg
    ///</remarks>
    let svgXLinkSpriteElement (viewBox: string) (symbolId: Identifier) =
        svg {
            "xmlns" => SvgUri
            "fill" => "currentColor"
            "preserveAspectRatio" => "xMidYMid meet"
            nameof viewBox => viewBox

            elt "use" { "xlink:href" => $"#{symbolId.StringValue}" }
        }

    ///<summary>
    /// Returns the HTML <c>svg</c> element,
    /// calling <see cref="StreamGeometry.ToSymbolElement" />
    /// for the specified symbol data.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/svg
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/symbol
    ///</remarks>
    let svgSymbolsContainer (symbolData: (Identifier * StreamGeometry * string)[]) =
        svg {
            "xmlns" => SvgUri
            attr.style "display: none;"
            forEach symbolData <| fun (id, d, _) ->
                concat {
                    newLine
                    indent 3
                    d.ToSymbolElement id
                }
            newLine
            indent 2
        }

    ///<summary>
    /// Returns the HTML ordered list element, <c>ul</c>, adorned with any CSS classes and any attributes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/ul
    /// </remarks>
    let unOrderedList (cssClasses: CssClassesOrEmpty) (liNodes: Node[]) =
        ul {
            cssClasses.Value

            forEach liNodes <| id
        }
