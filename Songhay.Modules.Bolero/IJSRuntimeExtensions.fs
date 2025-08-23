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
    /// Logs the specified logging message
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="message">the logging message</param>
    [<Extension>]
    static member inline LogDebug (jsRuntime: IJSRuntime, message: string) =

        if jsRuntime <> null then

            jsRuntime |> consoleDebugAsync [| message |] |> ignore

    /// <summary>
    /// Logs the specified logging message
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="message">the logging message</param>
    [<Extension>]
    static member inline LogDebugAsync (jsRuntime: IJSRuntime, message: string) = async { jsRuntime.LogDebug message }

    /// <summary>
    /// Logs the specified logging message
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="message">the logging message</param>
    [<Extension>]
    static member inline LogError (jsRuntime: IJSRuntime, message: string) =

        if jsRuntime <> null then

            jsRuntime |> consoleErrorAsync [| message |] |> ignore

    /// <summary>
    /// Logs the specified logging message
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="message">the logging message</param>
    [<Extension>]
    static member inline LogErrorAsync (jsRuntime: IJSRuntime, message: string) = async { jsRuntime.LogError message }

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

    /// <summary>
    /// Logs the specified <see cref="Exception"/>
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="ex">the <see cref="Exception"/></param>
    [<Extension>]
    static member inline LogExceptionAsync (jsRuntime: IJSRuntime, ex: Exception) = async { jsRuntime.LogException ex }

    /// <summary>
    /// Logs the specified logging message
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="message">the logging message</param>
    [<Extension>]
    static member inline LogInformation (jsRuntime: IJSRuntime, message: string) =

        if jsRuntime <> null then

            jsRuntime |> consoleInfoAsync [| message |] |> ignore

    /// <summary>
    /// Logs the specified logging message
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="message">the logging message</param>
    [<Extension>]
    static member inline LogInformationAsync (jsRuntime: IJSRuntime, message: string) = async { jsRuntime.LogInformation message }

    /// <summary>
    /// Logs the specified logging message
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="message">the logging message</param>
    [<Extension>]
    static member inline LogWarning (jsRuntime: IJSRuntime, message: string) =

        if jsRuntime <> null then

            jsRuntime |> consoleWarnAsync [| message |] |> ignore

    /// <summary>
    /// Logs the specified logging message
    /// </summary>
    /// <param name="jsRuntime">the <see cref="IJSRuntime"/></param>
    /// <param name="message">the logging message</param>
    [<Extension>]
    static member inline LogWarningAsync (jsRuntime: IJSRuntime, message: string) = async { jsRuntime.LogWarning message }
