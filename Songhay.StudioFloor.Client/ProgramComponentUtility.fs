namespace Songhay.StudioFloor.Client

open System
open System.Net
open System.Net.Http
open Microsoft.FSharp.Core
open Elmish

open FsToolkit.ErrorHandling
open Bolero.Remoting.Client

open Songhay.Modules.HttpClientUtility
open Songhay.Modules.HttpRequestMessageUtility
open Songhay.Modules.Bolero.JsRuntimeUtility
open Songhay.Modules.Bolero.RemoteHandlerUtility

open Songhay.StudioFloor.Client.Models

module ProgramComponentUtility =

    module Remote =
        let tryDownloadToStringAsync (client: HttpClient, uri: Uri) =
            async {
                let! responseResult = client |> trySendAsync (get uri) |> Async.AwaitTask
                let! output =
                    (None, responseResult) ||> tryDownloadToStringAsync
                    |> Async.AwaitTask

                return output
            }

    let getCommandForGetReadMe model =
        let success (result: Result<string, HttpStatusCode>) =
            let data = result |> Result.valueOr (fun code -> $"The expected README data is not here. [error code: {code}]")
            GotReadMe data
        let label = $"{nameof Remote.tryDownloadToStringAsync}:" |> Some
        let failure ex = model.blazorServices.jsRuntime |> passErrorToConsole label ex |> Error
        let uri = ("./README.html", UriKind.Relative) |> Uri

        Cmd.OfAsync.either Remote.tryDownloadToStringAsync (model.blazorServices.httpClient, uri) success failure
