namespace Songhay.Modules.Bolero

open System
open System.Net.Http
open Microsoft.AspNetCore.Components
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Microsoft.JSInterop

/// <summary>
/// Functions for the <see cref="IServiceProvider" /> interface.
/// </summary>
module ServiceProviderUtility =
    let mutable private serviceProvider = Unchecked.defaultof<IServiceProvider>

    /// <summary>
    /// Gets the specified <c>'service</c>
    /// from the instance of <see cref="IServiceProvider" />
    /// for the entire Blazor application.
    /// </summary>
    let getBlazorService<'service>() =
        if serviceProvider = null then
            raise (exn "The expected Service Provider is not here. Was `setBlazorServiceProvider` called?")

        serviceProvider.GetService<'service>()

    /// <summary>
    /// Gets <see cref="HttpClient" /> from the <see cref="IServiceProvider"/>
    /// of this application.
    /// </summary>
    let getHttpClient() = getBlazorService<HttpClient>()

    /// <summary>
    /// Gets <see cref="IConfiguration" /> from the <see cref="IServiceProvider"/>
    /// of this application.
    /// </summary>
    let getIConfiguration() = getBlazorService<IConfiguration>()

    /// <summary>
    /// Gets <see cref="ILogger" /> from the <see cref="IServiceProvider"/>
    /// of this application.
    /// </summary>
    /// <remarks>
    /// An instance of <see cref="ILogger" /> appears to not be available
    /// from the <see cref="IServiceProvider"/> for the WASM-based version of Blazor.
    /// Use <c>[<Inject>]</c> on the component level or the <c>JsRuntimeUtility.console*</c> members instead.
    /// </remarks>
    let getILogger() = getBlazorService<ILogger>()

    /// <summary>
    /// Gets <see cref="IJSRuntime" /> from the <see cref="IServiceProvider"/>
    /// of this application.
    /// </summary>
    let getIJSRuntime() = getBlazorService<IJSRuntime>()

    /// <summary>
    /// Gets <see cref="NavigationManager" /> from the <see cref="IServiceProvider"/>
    /// of this application.
    /// </summary>
    let getNavigationManager() = getBlazorService<NavigationManager>()

    /// <summary>
    /// Sets the <see cref="IServiceProvider" /> interface
    /// for the entire Blazor application.
    /// </summary>
    let setBlazorServiceProvider (provider: IServiceProvider) = serviceProvider <- provider
