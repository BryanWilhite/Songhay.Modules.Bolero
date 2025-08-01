namespace Songhay.StudioFloor.Client.Components

open Microsoft.AspNetCore.Components
open Microsoft.JSInterop
open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.JsRuntimeUtility
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma.Component
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass

open Songhay.StudioFloor.Client.Models

type BoleroJsRuntimeCssCustomPropertyElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    let demoCssCustomPropertyHtmlRef = HtmlRef()

    [<Inject>]
    member val private jsRuntime = Unchecked.defaultof<IJSRuntime> with get, set

    static member EComp model dispatch =
        ecomp<BoleroJsRuntimeCssCustomPropertyElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) = oldModel.visualStates <> newModel.visualStates

    override this.View _ _ =
        let colorAzure = "azure"
        let colorYellow = "yellow"
        let cssVariable = CssCustomProperty.fromInput "main-bg-color"
        let cssVariableAndValue = CssCustomPropertyAndValue (cssVariable, CssValue colorAzure)
        let styleList =
            [
                cssVariableAndValue.toCssDeclaration
                $"background-color: {cssVariable.toCssPropertyValue};"
            ]

        div {
            [ p (All, L4); m (All, L4); elementTextAlign AlignCentered ] |> CssClasses.toHtmlClassFromList
            attr.style (styleList |> List.reduce (fun a i -> $"{a}{i}"))
            attr.ref demoCssCustomPropertyHtmlRef
            bulmaMessage
                (HasClasses CssClasses[ message; ColorPrimary.CssClass; DisplayInlineBlock.CssClass ])
                (para { text "Click the button to demonstrate:" })
                (
                    [
                        li { text "a call "; code { text "getComputedStylePropertyValueAsync" }; text " to get the current color" }
                        li { text "a get the next color and call "; code { text "consoleInfoAsync" }; text " to log the current and next color" }
                        li { text "a call "; code { text "setComputedStylePropertyValueAsync" }; text " to set the background color" }
                    ]
                    |> orderedList (HasClasses <| CssClasses [ LowerRoman.CssClass; elementTextAlign AlignLeft ])
                )
            buttonElement
                (HasClasses <| CssClasses [ buttonClass; ColorGhost.CssClass; BulmaElementLarge.CssClass; DisplayInlineBlock.CssClass; m (All, L4)])
                (HasAttr <|
                    attrs {
                        on.async.click (fun _ ->
                            async {
                                let! currentColor =
                                    this.jsRuntime
                                    |> getComputedStylePropertyValueAsync demoCssCustomPropertyHtmlRef cssVariable.Value
                                    |> Async.AwaitTask

                                let nextColor = if currentColor = colorAzure then colorYellow else colorAzure

                                this.jsRuntime
                                |> consoleInfoAsync [| $"{nameof currentColor}: {currentColor}"; $"{nameof nextColor}: {nextColor}" |]
                                |> ignore

                                this.jsRuntime
                                |> setComputedStylePropertyValueAsync demoCssCustomPropertyHtmlRef cssVariable.Value nextColor
                                |> ignore
                            }
                        )
                    })
                (text "set computed style with CSS variable 🐎💄")
        }
