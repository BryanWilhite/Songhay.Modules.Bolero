namespace Songhay.StudioFloor.Client.Components

open Elmish
open Bolero

open Microsoft.Extensions.Logging
open Songhay.Modules.Bolero
open Songhay.Modules.Bolero.Models
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
            ServiceProviderUtility.getIJSRuntime() |> JsRuntimeUtility.consoleWarnAsync [|
                    $"{nameof StudioFloorProgramComponent}: hey! This is a warning for the {nameof GotReadMe} state!";
                    $"\n{nameof StudioFloorProgramComponent}: {nameof ILogger} available in Elmish update function?: {ServiceProviderUtility.getILogger() <> null}";
                    $"\n{nameof StudioFloorProgramComponent}: {nameof RestApiMetadata} available on Elmish model?: {model.restApiMetadata.GetApiBase() <> System.String.Empty}"
                |]
                |> ignore

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
        let cmd = Cmd.ofMsg GetReadMe

        Program.mkProgram (fun _ -> m, cmd) update view
        |> Program.withRouter ElmishRoutes.router
