namespace Songhay.Modules.Bolero.Visuals

open System.Collections.Generic

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.SvgUtility
open Songhay.Modules.Models

module SvgElement =
    /// <summary>
    /// Returns an SVG <c>polygon</c> element.
    /// </summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/polygon
    /// </remarks>
    let polygonNode (attr: HtmlAttributeOrEmpty) (points: string) =
        elt "polygon" {
            "points" => points
            attr.Value
        }

    /// <summary>
    /// Returns the conventional <c>svg</c> element.
    /// </summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/svg
    /// </remarks>
    let toSvgBlock (svgDictionary: IDictionary<Identifier, Node>) =
        svg {
            "xmlns" => SvgUri
            attr.style "display: none;"
            forEach svgDictionary <| fun pair -> pair.Value
        }

    let toSvgUse (attr: HtmlAttributeOrEmpty) (id: Identifier) =
        elt "use" {
            Html.attr.id id.StringValue
            attr.Value
            "xlink:href" => $"#{id.StringValue}"
        }
