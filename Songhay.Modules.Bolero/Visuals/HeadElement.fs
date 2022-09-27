namespace Songhay.Modules.Bolero.Visuals

open System
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.MimeTypes
open Songhay.Modules.Bolero.Visuals.BodyElement

///<summary>
/// Shared functions for generating selected HTML elements as a child of <c>head</c>.
///</summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/head
/// </remarks>
module HeadElement =

    /// <summary>
    /// Returns a <c>head</c> element that
    /// “…specifies the base URL to use for all relative URLs in a document.
    /// There can be only one <c>base</c> element in a document.”
    /// </summary>
    /// <remarks> 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/base </remarks>
    let baseElement (href: string option) = ``base`` { attr.href (href |> Option.defaultWith (fun _ -> "/")) }

    /// <summary>
    /// Returns a <c>link</c> element that
    /// “…specifies relationships between the current document and an external resource.”
    /// </summary>
    /// <remarks> 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/link </remarks>
    let linkRelElement (rel: HtmlLinkedDocumentRelationship) (moreAttributes: HtmlAttributeOrEmpty) (href: Uri) =
        match rel with
        | RelIcon ->
            link { attr.rel RelIcon.Value; attr.``type`` "image/x-icon"; moreAttributes.Value; attr.href href }
        | RelBookmark | RelExternal | RelNoFollow | RelNoOpener | RelNoReferrer | RelTag
            -> htmlComment $"ERROR: the `link` element does not support `{rel.Value}`."
        | _ -> link { attr.rel rel.Value; moreAttributes.Value; attr.href href }

    /// <summary>
    /// Calls <see cref="linkRelElement" /> to return a <c>link</c> element for Atom feed syndication.
    /// </summary>
    /// <remarks> 📖 https://en.wikipedia.org/wiki/Atom_(web_standard) </remarks>
    let linkRelAtomSyndicationElement (appTitle: string) (href: Uri) =
        linkRelElement
            RelAlternate
            (HasAttr (attrs { attr.``type`` ApplicationAtomXml; attr.title appTitle }))
            href

    /// <summary>
    /// Returns a <c>title</c> element that
    /// “…defines the document’s title that is shown in a browser’s title bar or a page’s tab.”
    /// </summary>
    /// <remarks> 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/title </remarks>
    let titleElement (appTitle: string) = title { text appTitle }
