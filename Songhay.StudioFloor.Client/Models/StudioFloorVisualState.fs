namespace Songhay.StudioFloor.Client.Models

type StudioFloorVisualState =
    | ClipboardData of string
    | DropDownContentActive
    | DropDownItem of int
    | ProgressValue of int
