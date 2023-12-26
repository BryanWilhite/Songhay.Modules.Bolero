namespace Songhay.StudioFloor.Client.Components

open Microsoft.JSInterop
open System.Threading.Tasks

open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.JsRuntimeUtility
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
open Songhay.Modules.Bolero.Visuals.Bulma.Component
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Element

open Songhay.StudioFloor.Client.Models

type BoleroJsRuntimeWindowAnimationComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<BoleroJsRuntimeWindowAnimationComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) =
        oldModel.tab <> newModel.tab
        || oldModel.bulmaVisualsStates.states <> newModel.bulmaVisualsStates.states

    override this.View model _ =
        div {
            [ p (All, L4); m (All, L4); elementTextAlign AlignCentered; ColorEmpty.BackgroundCssClassLight; box ] |> CssClasses.toHtmlClassFromList
            bulmaMessage
                (HasClasses CssClasses[ message; ColorPrimary.CssClass; DisplayInlineBlock.CssClass ])
                ( Html.p { text "Click the button to demonstrate:" })
                (
                    [
                        li { text "calling the JavaScript that will start a browser-window-level animation callback loop" }
                        li { text "that the callback will call a .NET method that returns data to the loop" }
                        li { text "that the loop will use that data to continue or stop the loop" }
                    ]
                    |> orderedList (HasClasses <| CssClasses [ LowerRoman.CssClass; elementTextAlign AlignLeft ])
                )
            buttonElementAsync
                (HasClasses <| CssClasses [ buttonClass; ColorGhost.CssClass; BulmaElementLarge.CssClass; DisplayInlineBlock.CssClass; m (All, L4)])
                NoAttr
                (fun _ ->
                    let dotNetObjectReference = DotNetObjectReference.Create(this)
                    let qualifiedName = $"{rx}.StudioFloorUtility.runMyAnimation"
                    model.blazorServices
                        .jsRuntime.InvokeVoidAsync(qualifiedName, dotNetObjectReference)
                        .AsTask()
                    |> Async.AwaitTask
                )
                (text "start animation")
            bulmaProgressElement
                (HasClasses <| CssClasses [ SizeLarge.CssClass ])
                (model.getProgressValue(), 100)
                (text $"{model.getProgressValue()}%%")
        }

    [<JSInvokable>]
    member this.invokeAsync() =
        this.Dispatch NextProgress
        Task.FromResult <| this.Model.getProgressValue()
