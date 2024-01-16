namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

open Songhay.StudioFloor.Client.Models

type BoleroJsRuntimeElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<BoleroJsRuntimeElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.progressValue <> newModel.progressValue ||
        oldModel.visualStates.states <> newModel.visualStates.states

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

                let getTileContent (titleNode: Node) (contentNode: Node) =
                    bulmaTile
                        HSizeAuto
                        (HasClasses <| CssClasses [tileIsParent])
                        (bulmaTile
                            HSizeAuto
                            (HasClasses <| CssClasses [tileIsChild; box])
                            (concat {
                                paragraphElement
                                    (HasClasses <| CssClasses (title DefaultBulmaFontSize))
                                    NoAttr
                                    titleNode

                                contentNode
                            })
                        )

                bulmaTile
                    HSizeAuto
                    (HasClasses <| CssClasses [tileIsAncestor])
                    (concat {
                        getTileContent
                            (text "using the Clipboard API")
                            (BoleroJsRuntimeClipboardApiElmishComponent.EComp model dispatch)
                        getTileContent
                            (text "changing a CSS variable (custom property)")
                            (BoleroJsRuntimeCssCustomPropertyElmishComponent.EComp model dispatch)
                    })

                bulmaTile
                    HSizeAuto
                    (HasClasses <| CssClasses [tileIsAncestor])
                    (concat {
                        getTileContent
                            (concat { text "the JavaScript "; code { text "WindowAnimation" }; text " class" })
                            (BoleroJsRuntimeWindowAnimationComponent.EComp model dispatch)
                    })
            })
