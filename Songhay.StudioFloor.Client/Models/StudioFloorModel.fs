namespace Songhay.StudioFloor.Client.Models

open System

open Songhay.Modules.Models

type StudioFloorModel =
    {
        page: StudioFloorPage
        readMeData: string option
        visualStates: AppStateSet<StudioFloorVisualState>
    }

    static member initialize (serviceProvider: IServiceProvider) =
        Songhay.Modules.Bolero.ServiceProviderUtility.setBlazorServiceProvider serviceProvider
        {
            page = ReadMePage
            readMeData = None
            visualStates = AppStateSet.initialize
                .addState(ClipboardData "Enter any text you want here or just copy this sentence to the clipboard.")
                .addState(ProgressValue 1)
        }

    member private this.getVisualState (getter: StudioFloorVisualState -> 'o option) =
        this.visualStates.states
        |> List.ofSeq
        |> List.choose getter
        |> List.head

    member this.getClipboardData() = this.getVisualState(function ClipboardData s -> Some s | _ -> None)

    member this.getProgressValue() = this.getVisualState(function ProgressValue n -> Some n | _ -> None)

    member this.iterateProgressValue() =
        let currentScalar = this.getProgressValue()
        this.visualStates.removeState(ProgressValue currentScalar).addState(ProgressValue <| currentScalar + 1)

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
