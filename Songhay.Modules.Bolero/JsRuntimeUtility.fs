namespace Songhay.Modules.Bolero

open System
open Microsoft.JSInterop

open FsToolkit.ErrorHandling

open Bolero

/// <summary>
/// Functions for the <see cref="IJSRuntime" /> interface.
/// </summary>
module JsRuntimeUtility =
    /// <summary> conventional namespace prefix for <c>songhay-core</c> JavaScript </summary>
    [<Literal>]
    let rx = "rx"

    /// <summary> references the <c>songhay-core</c> CssUtility from Bolero/Blazor </summary>
    /// <remarks> 📖 https://github.com/BryanWilhite/songhay-core/blob/master/src/utilities/css.utility.ts </remarks>
    [<Literal>]
    let CssUtility = "CssUtility"

    /// <summary> references the <c>songhay-core</c> DomUtility from Bolero/Blazor </summary>
    /// <remarks> 📖 https://github.com/BryanWilhite/songhay-core/blob/master/src/utilities/dom.utility.ts </remarks>
    [<Literal>]
    let DomUtility = "DomUtility"

    /// <summary>
    /// Tries to get <see cref="ElementReference" /> to a rendered element
    /// or errors with <see cref="FormatException" />.
    /// </summary>
    let tryGetElementReference (htmlRef: HtmlRef) =
        match htmlRef.Value with
        | Some elementRef -> Ok elementRef
        | _ -> Error (FormatException "The expected HTML element reference is not here.")

    ///<summary>
    /// Calls the methods of the JavaScript <c>console</c> object
    /// via the <see cref="IJSRuntime.InvokeVoidAsync" /> method.
    /// See  https://developer.mozilla.org/en-US/docs/Web/API/console ]
    ///</summary>
    /// <remarks>
    /// The specified <c>args</c> object array has its elements converted to strings
    /// before it is passed to the <see cref="IJSRuntime.InvokeVoidAsync" /> method.
    /// </remarks>
    let callConsoleMethodAsync (methodName: string) (args: obj[]) (jsRuntime: IJSRuntime) =
        let toStringArray (oArray: obj[]) =
            oArray |> Array.map(fun o -> if o :? string then o else $"{o}" )

        jsRuntime.InvokeVoidAsync($"console.{methodName}", args |> toStringArray).AsTask()

    ///<summary>
    /// Calls the <c>console.debug</c> method
    /// via the <see cref="callConsoleMethodAsync" /> function.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/API/console/debug
    /// </remarks>
    let consoleDebugAsync (args: obj[]) (jsRuntime: IJSRuntime) = jsRuntime |> callConsoleMethodAsync "debug" args

    ///<summary>
    /// Calls the <c>console.error</c> method
    /// via the <see cref="callConsoleMethodAsync" /> function.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/API/console/error
    /// </remarks>
    let consoleErrorAsync (args: obj[]) (jsRuntime: IJSRuntime) = jsRuntime |> callConsoleMethodAsync "error" args

    ///<summary>
    /// Calls the <c>console.info</c> method
    /// via the <see cref="callConsoleMethodAsync" /> function.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/API/console/info
    /// </remarks>
    let consoleInfoAsync (args: obj[]) (jsRuntime: IJSRuntime) = jsRuntime |> callConsoleMethodAsync "info" args

    ///<summary>
    /// Calls the <c>console.log</c> method
    /// via the <see cref="callConsoleMethodAsync" /> function.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/API/console/log
    /// </remarks>
    let consoleLogAsync (args: obj[]) (jsRuntime: IJSRuntime) = jsRuntime |> callConsoleMethodAsync "log" args

    ///<summary>
    /// Calls the <c>console.warn</c> method
    /// via the <see cref="callConsoleMethodAsync" /> function.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/API/console/warn
    /// </remarks>
    let consoleWarnAsync (args: obj[]) (jsRuntime: IJSRuntime) = jsRuntime |> callConsoleMethodAsync "warn" args

    ///<summary>
    /// Calls the <c>CssUtility.getComputedStylePropertyValue</c> method in <c>songhay-core</c>.
    ///</summary>
    /// <remarks>
    /// 📖 https://github.com/BryanWilhite/songhay-core/blob/master/src/utilities/css.utility.ts#L33
    /// </remarks>
    let getComputedStylePropertyValueAsync (htmlRef: HtmlRef) (propertyName: string) (jsRuntime: IJSRuntime) =
        let elementRef = htmlRef |> tryGetElementReference |> Result.valueOr raise
        jsRuntime.InvokeAsync<string>($"{rx}.{CssUtility}.getComputedStylePropertyValue", elementRef, propertyName).AsTask()

    ///<summary>
    /// Calls the <c>CssUtility.getComputedStylePropertyValueById</c> method in <c>songhay-core</c>.
    ///</summary>
    /// <remarks>
    /// 📖 https://github.com/BryanWilhite/songhay-core/blob/master/src/utilities/css.utility.ts#L49
    /// </remarks>
    let getComputedStylePropertyValueByIdAsync (elementId: string) (propertyName: string) (jsRuntime: IJSRuntime) =
        jsRuntime.InvokeAsync<string>($"{rx}.{CssUtility}.getComputedStylePropertyValueById", elementId, propertyName).AsTask()

    ///<summary>
    /// Calls the <c>CssUtility.getComputedStylePropertyValueByQuery</c> method in <c>songhay-core</c>.
    ///</summary>
    /// <remarks>
    /// 📖 https://github.com/BryanWilhite/songhay-core/blob/master/src/utilities/css.utility.ts#L70
    /// </remarks>
    let getComputedStylePropertyValueByQueryAsync (query: string) (propertyName: string) (jsRuntime: IJSRuntime) =
        jsRuntime.InvokeAsync<string>($"{rx}.{CssUtility}.getComputedStylePropertyValueByQuery", query, propertyName).AsTask()

    ///<summary>
    /// Calls the <c>CssUtility.setComputedStylePropertyValue</c> method in <c>songhay-core</c>.
    ///</summary>
    /// <remarks>
    /// 📖 https://github.com/BryanWilhite/songhay-core/blob/master/src/utilities/css.utility.ts#L153
    /// </remarks>
    let setComputedStylePropertyValueAsync (htmlRef: HtmlRef) (propertyName: string) (propertyValue: string) (jsRuntime: IJSRuntime) =
        let elementRef = htmlRef |> tryGetElementReference |> Result.valueOr raise
        jsRuntime.InvokeVoidAsync($"{rx}.{CssUtility}.setComputedStylePropertyValue", elementRef, propertyName, propertyValue).AsTask()
