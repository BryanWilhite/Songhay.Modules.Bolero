namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.JsRuntimeUtility
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Component


open Songhay.StudioFloor.Client.Models

type BoleroJsRuntimeClipboardApi() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<BoleroJsRuntimeClipboardApi, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.tab <> newModel.tab
        || oldModel.bulmaVisualsStates.states <> newModel.bulmaVisualsStates.states

    override this.View model dispatch =
        div {
            [ p (All, L4); m (All, L4); elementTextAlign AlignCentered; ColorEmpty.BackgroundCssClassLight; box ] |> CssClasses.toHtmlClassFromList
            bulmaMessage
                (HasClasses CssClasses[ message; ColorPrimary.CssClass; DisplayInlineBlock.CssClass ])
                ( Html.p { text "Click the button to demonstrate:" })
                (concat {
                    buttonElementAsync
                        (HasClasses <| CssClasses [ buttonClass; ColorGhost.CssClass; BulmaElementLarge.CssClass; DisplayInlineBlock.CssClass; m (All, L4)])
                        NoAttr
                        (fun _ ->
                            model.blazorServices.jsRuntime
                            |> copyToClipboard "Click the button to demonstrate:"
                            |> Async.AwaitTask
                        )
                        (text "copy to clipboard 📋")
                })
        }