namespace Songhay.Modules.Bolero

open System
open Microsoft.Extensions.DependencyInjection

/// <summary>
/// Functions for the <see cref="IServiceProvider" /> interface.
/// </summary>
module ServiceProviderUtility =
    let mutable private serviceProvider: IServiceProvider option = None

    /// <summary>
    /// Gets the specified <c>'service</c>
    /// from the instance of <see cref="IServiceProvider" />
    /// for the entire Blazor application.
    /// </summary>
    let getBlazorService<'service>() =
        if serviceProvider.IsSome then
            serviceProvider.Value.GetService<'service>() |> Some
        else
            None

    /// <summary>
    /// Sets the <see cref="IServiceProvider" /> interface
    /// for the entire Blazor application.
    /// </summary>
    let setBlazorServiceProvider (provider: IServiceProvider) =
        serviceProvider <- provider |> Some
