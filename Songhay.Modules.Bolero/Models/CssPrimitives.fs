namespace Songhay.Modules.Bolero.Models

open Bolero.Html

///<summary>
/// Identifies a list of <see cref="string" /> as a collection of CSS class names.
///</summary>
/// <remarks>
/// The original inspiration for this type is based on
/// the need to avoid writing <c>attr.``class``</c> more than once. 
/// </remarks>
type CssClasses =
    //<summary> a list of <see cref="string" /> as a collection of CSS class names </summary>
    | CssClasses of string list

    //<summary> converts a <see cref="string" /> into a CSS <c>class</c> <see cref="Attr" /> </summary>
    static member toHtmlClass (s: string) = (CssClasses [s]).ToHtmlClassAttribute

    //<summary> converts a list of <see cref="string" /> into a CSS <c>class</c> <see cref="Attr" /> </summary>
    static member toHtmlClassFromList (list: string list) = (CssClasses list).ToHtmlClassAttribute

    //<summary> returns the underlying list of <see cref="string" /> </summary>
    member this.Value = let (CssClasses l) = this in l

    //<summary> appends the specified <see cref="string" /> to this instance </summary>
    member this.Append s = CssClasses (List.append this.Value [s])

    //<summary> appends the specified list of <see cref="string" /> to this instance </summary>
    member this.AppendList (l: string list) = CssClasses (List.append this.Value l)

    //<summary> pre-pends the specified <see cref="string" /> to this instance </summary>
    member this.Prepend s = CssClasses (List.append [s] this.Value)

    //<summary> pre-pends the specified list of <see cref="string" /> to this instance </summary>
    member this.PrependList (l: string list) = CssClasses (List.append l this.Value)

    //<summary> reduces the underlying list of <see cref="string" /> the the value of the <c>class</c> attribute </summary>
    member this.ToAttributeValue = this.Value |> List.reduce(fun a b -> $"{a} {b}")

    //<summary> calls <see cref="ToAttributeValue" /> and return the result of <c>attr.``class``</c> </summary>
    member this.ToHtmlClassAttribute = attr.``class`` this.ToAttributeValue

///<summary>
/// Represents <see cref="CssClasses" /> or emptiness.
/// </summary>
type CssClassesOrEmpty =
    | NoCssClasses
    | HasClasses of CssClasses

    ///<summary>
    /// Appends or passes through the specified <see cref="CssClasses" />
    /// and calls <see cref="CssClasses.ToHtmlClassAttribute" />.
    /// </summary>
    member this.ToHtmlClassAttribute (cssClasses: CssClasses) =
        match this with
        | HasClasses moreCssClasses -> (moreCssClasses.Value |> cssClasses.AppendList).ToHtmlClassAttribute
        | NoCssClasses -> cssClasses.ToHtmlClassAttribute

    ///<summary>
    /// Calls <see cref="CssClasses.ToHtmlClassAttribute" />
    /// or returns an empty <see cref="Attr" />,
    /// matching the underlying union type.
    /// </summary>
    member this.Value = match this with | NoCssClasses -> attr.empty() | HasClasses classes -> classes.ToHtmlClassAttribute
