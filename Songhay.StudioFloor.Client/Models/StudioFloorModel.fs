namespace Songhay.StudioFloor.Client.Models

open System.Net.Http

open Microsoft.AspNetCore.Components
open Microsoft.JSInterop
open Songhay.Modules.Models

type StudioFloorModel =
    {
        blazorServices: {| httpClient: HttpClient; jsRuntime: IJSRuntime; navigationManager: NavigationManager |}
        bulmaVisualsStates: AppStateSet<StudioFloorBulmaVisualsState>
        tab: StudioFloorTab
        readMeData: string option
    }

    static member initialize (httpClient: HttpClient) (jsRuntime: IJSRuntime) (navigationManager: NavigationManager) =
        {
            blazorServices = {| httpClient = httpClient; jsRuntime = jsRuntime; navigationManager = navigationManager |}
            bulmaVisualsStates = AppStateSet.initialize.addState(ProgressValue 1)
            tab = ReadMeTab
            readMeData = None
        }

    member this.getProgressValue() =
        this.bulmaVisualsStates.states
        |> Set.map(fun i -> match i with | ProgressValue n -> n | _ -> 1 )
        |> Set.toArray |> Array.head

    member this.iterateProgressValue() =
        let currentScalar = this.getProgressValue()
        let nextValue = ProgressValue <| currentScalar + 1
        this.bulmaVisualsStates.removeState(ProgressValue currentScalar).addState(nextValue)