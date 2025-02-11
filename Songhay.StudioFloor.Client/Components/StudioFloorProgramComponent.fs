namespace Songhay.StudioFloor.Client.Components

open Elmish
open Bolero

open Songhay.StudioFloor.Client
open Songhay.StudioFloor.Client.Models

module pcu = ProgramComponentUtility

type StudioFloorProgramComponent() =
    inherit ProgramComponent<StudioFloorModel, StudioFloorMessage>()

    let update message model =

        match message with
        | Error _ ->
            model, Cmd.none
        | GetReadMe ->
            let cmd = pcu.getCommandForGetReadMe model
            model, cmd
        | GotReadMe data ->
            let m = { model with readMeData = data |> Some }
            m, Cmd.none
        | NavigateTo page ->
            let m = { model with page = page }
            m, Cmd.none
        | ChangeVisualState state ->
            let m =
                match state with
                | ClipboardData s -> { model with visualStates = model.setClipboardData s }
                | DropDownItem n -> { model with visualStates = model.toggleDropDownItemState n }
                | ProgressValue _ -> { model with visualStates = model.iterateProgressValue() }
                | _ -> { model with visualStates = model.visualStates.toggleState state }
            m, Cmd.none

    let view model dispatch = TabsElmishComponent.EComp model dispatch

    override this.Program =
        let m = StudioFloorModel.initialize this.Services
        let cmd = Cmd.ofMsg StudioFloorMessage.GetReadMe

        Program.mkProgram (fun _ -> m, cmd) update view
        |> Program.withRouter ElmishRoutes.router
