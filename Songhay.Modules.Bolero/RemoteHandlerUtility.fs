namespace Songhay.Modules.Bolero

open System.Net
open System.Net.Http
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.Logging

open Songhay.Modules.HttpResponseMessageUtility
open Songhay.Modules.Bolero.JsonDocumentUtility

///<summary>
/// Shared functions for the <see cref="IRemoteService" /> interface.
///</summary>
/// <remarks>
/// 📖 https://fsbolero.io/docs/Remoting
/// </remarks>
module RemoteHandlerUtility =

    ///<summary>
    /// Tries to download a <see cref="string" /> or errors with <see cref="HttpStatusCode" />
    /// based on the specified response Result.
    ///</summary>
    let tryDownloadToStringAsync (logger: ILogger option) (responseResult: Result<HttpResponseMessage, exn>) =
        task {
            if logger.IsSome then logger.Value.LogInformation("downloading string result...")

            match responseResult with
            | Result.Error err ->
                if logger.IsSome then logger.Value.LogError(err.Message, err.GetType().FullName, err.Source, err.StackTrace)
                return Result.Error HttpStatusCode.InternalServerError

            | Result.Ok response ->
                let! stringResult = response |> tryDownloadToStringAsync
                return stringResult
        }

    ///<summary>
    /// Converts the specified <see cref="string" /> Result to the expected <c>'TOutput</c>
    /// of the specified data-getter or returns <see cref="None" />.
    ///</summary>
    let toHandlerOutput<'TOutput>
        (logger: ILogger option)
        (dataGetter: Result<JsonElement, JsonException> -> 'TOutput option)
        (stringResult: Result<string,HttpStatusCode>) : 'TOutput option =
        stringResult |> tryGetJsonElement logger |> dataGetter

    ///<summary>
    /// Converts the specified asynchronous <see cref="string" /> Result to the expected <c>'TOutput</c>
    /// of the specified data-getter or returns <see cref="None" />.
    ///</summary>
    let toHandlerOutputAsync<'TOutput>
        (logger: ILogger option)
        (dataGetter: Result<JsonElement, JsonException> -> 'TOutput option)
        (responseResult: Result<HttpResponseMessage, exn>) : Task<'TOutput option> =
        task {
            let! stringResult = (logger, responseResult) ||> tryDownloadToStringAsync
            return stringResult |> tryGetJsonElement logger |> dataGetter
        }
