namespace Songhay.StudioFloor.Client.Components

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.SvgElement
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout
open Songhay.Modules.Models

type SvgComponent() =
    inherit Component()
    let svgData = [
        SonghaySvgKeys.MDI_AZURE_24PX.ToAlphanumeric
        SonghaySvgKeys.MDI_WRENCH_24PX.ToAlphanumeric
        SonghaySvgKeys.MDI_JSON_24PX.ToAlphanumeric
        SonghaySvgKeys.MDI_PACKAGE_24PX.ToAlphanumeric
    ]

    static member BComp =
        comp<SvgComponent> { Attr.Empty() }

    override this.Render() =
        let getVisual (svgKey: Identifier) =
            let svgPathData = SonghaySvgData.Get(svgKey)
            bulmaColumn
                NoCssClasses
                (bulmaNotification
                    NoCssClasses
                    (bulmaImageContainer
                    (Square Square48)
                    NoAttr
                    (svgElement (bulmaIconSvgViewBox Square24) svgPathData))
                )

        bulmaColumnsContainer
            (HasClasses <| CssClasses [ m (All, L4) ])
            (forEach svgData <| getVisual)
