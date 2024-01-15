namespace Songhay.Modules.Bolero.Visuals

open System

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models

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
        (classesOrEmpty: CssClassesOrEmpty)
        (attributeOrEmpty: HtmlAttributeOrEmpty)
        (childNode: Node) =
        a {
            classesOrEmpty.Value
            attributeOrEmpty.Value
            childNode
        }

    ///<summary>
    /// Returns the HTML <c>a</c> element,
    /// declaring an eventing 🗲🐎 callback and adorned with any CSS classes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/a
    /// </remarks>
    let anchorButtonElement
        (cssClasses: CssClassesOrEmpty)
        (moreAttrs: HtmlAttributeOrEmpty)
        (childNode: Node) =
        a {
            cssClasses.Value

            DomElementEvent.Click.PreventDefault
            attr.href "#"
            moreAttrs.Value

            childNode
        }

    ///<summary>
    /// Returns the HTML <c>button</c> element,
    /// declaring an eventing 🗲🐎 callback and adorned with any CSS classes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/button
    /// </remarks>
    let buttonElement
        (cssClasses: CssClassesOrEmpty)
        (attributeOrEmpty: HtmlAttributeOrEmpty)
        (childNode: Node) =
        button {
            cssClasses.Value

            attributeOrEmpty.Value

            childNode
        }

    ///<summary>
    /// Returns the HTML <c>footer</c> element, adorned with any CSS classes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/footer
    /// </remarks>
    let footerElement (classesOrEmpty: CssClassesOrEmpty) (childNode: Node) =
        footer {
            classesOrEmpty.Value

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
        (classesOrEmpty: CssClassesOrEmpty)
        (moreAttrs: HtmlAttributeOrEmpty)
        (alt: string)
        (src: Uri) =
        img {
            classesOrEmpty.Value

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
    let listItemElement (classesOrEmpty: CssClassesOrEmpty) (moreAttrs: HtmlAttributeOrEmpty) (childNode: Node) =
        li {
            classesOrEmpty.Value
            moreAttrs.Value

            childNode
        }

    ///<summary>
    /// Returns the HTML ordered list element, <c>ol</c>, adorned with any CSS classes and any attributes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/ol
    /// </remarks>
    let orderedList (classesOrEmpty: CssClassesOrEmpty) (liNodes: Node list) =
        ol {
            classesOrEmpty.Value

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
        (classesOrEmpty: CssClassesOrEmpty)
        (moreAttrs: HtmlAttributeOrEmpty)
        (childNode: Node) =
        p {
            classesOrEmpty.Value
            moreAttrs.Value

            childNode
        }

    ///<summary>
    /// Returns the HTML ordered list element, <c>ul</c>, adorned with any CSS classes and any attributes.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/ul
    /// </remarks>
    let unOrderedList (classesOrEmpty: CssClassesOrEmpty) (liNodes: Node list) =
        ul {
            classesOrEmpty.Value

            forEach liNodes <| id
        }
