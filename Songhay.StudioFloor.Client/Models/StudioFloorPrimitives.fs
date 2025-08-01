namespace Songhay.StudioFloor.Client.Models

open Bolero

type StudioFloorPage =
    | [<EndPoint "/">] ReadMePage
    | [<EndPoint "/bolero-js-runtime/">] BoleroJsRuntimePage
    | [<EndPoint "/bulma-visuals/">] BulmaVisualsPage
    | [<EndPoint "/blazor-config/">] BlazorConfiguration

type StudioFloorVisualState =
    | ClipboardData of string
    | DropDownContentActive
    | DropDownItem of int
    | ProgressValue of int

type StudioFloorMessage =
    | Error of exn
    | GetReadMe | GotReadMe of string
    | NavigateTo of StudioFloorPage
    | ChangeVisualState of StudioFloorVisualState
