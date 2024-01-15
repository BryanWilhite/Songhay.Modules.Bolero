namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

open Songhay.StudioFloor.Client.Models

type ReadMeElmishComponent() =
    inherit ElmishComponent<StudioFloorModel, StudioFloorMessage>()

    static member EComp model dispatch =
        ecomp<ReadMeElmishComponent, _, _> model dispatch { attr.empty() }

    override this.ShouldRender(oldModel, newModel) = oldModel.readMeData <> newModel.readMeData

    override this.View model _ =
        if model.readMeData.IsNone then
            bulmaLoader
                (HasClasses <| CssClasses [ DisplayInlineBlock.CssClass; CssClass.p (All, L6); CssClass.m (All, L6) ])
        else
            (bulmaNotification
                (HasClasses <| CssClasses [ ColorPrimary.CssClass ])
                (rawHtml model.readMeData.Value))
