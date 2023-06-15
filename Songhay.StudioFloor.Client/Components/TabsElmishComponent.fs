namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

open Songhay.StudioFloor.Client.Models

type TabsElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<TabsElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.tab <> newModel.tab
        || oldModel.readMeData <> newModel.readMeData

    override this.View model dispatch =
        let tabs = [
            ("README", ReadMeTab)
            ("Bolero `IJsRuntime`", BoleroJsRuntimeTab)
            ("Bulma: Columns", BulmaColumnsTab)
            ("SVG", SvgTab)
        ]

        concat {
            div {
                [
                    "tabs";
                    "has-background-grey-light";
                    "is-toggle";
                    "is-fullwidth";
                    "is-large"
                ] |> CssClasses.toHtmlClassFromList

                ul {
                    forEach tabs <| fun (label, pg) ->
                    li {
                        a {
                            attr.href "#"
                            DomElementEvent.Click.PreventDefault
                            on.click (fun _ -> SetTab pg |> dispatch)
                            text label
                        }
                    }
                }
            }

            cond model.tab <| function
            | ReadMeTab ->
                if model.readMeData.IsNone then
                    text "loading…"
                else
                    bulmaContainer
                        ContainerWidthFluid
                        NoCssClasses
                        (bulmaNotification
                            (HasClasses <| CssClasses [ "is-info" ])
                            (rawHtml model.readMeData.Value))
            | BoleroJsRuntimeTab
            | BulmaColumnsTab
            | SvgTab ->
                bulmaContainer
                    ContainerWidthFluid
                    NoCssClasses
                    (empty())
        }
