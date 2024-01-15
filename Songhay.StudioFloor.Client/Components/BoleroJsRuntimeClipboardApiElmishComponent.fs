namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.JsRuntimeUtility
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Form

open Songhay.StudioFloor.Client.Models

type BoleroJsRuntimeClipboardApiElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<BoleroJsRuntimeClipboardApiElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) = oldModel.visualStates.states <> newModel.visualStates.states

    override this.View model dispatch =
        let buttonCaption = "copy Source to clipboard 📋"

        form {
            bulmaField
                (HasClasses <| CssClasses [ fieldIsHorizontal ])
                (concat {
                    bulmaFieldLabelContainer
                        (HasClasses <| CssClasses [ SizeNormal.CssClass ])
                        (bulmaLabel
                            NoCssClasses
                            NoAttr
                            (text "Source:")
                        )
                    bulmaFieldBodyContainer
                        NoCssClasses
                        (concat {
                            bulmaField
                                NoCssClasses
                                (bulmaControl
                                    NoCssClasses
                                    (bulmaTextarea
                                        NoCssClasses
                                        (HasAttr <|
                                            bind.input.string
                                                (model.getClipboardData())
                                                (fun s -> dispatch (ChangeVisualState <| ClipboardData s))
                                        )
                                        (text <| model.getClipboardData())
                                    )
                                )
                            bulmaField
                                NoCssClasses
                                (bulmaControl
                                    NoCssClasses
                                    (bulmaTextarea
                                        NoCssClasses
                                        (HasAttr <| attr.placeholder $"Press the `{buttonCaption}` button and paste here.")
                                        (text System.String.Empty)
                                    )
                                )
                        })
                })
            bulmaField
                (HasClasses <| CssClasses [ fieldIsHorizontal ])
                (buttonElement
                    (HasClasses <| CssClasses [ buttonClass; ColorGhost.CssClass; BulmaElementLarge.CssClass; DisplayInlineBlock.CssClass; m (All, L4)])
                    (HasAttr <|
                        attrs {
                            Button.ToAttr
                            on.async.click (fun _ ->
                                    model.blazorServices.jsRuntime
                                    |> copyToClipboard (model.getClipboardData())
                                    |> Async.AwaitTask
                                )
                        })
                    (text buttonCaption)
                )
        }
