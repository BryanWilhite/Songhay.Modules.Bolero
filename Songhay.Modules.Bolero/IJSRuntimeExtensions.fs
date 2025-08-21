namespace Songhay.Modules.Bolero

open System
open System.Runtime.CompilerServices
open Microsoft.JSInterop

open Songhay.Modules.Bolero.JsRuntimeUtility

/// <summary>
/// Extensions of <see cref="IJSRuntime"/>
/// </summary>
type IJSRuntimeExtensions =

    /// <summary>
    /// Logs the specified <see cref="Exception"/>
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="ex">the <see cref="Exception"/></param>
    [<Extension>]
    static member inline LogException (jsRuntime: IJSRuntime, ex: Exception) =

        if jsRuntime <> null then

            if ex <> null then
                let message = $"Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}"
                jsRuntime |> consoleErrorAsync [| message |] |> ignore

                if ex.InnerException <> null then
                    let message = $"Error: {ex.InnerException.Message}{Environment.NewLine}{ex.InnerException.StackTrace}"
                    jsRuntime |> consoleErrorAsync [| message |] |> ignore
