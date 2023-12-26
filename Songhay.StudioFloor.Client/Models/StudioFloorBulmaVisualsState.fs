namespace Songhay.StudioFloor.Client.Models

type StudioFloorBulmaVisualsState =
    | DropDownContentActive
    | DropDownItem of int
    | ProgressValue of int
