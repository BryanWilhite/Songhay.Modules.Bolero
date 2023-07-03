namespace Songhay.StudioFloor.Client.Components

open System.Threading.Tasks
open Microsoft.JSInterop

open Bolero
open Bolero.Html
open Elmish

open Songhay.Modules.Bolero.JsRuntimeUtility
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma.Component
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
            [ p (All, L4); m (All, L4); elementTextAlign AlignCentered; box ] |> CssClasses.toHtmlClassFromList
            attr.style (styleList |> List.reduce (fun a i -> $"{a}{i}"))
            attr.ref demoCssVariableHtmlRef
            bulmaMessage
                (HasClasses CssClasses[ message; ColorPrimary.CssClass; DisplayInlineBlock.CssClass ])
                (Html.p { text "Click the button to demonstrate:" })
                (
                    [
                        li { text "a call "; code { text "getComputedStylePropertyValueAsync" }; text " to get the current color" }
                        li { text "a get the next color and call "; code { text "consoleInfoAsync" }; text " to log the current and next color" }
                        li { text "a call "; code { text "setComputedStylePropertyValueAsync" }; text " to set the background color" }
                    ]
                    |> orderedList (HasClasses <| CssClasses [ "is-lower-roman"; elementTextAlign AlignLeft ])
                )
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
            [ p (All, L4); m (All, L4); elementTextAlign AlignCentered; ColorLight.BackgroundCssClass; box ] |> CssClasses.toHtmlClassFromList
            bulmaMessage
                (HasClasses CssClasses[ message; ColorPrimary.CssClass; DisplayInlineBlock.CssClass ])
                ( Html.p { text "Click the button to demonstrate:" })
                (
                    [
                        li { text "calling the JavaScript that will start a browser-window-level animation callback loop" }
                        li { text "that the callback will call a .NET method that returns data to the loop" }
                        li { text "that the loop will use that data to continue or stop the loop" }
                    ]
                    |> orderedList (HasClasses <| CssClasses [ "is-lower-roman"; elementTextAlign AlignLeft ])
                )
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
            progress {
                [ "progress"; "is-large" ] |> CssClasses.toHtmlClassFromList
                attr.value model.progressValue
                attr.max 100
                text $"{model.progressValue}%%"
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
                    title DefaultBulmaFontSize @ [ ColorPrimary.TextCssClass ]
                    |> CssClasses.toHtmlClassFromList
                    text "the "; code { text "JsRuntimeUtility" }; text " module"
                }

                details {
                    (subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1);] |> CssClasses.toHtmlClassFromList
                    summary {
                        elementIsClickable |> CssClasses.toHtmlClass
                        text "changing a CSS variable (custom property)"
                    }
                    demoCssVariableBlock model dispatch
                }

                details {
                    (subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1);] |> CssClasses.toHtmlClassFromList
                    summary {
                        elementIsClickable |> CssClasses.toHtmlClass
                        text "the JavaScript "; code { text "WindowAnimation" }; text " class"
                    }
                    demoWindowAnimationBlock this model dispatch
                }
            })

    [<JSInvokable>]
    member this.invokeAsync() =
        this.Dispatch NextProgress
        Task.FromResult this.Model.progressValue
