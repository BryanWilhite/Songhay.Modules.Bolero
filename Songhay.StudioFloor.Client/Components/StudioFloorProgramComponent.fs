namespace Songhay.StudioFloor.Client.Components

open System.Net.Http
open Bolero.Remoting.Client
open Microsoft.AspNetCore.Components
open Microsoft.JSInterop

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
        | SetTab tab ->
            let m = { model with tab = tab }
            m, Cmd.none

    let view model dispatch =
        TabsElmishComponent.EComp model dispatch

    [<Inject>]
    member val HttpClient = Unchecked.defaultof<HttpClient> with get, set

    [<Inject>]
    member val JSRuntime = Unchecked.defaultof<IJSRuntime> with get, set

    [<Inject>]
    member val NavigationManager = Unchecked.defaultof<NavigationManager> with get, set

    override this.Program =
        let m = StudioFloorModel.initialize this.HttpClient this.JSRuntime this.NavigationManager
        let cmd = Cmd.ofMsg StudioFloorMessage.GetReadMe

        Program.mkProgram (fun _ -> m, cmd) update view