namespace Songhay.StudioFloor.Client.Models

type StudioFloorMessage =
    | Error of exn
    | GetReadMe | GotReadMe of string
    | SetTab of StudioFloorTab
