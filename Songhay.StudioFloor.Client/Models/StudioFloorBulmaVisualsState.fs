namespace Songhay.StudioFloor.Client.Models

type StudioFloorBulmaVisualsState =
    | ClipboardData of string
    | DropDownContentActive
    | DropDownItem of int
    | ProgressValue of int
