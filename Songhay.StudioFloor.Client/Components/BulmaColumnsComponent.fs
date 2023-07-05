namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout
open Songhay.Modules.Models

type BulmaColumnsComponent() =
    inherit Component()

    static member BComp =
        comp<BulmaColumnsComponent> { Attr.Empty() }

    override this.Render() =
        bulmaColumnsContainer
            (HasClasses <| CssClasses [ m (All, L4) ])
            (concat {
                bulmaColumn NoCssClasses (bulmaNotification NoCssClasses (text "1"))
                bulmaColumn NoCssClasses (bulmaNotification NoCssClasses (text "2"))
                bulmaColumn NoCssClasses (bulmaNotification NoCssClasses (text "3"))
                bulmaColumn NoCssClasses (bulmaNotification NoCssClasses (text "4"))
            })
