namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma
open Songhay.Modules.Bolero.Visuals.Bulma.Component

open Songhay.StudioFloor.Client
open Songhay.StudioFloor.Client.Models

type TabsElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<TabsElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.readMeData <> newModel.readMeData
        || oldModel.page <> newModel.page
        || oldModel.visualStates <> newModel.visualStates

    override this.View model dispatch =
        concat {

            let tabPairs =
                [
                    ( text "README", ReadMePage )
                    ( concat { text "Bolero "; code { text "IJsRuntime" } }, BoleroJsRuntimePage )
                    ( text "Bulma Visuals", BulmaVisualsPage )
                    ( text "Blazor Configuration", BlazorConfiguration )
                ]
                |> List.map (fun (n, page) ->
                        anchorElement NoCssClasses (HasAttr <| ElmishRoutes.router.HRef page) n, page
                )

            bulmaTabs
                (HasClasses <| CssClasses [
                    ColorEmpty.BackgroundCssClassLight
                    CssClass.tabsElementIsToggle
                    CssClass.elementIsFullWidth
                    SizeLarge.CssClass
                ])
                (fun page -> model.page = page)
                tabPairs

            cond model.page <| function
            | ReadMePage -> ReadMeElmishComponent.EComp model dispatch
            | BoleroJsRuntimePage -> BoleroJsRuntimeElmishComponent.EComp model dispatch
            | BulmaVisualsPage -> BulmaVisualsElmishComponent.EComp model dispatch
            | BlazorConfiguration -> AppSettingsComponent.BComp
        }
