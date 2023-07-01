namespace Songhay.StudioFloor.Client.Components

open System.Threading.Tasks
open Microsoft.JSInterop

open Bolero
open Bolero.Html
open Elmish

open Songhay.Modules.Bolero.JsRuntimeUtility
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

open Songhay.Modules.Models
open Songhay.StudioFloor.Client.Models

type BoleroJsRuntimeElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    let demoCssVariableHtmlRef = HtmlRef()

    let demoCssVariableBlock (model: StudioFloorModel) (_: Dispatch<StudioFloorMessage>) =
        let colorAzure = "azure"
        let colorYellow = "yellow"
        let cssVariable = CssVariable.fromInput "main-bg-color"
        let cssVariableAndValue = CssVariableAndValue (cssVariable, CssValue colorAzure)
        let styleList =
            [
                cssVariableAndValue.toCssDeclaration
                $"background-color: {cssVariable.toCssPropertyValue};"
            ]

        div {
            [ p (All, L4); m (All, L4); elementTextAlign AlignCentered ] |> CssClasses.toHtmlClassFromList
            attr.style (styleList |> List.reduce (fun a i -> $"{a}{i}"))
            attr.ref demoCssVariableHtmlRef
            div {
                [ notification; ColorPrimary.CssClass; DisplayInlineBlock.CssClass ] |> CssClasses.toHtmlClassFromList
                div {
                    content |> CssClasses.toHtmlClass
                    rawHtml @"
                        <p>Click the button to demonstrate:</p>
                        <ol class=""is-lower-roman"">
                            <li>a call <code>getComputedStylePropertyValueAsync</code> to get the current color</li>
                            <li>a get the next color and call <code>consoleInfoAsync</code> to log the current and next color</li>
                            <li>a call <code>setComputedStylePropertyValueAsync</code> to set the background color</li>
                        </ol>
                    "
                }
            }
            button {
                [
                    buttonClass
                    "is-ghost"
                    "is-large"
                    DisplayInlineBlock.CssClass
                    m (All, L4)
                ] |> CssClasses.toHtmlClassFromList
                on.async.click (fun _ ->
                    async {
                        let! currentColor =
                            model.blazorServices.jsRuntime
                            |> getComputedStylePropertyValueAsync demoCssVariableHtmlRef cssVariable.Value
                            |> Async.AwaitTask

                        let nextColor = if currentColor = colorAzure then colorYellow else colorAzure

                        model.blazorServices.jsRuntime
                        |> consoleInfoAsync [| $"{nameof currentColor}: {currentColor}"; $"{nameof nextColor}: {nextColor}" |]
                        |> ignore

                        model.blazorServices.jsRuntime
                        |> setComputedStylePropertyValueAsync demoCssVariableHtmlRef cssVariable.Value nextColor
                        |> ignore
                    }
                )

                text "set computed style with CSS variable"
            }
        }

    let demoWindowAnimationBlock (comp: BoleroJsRuntimeElmishComponent) (model: StudioFloorModel) (_: Dispatch<StudioFloorMessage>) =

        div {
            [ p (All, L4); m (All, L4); elementTextAlign AlignCentered ] |> CssClasses.toHtmlClassFromList
            div {
                [ notification; ColorPrimary.CssClass; DisplayInlineBlock.CssClass ] |> CssClasses.toHtmlClassFromList
                div {
                    content |> CssClasses.toHtmlClass
                    rawHtml @"
                        <p>Click the button to demonstrate:</p>
                        <ol class=""is-lower-roman"">
                            <li></li>
                        </ol>
                    "
                }
            }
            button {
                [
                    buttonClass
                    "is-ghost"
                    "is-large"
                    DisplayInlineBlock.CssClass
                    m (All, L4)
                ] |> CssClasses.toHtmlClassFromList
                on.click (fun _ ->
                    let dotNetObjectReference = DotNetObjectReference.Create(comp)
                    let qualifiedName = $"{rx}.StudioFloorUtility.runMyAnimation"
                    model.blazorServices
                        .jsRuntime.InvokeVoidAsync(qualifiedName, dotNetObjectReference).AsTask() |> ignore
                )

                text "start animation"
            }
        }

    static member EComp model dispatch =
        ecomp<BoleroJsRuntimeElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.tab <> newModel.tab
        || oldModel.progressValue <> newModel.progressValue

    override this.View model dispatch =
        bulmaSection
            NoCssClasses
            NoAttr
            (concat {
                h1 {
                    title DefaultBulmaFontSize @ [ ColorPrimary.TextCssClass ] |> CssClasses.toHtmlClassFromList
                    text "the "; code { text "JsRuntimeUtility" }; text " module"
                }

                h2 {
                    (subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ] |> CssClasses.toHtmlClassFromList
                    text "changing a CSS variable (custom property)"
                }
                demoCssVariableBlock model dispatch

                h2 {
                    (subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ] |> CssClasses.toHtmlClassFromList
                    text "the JavaScript "; code { text "WindowAnimation" }; text " class"
                }
                demoWindowAnimationBlock this model dispatch
            })

    [<JSInvokable>]
    member this.invokeAsync() =
        this.Dispatch NextProgress
        Task.FromResult this.Model.progressValue
