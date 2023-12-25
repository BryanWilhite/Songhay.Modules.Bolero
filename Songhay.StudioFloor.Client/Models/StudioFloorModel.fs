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
        progressValue: int
    }

    static member initialize (httpClient: HttpClient) (jsRuntime: IJSRuntime) (navigationManager: NavigationManager) =
        {
            blazorServices = {| httpClient = httpClient; jsRuntime = jsRuntime; navigationManager = navigationManager |}
            bulmaVisualsStates = AppStateSet.initialize 
            tab = ReadMeTab
            readMeData = None
            progressValue = 1
        }
