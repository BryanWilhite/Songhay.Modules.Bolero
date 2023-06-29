namespace Songhay.StudioFloor.Client.Models

type StudioFloorMessage =
    | Error of exn
    | GetReadMe | GotReadMe of string
    | NextProgress
    | SetTab of StudioFloorTab
