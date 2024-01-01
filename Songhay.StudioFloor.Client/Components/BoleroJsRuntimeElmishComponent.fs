namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

open Songhay.StudioFloor.Client.Models

type BoleroJsRuntimeElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<BoleroJsRuntimeElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.tab <> newModel.tab
        || oldModel.bulmaVisualsStates.states <> newModel.bulmaVisualsStates.states

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

                bulmaDetailsElement
                    (HasClasses <| CssClasses ((subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ]))
                    (text "using the Clipboard API")
                    (BoleroJsRuntimeClipboardApiElmishComponent.EComp model dispatch)

                bulmaDetailsElement
                    (HasClasses <| CssClasses ((subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ]))
                    (text "changing a CSS variable (custom property)")
                    (BoleroJsRuntimeCssCustomPropertyElmishComponent.EComp model dispatch)

                bulmaDetailsElement
                    (HasClasses <| CssClasses ((subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ]))
                    (concat { text "the JavaScript "; code { text "WindowAnimation" }; text " class" })
                    (BoleroJsRuntimeWindowAnimationComponent.EComp model dispatch)
            })
