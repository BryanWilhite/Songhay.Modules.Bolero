namespace Songhay.Modules.Bolero

open System
open System.Runtime.CompilerServices
open Microsoft.Extensions.Logging

/// <summary>
/// Extensions of <see cref="ILogger"/>
/// </summary>
type ILoggerExtensions =

    /// <summary>
    /// Logs the specified <see cref="Exception"/>
    /// </summary>
    /// <param name="logger">the <see cref="ILogger"/></param>
    /// <param name="ex">the <see cref="Exception"/></param>
    [<Extension>]
    static member inline LogException (logger: ILogger, ex: Exception) =
        let template = "Error: {Message}{NewLine}{StackTrace}"

        if logger <> null then

            if ex <> null then
                logger.LogError(template, ex.Message, Environment.NewLine, ex.StackTrace)

                if ex.InnerException <> null then
                    logger.LogError(template, ex.InnerException.Message, Environment.NewLine, ex.InnerException.StackTrace)
