namespace Songhay.Modules.Bolero.Visuals.Bulma

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models

///<summary>
/// Bulma Form controls
/// “All generic form controls, designed for consistency…”
/// — https://bulma.io/documentation/form/general/
///</summary>
module Form =

    /// <summary>
    /// “…use the <c>field</c> class as a container, to keep the spacing consistent…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/form/general/
    /// </remarks>
    let bulmaField (cssClasses: CssClassesOrEmpty) (childNode: Node) =
        div {
            CssClasses [ "field" ] |> cssClasses.ToHtmlClassAttribute

            childNode
        }

    /// <summary>
    /// “…a text <c>label</c>…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/form/general/
    /// </remarks>
    let bulmaLabel (cssClasses: CssClassesOrEmpty) (attr: HtmlAttributeOrEmpty) (childNode: Node) =
        label {
            CssClasses [ "label" ] |> cssClasses.ToHtmlClassAttribute
            attr.Value

            childNode
        }

    /// <summary>
    /// “…Bulma provides a very useful <c>control</c> container with which you can wrap the form controls…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/form/general/
    /// </remarks>
    let bulmaControl (cssClasses: CssClassesOrEmpty) (childNode: Node) =
        div {
            CssClasses [ "control" ] |> cssClasses.ToHtmlClassAttribute

            childNode
        }

    /// <summary>
    /// “The text input and its variations…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/form/input/
    /// </remarks>
    let bulmaInput (cssClasses: CssClassesOrEmpty) (attr: HtmlAttributeOrEmpty) =
        input {
            CssClasses [ "input" ] |> cssClasses.ToHtmlClassAttribute
            attr.Value
        }

    /// <summary>
    /// “The browser built-in <c>select</c> dropdown, styled accordingly…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/form/select/
    /// </remarks>
    let bulmaSelect (cssClasses: CssClassesOrEmpty) (attr: HtmlAttributeOrEmpty) (childNode: Node) =
        div {
            CssClasses [ "select" ] |> cssClasses.ToHtmlClassAttribute
            select {
                attr.Value

                childNode
            }
        }

    /// <summary>
    /// “The multiline textarea and its variations…”
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/form/textarea/
    /// </remarks>
    let bulmaTextarea (cssClasses: CssClassesOrEmpty) (attr: HtmlAttributeOrEmpty) (childNode: Node) =
        textarea {
            CssClasses [ "textarea" ] |> cssClasses.ToHtmlClassAttribute
            attr.Value

            childNode
        }
