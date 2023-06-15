namespace Songhay.StudioFloor.Client.Models

open System.Net.Http

open Microsoft.AspNetCore.Components
open Microsoft.JSInterop

type StudioFloorModel =
    {
        blazorServices: {| httpClient: HttpClient; jsRuntime: IJSRuntime; navigationManager: NavigationManager |}
        tab: StudioFloorTab
        readMeData: string option
    }

    static member initialize (httpClient: HttpClient) (jsRuntime: IJSRuntime) (navigationManager: NavigationManager) =
        {
            blazorServices = {| httpClient = httpClient; jsRuntime = jsRuntime; navigationManager = navigationManager |}
            tab = ReadMeTab
            readMeData = None
        }
