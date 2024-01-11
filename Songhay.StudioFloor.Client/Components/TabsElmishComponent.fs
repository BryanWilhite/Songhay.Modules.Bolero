namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma
open Songhay.Modules.Bolero.Visuals.Bulma.Component
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

open Songhay.Modules.Models
open Songhay.StudioFloor.Client.Models

type TabsElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<TabsElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.tab <> newModel.tab
        || oldModel.readMeData <> newModel.readMeData
        || oldModel.bulmaVisualsStates.states <> newModel.bulmaVisualsStates.states

    override this.View model dispatch =
        concat {
            let anchor tab (node: Node) =
                anchorButtonElement
                    NoCssClasses
                    (HasAttr <| on.click (fun _ -> SetTab tab |> dispatch))
                    node

            let tabs =
                [
                    (text "README", ReadMeTab)
                    (concat { text "Bolero "; code { text "IJsRuntime" } }, BoleroJsRuntimeTab)
                    (text "Bulma Visuals", BulmaVisualsTab)
                ]
                |> List.map (fun (node, tab) -> anchor tab node, tab)

            bulmaTabs
                (HasClasses <| CssClasses [
                    ColorEmpty.BackgroundCssClassLight
                    CssClass.tabsElementIsToggle
                    CssClass.elementIsFullWidth
                    SizeLarge.CssClass
                ])
                (fun pg -> model.tab = pg)
                tabs

            cond model.tab <| function
            | ReadMeTab ->
                if model.readMeData.IsNone then
                    bulmaLoader
                        (HasClasses <| CssClasses [ DisplayInlineBlock.CssClass; CssClass.p (All, L6); CssClass.m (All, L6) ])
                else
                    (bulmaNotification
                        (HasClasses <| CssClasses [ ColorPrimary.CssClass ])
                        (rawHtml model.readMeData.Value))

            | BoleroJsRuntimeTab -> BoleroJsRuntimeElmishComponent.EComp model dispatch

            | BulmaVisualsTab -> BulmaVisualsElmishComponent.EComp model dispatch
        }
