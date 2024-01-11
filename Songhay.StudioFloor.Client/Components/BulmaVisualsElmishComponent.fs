namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Components
open Songhay.Modules.Bolero.Models

open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma.Component
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

open Songhay.StudioFloor.Client.Models

type BulmaVisualsElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<BulmaVisualsElmishComponent, _, _> model dispatch { attr.empty() }

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
                    text "Bulma visuals and components"
                }
                let getArticleTile (titleNode: Node) (childNode: Node) =
                    bulmaTile
                        HSizeAuto
                        (HasClasses <| CssClasses [tileIsParent])
                        (article {
                            [ tile; tileIsChild; box ] |> CssClasses.toHtmlClassFromList

                            paragraphElement
                                (HasClasses <| CssClasses (title DefaultBulmaFontSize))
                                NoAttr
                                titleNode

                            childNode
                        })

                bulmaTile
                    HSizeAuto
                    (HasClasses <| CssClasses [tileIsAncestor])
                    (concat {
                        getArticleTile
                            (text "Bulma columns")
                            BulmaColumnsComponent.BComp

                        getArticleTile
                            (text "SVG")
                            SvgComponent.BComp

                        getArticleTile
                            (text "Bulma dropdown")
                            (bulmaDropdown
                                (model.bulmaVisualsStates.hasState DropDownContentActive)
                                "Content"
                                (fun _ -> dispatch <| ToggleBulmaVisualsState DropDownContentActive)
                                (concat {
                                    bulmaDropdownItem
                                        (model.bulmaVisualsStates.hasState (DropDownItem 1))
                                        (fun _ -> dispatch <| ToggleBulmaVisualsState (DropDownItem 1))
                                        "Item 1"
                                    bulmaDropdownDivider()
                                    bulmaDropdownItem
                                        (model.bulmaVisualsStates.hasState (DropDownItem 2))
                                        (fun _ -> dispatch <| ToggleBulmaVisualsState (DropDownItem 2))
                                        "Item 2"
                                    bulmaDropdownDivider()
                                    bulmaDropdownItem
                                        (model.bulmaVisualsStates.hasState (DropDownItem 3))
                                        (fun _ -> dispatch <| ToggleBulmaVisualsState (DropDownItem 3))
                                        "Item 3"
                                }))
                    })

                bulmaTile
                    HSizeAuto
                    (HasClasses <| CssClasses [tileIsAncestor])
                    (concat {

                        getArticleTile
                            (text $"{nameof AppVersionsComponent}")
                            AppVersionsComponent.BComp

                        getArticleTile
                            (text System.String.Empty)
                            (empty())

                        getArticleTile
                            (text System.String.Empty)
                            (empty())
                    })
            })
