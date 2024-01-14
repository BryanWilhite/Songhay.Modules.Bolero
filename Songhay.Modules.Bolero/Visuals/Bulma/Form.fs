namespace Songhay.Modules.Bolero.Visuals.Bulma

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma

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
            CssClasses [ CssClass.field ] |> cssClasses.ToHtmlClassAttribute

            childNode
        }

    /// <summary>
    /// “…<c>field-body</c> for the input/select/textarea container …”
    /// to be used with <see cref="bulmaField"/> parent having <c>fieldIsHorizontal</c>
    /// </summary>
    /// <remarks>
    /// 📖 https://bulma.io/documentation/form/general/#horizontal-form
    /// </remarks>
    let bulmaFieldBodyContainer (cssClasses: CssClassesOrEmpty) (childNode: Node) =
        div {
            CssClasses [ CssClass.fieldBody ] |> cssClasses.ToHtmlClassAttribute

            childNode
        }

    /// <summary>
    /// “…<c>field-label</c> for the side label …”
    /// to be used with <see cref="bulmaField"/> parent having <c>fieldIsHorizontal</c>
    /// </summary>
    /// <remarks>
    /// This is the container for displaying a label on the “side” for horizontal layout.
    /// A child node should include the <see cref="bulmaLabel"/>.
    ///
    /// 📖 https://bulma.io/documentation/form/general/#horizontal-form
    /// </remarks>
    let bulmaFieldLabelContainer (cssClasses: CssClassesOrEmpty) (childNode: Node) =
        div {
            CssClasses [ CssClass.fieldLabel ] |> cssClasses.ToHtmlClassAttribute

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
            CssClasses [ CssClass.label ] |> cssClasses.ToHtmlClassAttribute
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
            CssClasses [ CssClass.control ] |> cssClasses.ToHtmlClassAttribute

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
            CssClasses [ CssClass.input ] |> cssClasses.ToHtmlClassAttribute
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
            CssClasses [ CssClass.select ] |> cssClasses.ToHtmlClassAttribute
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
            CssClasses [ CssClass.textarea ] |> cssClasses.ToHtmlClassAttribute
            attr.Value

            childNode
        }
