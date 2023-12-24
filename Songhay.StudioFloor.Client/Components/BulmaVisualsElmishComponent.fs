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

    override this.View _ _ =
        bulmaSection
            NoCssClasses
            NoAttr
            (concat {
                h1 {
                    title DefaultBulmaFontSize @ [ ColorPrimary.TextCssClass ]
                    |> CssClasses.toHtmlClassFromList
                    text "Bulma visuals and components"
                }
                bulmaTile
                    HSizeAuto
                    (HasClasses <| CssClasses [tileIsAncestor])
                    (concat {
                        bulmaTile
                            HSizeAuto
                            (HasClasses <| CssClasses [tileIsParent])
                            (article {
                                [ tile; tileIsChild; box ] |> CssClasses.toHtmlClassFromList
                                paragraphElement
                                    (HasClasses <| CssClasses (title DefaultBulmaFontSize))
                                    NoAttr
                                    (text "Bulma columns")
                                BulmaColumnsComponent.BComp
                            })
                        bulmaTile
                            HSizeAuto
                            (HasClasses <| CssClasses [tileIsParent])
                            (article {
                                [ tile; tileIsChild; box ] |> CssClasses.toHtmlClassFromList
                                paragraphElement
                                    (HasClasses <| CssClasses (title DefaultBulmaFontSize))
                                    NoAttr
                                    (text "SVG")
                                SvgComponent.BComp
                            })
                        bulmaTile
                            HSizeAuto
                            (HasClasses <| CssClasses [tileIsParent])
                            (article {
                                [ tile; tileIsChild; box ] |> CssClasses.toHtmlClassFromList
                                paragraphElement
                                    (HasClasses <| CssClasses (title DefaultBulmaFontSize))
                                    NoAttr
                                    (text "Bulma dropdown")
                                bulmaDropdown
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
                            })
                    })
                bulmaTile
                    HSizeAuto
                    (HasClasses <| CssClasses [tileIsAncestor])
                    (concat {
                        bulmaTile
                            HSizeAuto
                            (HasClasses <| CssClasses [tileIsParent])
                            (article {
                                [ tile; tileIsChild; box ] |> CssClasses.toHtmlClassFromList
                                paragraphElement
                                    (HasClasses <| CssClasses (title DefaultBulmaFontSize))
                                    NoAttr
                                    (text $"{nameof AppVersionsComponent}")
                                AppVersionsComponent.BComp
                            })
                        bulmaTile
                            HSizeAuto
                            (HasClasses <| CssClasses [tileIsParent])
                            (article {
                                [ tile; tileIsChild; box ] |> CssClasses.toHtmlClassFromList
                                paragraphElement
                                    (HasClasses <| CssClasses (title DefaultBulmaFontSize))
                                    NoAttr
                                    (text "")
                            })
                        bulmaTile
                            HSizeAuto
                            (HasClasses <| CssClasses [tileIsParent])
                            (article {
                                [ tile; tileIsChild; box ] |> CssClasses.toHtmlClassFromList
                                paragraphElement
                                    (HasClasses <| CssClasses (title DefaultBulmaFontSize))
                                    NoAttr
                                    (text "")
                            })
                    })
            })
