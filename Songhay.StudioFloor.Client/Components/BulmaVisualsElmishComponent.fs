namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.Component
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout
open Songhay.Modules.Bolero.Components

open Songhay.StudioFloor.Client.Models

type BulmaVisualsElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<BulmaVisualsElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.tab <> newModel.tab

    override this.View _ _ =
        bulmaSection
            NoCssClasses
            NoAttr
            (concat {
                h1 {
                    title DefaultBulmaFontSize @ [ ColorPrimary.TextCssClass ]
                    |> CssClasses.toHtmlClassFromList
                    text "Bulma visuals"
                }

                bulmaDetailsElement
                    (HasClasses <| CssClasses ((subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ]))
                    (text "Bulma columns")
                    BulmaColumnsComponent.BComp

                bulmaDetailsElement
                    (HasClasses <| CssClasses ((subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ]))
                    (text "SVG")
                    SvgComponent.BComp

                bulmaDetailsElement
                    (HasClasses <| CssClasses ((subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ]))
                    (text "Bulma dropdown")
                    (bulmaDropdown
                        true
                        "Content"
                        (fun _ -> ())
                        (concat {
                            bulmaDropdownItem
                                false
                                (fun _ -> ())
                                "Item 1"
                            bulmaDropdownDivider()
                            bulmaDropdownItem
                                false
                                (fun _ -> ())
                                "Item 2"
                            bulmaDropdownDivider()
                            bulmaDropdownItem
                                false
                                (fun _ -> ())
                                "Item 3"
                        })
                    )

                bulmaDetailsElement
                    (HasClasses <| CssClasses ((subtitle DefaultBulmaFontSize) @ [ ColorPrimary.TextCssClass; m (T, L1) ]))
                    (text $"{nameof AppVersionsComponent}")
                    (bulmaColumnsContainer
                        NoCssClasses
                        (concat {
                            bulmaColumn
                                (HasClasses <| CssClasses [ HSize4.CssClass; elementTextAlign AlignCentered ])
                                (empty())
                            bulmaColumn
                                (HasClasses <| CssClasses [ HSize4.CssClass ])
                                AppVersionsComponent.BComp
                            bulmaColumn
                                (HasClasses <| CssClasses [ HSize4.CssClass; elementTextAlign AlignCentered ])
                                (empty())
                        }))
            })
