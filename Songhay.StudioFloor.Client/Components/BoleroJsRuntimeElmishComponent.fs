namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

open Songhay.Modules.Models
open Songhay.StudioFloor.Client.Models

type BoleroJsRuntimeElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<BoleroJsRuntimeElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.tab <> newModel.tab

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
                    text "This utility meets the concern of Blazor-JavaScript interoperability."
                }
            })
