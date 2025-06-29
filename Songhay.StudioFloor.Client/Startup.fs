namespace Songhay.StudioFloor.Client

open System
open System.Net.Http

open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection

open Microsoft.Extensions.Logging
open Songhay.StudioFloor.Client.Components

module Program =

    [<EntryPoint>]
    let Main args =
        let builder = WebAssemblyHostBuilder.CreateDefault(args)
        builder.Logging.SetMinimumLevel(LogLevel.Debug) |> ignore
        builder.RootComponents.Add<StudioFloorProgramComponent>("#studio-floor")
        builder.Services.AddScoped<HttpClient>(fun _ ->
            new HttpClient(BaseAddress = Uri builder.HostEnvironment.BaseAddress)) |> ignore
        builder.Build().RunAsync() |> ignore
        0
