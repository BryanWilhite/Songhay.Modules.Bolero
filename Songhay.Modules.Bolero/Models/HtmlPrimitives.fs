namespace Songhay.Modules.Bolero.Models

open System

open Bolero
open Bolero.Html

open Songhay.Modules.StringUtility

///<summary>
/// Defines a subset of the Global WAI-ARIA attributes.
/// 📖 https://www.w3.org/WAI/standards-guidelines/aria/
///</summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/Accessibility/ARIA/Attributes#global_aria_attributes
/// </remarks>
type AriaGlobal =
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaBusy
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaControls
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaCurrent
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaDescription
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaDisabled
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaErrorMessage
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaExpanded
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaHasPopup
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaHidden
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaInvalid
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaKeyShortcuts
    ///<summary> Global WAI-ARIA attribute </summary>
    | AriaLabel

    ///<summary>Returns the <see cref="string" /> representation of the WAI-ARIA attribute.</summary>
    member this.AttrName =
        match this with
        | AriaErrorMessage -> "aria-errormessage"
        | AriaHasPopup -> "aria-haspopup"
        | AriaKeyShortcuts -> "aria-keyshortcuts"
        | _ -> this.ToString().ToLowerInvariant() |> toKabobCase |> Option.get

    ///<summary>
    /// Returns an <see cref="Attr" /> representing a CSS declaration
    /// with the specified value.
    /// </summary>
    member this.ToAttr (value: string) = (this.AttrName, value) ||> Attr.Make

    ///<summary>
    /// Returns an <see cref="Attr" /> representing a CSS declaration
    /// with a value of <c>true</c> like <c>aria-busy="true"</c>.
    /// </summary>
    member this.ToAttrWithTrueValue = (this.AttrName, "true") ||> Attr.Make

///<summary>
/// Defines selected DOM <c>Element</c> events.
///</summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/API/Element#events
/// </remarks>
type DomElementEvent =
    /// <summary> a DOM <c>Element</c> event </summary>
    | Blur
    /// <summary> a DOM <c>Element</c> event </summary>
    | Change
    /// <summary> a DOM <c>Element</c> event </summary>
    | Click
    /// <summary> a DOM <c>Element</c> event </summary>
    | DblClick
    /// <summary> a DOM <c>Element</c> event </summary>
    | Focus
    /// <summary> a DOM <c>Element</c> event </summary>
    | KeyDown
    /// <summary> a DOM <c>Element</c> event </summary>
    | KeyUp
    /// <summary> a DOM <c>Element</c> event </summary>
    | Load
    /// <summary> a DOM <c>Element</c> event </summary>
    | LoadEnd

    /// <summary>Returns the event name of the DOM <c>Element</c> event.</summary>
    member this.Name = this.ToString().ToLowerInvariant()

    /// <summary>Prevent the default event behavior for a given HTML event.</summary>
    /// <remarks>
    /// This <see cref="on.preventDefault" /> attribute is surely based
    /// on the <c>@on{DOM EVENT}:preventDefault<c> directive attribute of Blazor.
    /// 📖 https://docs.microsoft.com/en-us/aspnet/core/blazor/components/event-handling?view=aspnetcore-6.0#prevent-default-actions
    /// </remarks>
    member this.PreventDefault = on.preventDefault $"on{this.Name}" true

///<summary>
/// Defines a type representing an <see cref="Attr" />
/// or a type representing the non-presence of <see cref="Attr" />.
/// </summary>
/// <remarks>
/// There is no list of <see cref="Attr" /> compliment of this type
/// because <see cref="attrs" /> can express multiple attributes as one.
/// </remarks>
type HtmlAttributeOrEmpty =
    ///<summary> the non-presence of <see cref="Attr" /> </summary>
    | NoAttr
    ///<summary> the presence of one or a concatenation of <see cref="Attr" /> </summary>
    | HasAttr of Attr

    ///<summary>
    /// Returns <see cref="Attr" />,
    /// representing a combination of <see cref="Attr" />
    /// or <see cref="attr.empty" />.
    /// </summary>
    member this.Value =
        match this with
        | NoAttr -> attr.empty()
        | HasAttr attribute -> attribute

///<summary>
/// Defines rules around how to handle a <see cref="Node" />.
/// </summary>
type HtmlChildNodeOrReplaceDefault =
    ///<summary> the <see cref="Node" /> is regarded as a child </summary>
    | ChildNode of Node
    ///<summary> the <see cref="Node" /> is regarded as a replacement </summary>
    | ReplacementNode of Node

///<summary>
/// Defines whether an HTML element is active.
/// This is useful for an HTML element representing a selectable item.
/// </summary>
type HtmlElementActiveOrDefault =
    ///<summary> the HTML element is active </summary>
    | ActiveState
    ///<summary> the HTML element is not active </summary>
    | DefaultState

///<summary>
/// Defines all HTML <c>meta</c> elements.
///</summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta
/// </remarks>
type HtmlMetaElement =
    ///<summary>
    /// “Declares the document’s character encoding… its value must be an ASCII case-insensitive match
    /// for the string <c>"utf-8"</c>, because UTF-8 is the only valid encoding for HTML5 documents.
    /// <c>meta</c> elements which declare a character encoding must be located entirely
    /// within the first 1024 bytes of the document.”
    /// </summary>
    /// <remarks> 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta#attr-charset </remarks>
    | HtmlCharSet
    ///<summary>
    /// “Defines a pragma directive. The attribute is named <c>http-equiv</c>(alent)
    /// because all the allowed values are names of particular HTTP headers…”
    /// </summary>
    /// <remarks> 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta#attr-http-equiv </remarks>
    | HttpEquivalent of string * string
    ///<summary>
    /// “The <c>name</c> and <c>content</c> attributes can be used together
    /// to provide document metadata in terms of name-value pairs…”
    /// </summary>
    /// <remarks> 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta#attr-name </remarks>
    | MetaNameAndContent of string * string

    ///<summary>
    /// Returns a declaration to the browser to:
    /// - Requests the robot to not index the page.
    /// - Requests the robot to not follow the links on the page.
    /// </summary>
    ///<remarks>
    /// The modern default is <c>all</c>:
    /// - Allow the robot to index the page (default)
    /// - Allows the robot to follow the links on the page (default)
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta/name#other_metadata_names
    /// </remarks>
    static member RobotsNoIndexNoFollow = (MetaNameAndContent ("robots", "noindex, nofollow"))

    ///<summary>
    /// Returns a declaration to the browser to “…render this website exactly as wide as you are naturally.”
    /// </summary>
    /// <remarks>
    /// This is responsive web design guidance from 2015: 📖 https://css-tricks.com/probably-use-initial-scale1/
    ///
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta/name#standard_metadata_names_defined_in_other_specifications
    /// </remarks>
    static member ViewPortInitialScale1 = (MetaNameAndContent ("viewport", "width=device-width, initial-scale=1.0"))

    ///<summary>Converts this instance to a <c>meta</c> element <see cref="Node" />.</summary>
    member this.ToMetaElement =
        match this with
        | HtmlCharSet -> meta { attr.charset "UTF-8" }
        | HttpEquivalent (n, v) -> meta { attr.httpEquiv n; attr.value v }
        | MetaNameAndContent (n, v) -> meta { attr.name n; attr.value v }

    ///<summary>Unwraps any <c>string * string</c> name-value pair of this instance.</summary>
    member this.ToNameValuePair =
        match this with
        | HtmlCharSet -> None
        | HttpEquivalent (n, v) | MetaNameAndContent (n, v) -> (n, v) |> Some

///<summary>
/// Defines selected HTML <c>link[rel]</c> values.
///</summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Link_types
/// </remarks>
type HtmlLinkedDocumentRelationship =
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelAlternate
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelAuthor
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelBookmark
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelCanonical
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelExternal
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelHelp
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelIcon
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelLicense
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelMe
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelModulePreload
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelNext
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelNoFollow
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelNoOpener
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelNoReferrer
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelPingback
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelPrefetch
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelPreload
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelPrevious
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelSearch
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelShortLink
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelStylesheet
    ///<summary> a HTML <c>link[rel]</c> value </summary>
    | RelTag

    ///<summary>Returns the <see cref="string" /> representation of the linked-document relationship.</summary>
    member this.Value = this.ToString().Substring(3).ToLowerInvariant()

///<summary>
/// Defines selected HTML <c>link[as]</c> values.
///</summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/link#attr-as
/// </remarks>
type HtmlPrefetchOrPreLoadType =
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchAudio
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchDocument
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchEmbed
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchFetch
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchFont
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchImage
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchObject
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchScript
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchStyle
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchTrack
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchVideo
    ///<summary> a HTML <c>link[as]</c> prefetch value </summary>
    | PrefetchWorker
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadAudio
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadDocument
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadEmbed
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadFetch
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadFont
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadImage
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadObject
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadScript
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadStyle
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadTrack
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadVideo
    ///<summary> a HTML <c>link[as]</c> preload value </summary>
    | PreloadWorker

    ///<summary>Returns the <see cref="string" /> representation of the linked-document <c>as</c> type.</summary>
    member this.Value =
        let prefetch = "prefetch"
        let preload = "preload"
        let s = this.ToString().ToLowerInvariant()
        if s.StartsWith(prefetch) then s.Replace(prefetch, String.Empty)
        else s.Replace(preload, String.Empty)

///<summary>
/// Defines a type representing an <see cref="Node" />
/// or a type representing the non-presence of <see cref="Node" />.
/// </summary>
/// <remarks>
/// There is no list of <see cref="Node" /> compliment of this type
/// because <see cref="concat" /> can express multiple nodes as one.
/// </remarks>
type HtmlNodeOrEmpty =
    ///<summary> the non-presence of <see cref="Node" /> </summary>
    | NoNode
    ///<summary> the presence of <see cref="Node" /> </summary>
    | HasNode of Node

    ///<summary>Returns <see cref="Node" /> or <see cref="empty" />.</summary>
    member this.Value = match this with | NoNode -> empty() | HasNode node -> node

///<summary>
/// Enumerates the names of “where to display the linked URL”
/// for the <c>target</c> attribute of the anchor element (<c>a</c>)
/// or to not specify this.
/// </summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/a#attr-target
/// </remarks>
type HtmlTargetOrEmpty =
    ///<summary> do not specify where to display the linked URL </summary>
    | DoNotSpecifyTarget
    ///<summary> the <c>_self</c> target </summary>
    | TargetSelf
    ///<summary> the <c>_blank</c> target </summary>
    | TargetBlank
    ///<summary> the <c>_parent</c> target </summary>
    | TargetParent
    ///<summary> the <c>_top</c> target </summary>
    | TargetTop

    ///<summary>Returns the <see cref="string" /> representation of the target.</summary>
    member this.Value =
        match this with
        | DoNotSpecifyTarget -> attr.empty()
        | _ -> this.ToString().ToLowerInvariant().Replace("target", String.Empty) |> fun s -> attr.target $"_{s}"
