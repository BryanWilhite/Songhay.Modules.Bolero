namespace Songhay.Modules.Bolero.Components

open System
open Bolero
open Bolero.Html

open Songhay.Modules.Models
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.SvgUtility
open Songhay.Modules.Bolero.Visuals.SvgElement

open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout

type AppVersionsComponent() =
    inherit Component()

    let appVersions =
        let boleroVersion = $"{typeof<Node>.Assembly.GetName().Version}"
        let dotnetRuntimeVersion = $"{Environment.Version.Major:D}.{Environment.Version.Minor:D2}"

        [
            {|
                id = SonghaySvgKeys.MDI_BOLERO_DANCE_24PX.ToAlphanumeric
                title = DisplayText $"Bolero {boleroVersion}"
                version = boleroVersion
            |}
            {|
                id = SonghaySvgKeys.MDI_DOTNET_24PX.ToAlphanumeric
                title = DisplayText $".NET Runtime {dotnetRuntimeVersion}"
                version = dotnetRuntimeVersion
            |}
        ]

    let svgVersionNode (data: {| id: Identifier; title: DisplayText; version: string |}) =
        let classes = CssClasses [
            levelItem
            ShadeGreyLight.TextCssClass
            elementTextIsUnselectable
            elementTextAlign AlignCentered
        ]

        bulmaLevelItem
            (HasClasses classes)
            (HasAttr (attr.title data.title.Value))
            (concat {
                bulmaIcon (((svgViewBoxSquare 24), SonghaySvgData.Get(data.id)) ||> svgElement)
                span { fontSize Size7 |> CssClasses.toHtmlClass; text data.version }
            })

    let versionsNode =
        let cssClassesParentLevel = CssClasses [ levelContainer; isMobileModifier ]
        let cssClassesSvgVersionNodes = [ ShadeGreyLight.TextCssClass ] |> cssClassesParentLevel.AppendList
        div {
            cssClassesSvgVersionNodes.ToHtmlClassAttribute
            forEach appVersions <| svgVersionNode
        }

    static member BComp =
        comp<AppVersionsComponent> { Attr.Empty() }

    override this.Render() =
        versionsNode
