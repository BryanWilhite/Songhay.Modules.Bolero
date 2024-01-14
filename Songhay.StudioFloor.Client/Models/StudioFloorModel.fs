namespace Songhay.StudioFloor.Client.Models

open System.Net.Http

open Microsoft.AspNetCore.Components
open Microsoft.JSInterop
open Songhay.Modules.Models

type StudioFloorModel =
    {
        blazorServices: {| httpClient: HttpClient; jsRuntime: IJSRuntime; navigationManager: NavigationManager |}
        tab: StudioFloorTab
        readMeData: string option
        visualStates: AppStateSet<StudioFloorVisualState>
    }

    static member initialize (httpClient: HttpClient) (jsRuntime: IJSRuntime) (navigationManager: NavigationManager) =
        {
            blazorServices = {| httpClient = httpClient; jsRuntime = jsRuntime; navigationManager = navigationManager |}
            tab = ReadMeTab
            readMeData = None
            visualStates = AppStateSet.initialize
                                     .addState(ProgressValue 1)
                                     .addState(ClipboardData "Enter any text you want here or just copy this sentence to the clipboard.")
        }

    member private this.getVisualState (getter: StudioFloorVisualState -> 'o) =
        this.visualStates.states
        |> Set.map(getter)
        |> Set.toArray |> Array.head

    member this.getClipboardData() = this.getVisualState (fun i -> match i with | ClipboardData s -> s | _ -> "[!empty]" )

    member this.getProgressValue() = this.getVisualState(fun i -> match i with | ProgressValue n -> n | _ -> 1 )

    member this.iterateProgressValue() =
        let currentScalar = this.getProgressValue()
        let nextValue = ProgressValue <| currentScalar + 1
        this.visualStates.removeState(ProgressValue currentScalar).addState(nextValue)

    member this.setClipboardData data =
        let currentData = this.getClipboardData()
        let nextData = ClipboardData data
        this.visualStates.removeState(ClipboardData currentData).addState(nextData)

    member this.toggleDropDownItemState n =
        this.visualStates
            .removeStates(
                [| DropDownItem 1; DropDownItem 2; DropDownItem 3 |] |> Array.except [| DropDownItem n |]
            )
            .toggleState(DropDownItem n)
