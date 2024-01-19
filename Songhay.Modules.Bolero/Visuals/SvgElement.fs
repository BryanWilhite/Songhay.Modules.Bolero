namespace Songhay.Modules.Bolero.Visuals

open System.Collections.Generic

open Bolero
open Bolero.Html

open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.BoleroUtility
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

    ///<summary>
    /// Returns the HTML <c>svg</c> element, with default attributes,
    /// based on the specified <see cref="StreamGeometry" />.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/svg
    ///</remarks>
    let svgElement (viewBox: string) (pathData: StreamGeometry) =
        svg {
            "xmlns" => SvgUri
            "fill" => "currentColor"
            "preserveAspectRatio" => "xMidYMid meet"
            nameof viewBox => viewBox

            elt "path" { "d" => pathData.Value }
        }

    ///<summary>
    /// Returns the HTML <c>svg</c> element, with default attributes,
    /// declaring <c>use</c> and <c>xlink:href</c>.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/svg
    ///</remarks>
    let svgXLinkSpriteElement (viewBox: string) (symbolId: Identifier) =
        svg {
            "xmlns" => SvgUri
            "fill" => "currentColor"
            "preserveAspectRatio" => "xMidYMid meet"
            nameof viewBox => viewBox

            elt "use" { "xlink:href" => $"#{symbolId.StringValue}" }
        }

    ///<summary>
    /// Returns the HTML <c>svg</c> element,
    /// calling <see cref="StreamGeometry.ToSymbolElement" />
    /// for the specified symbol data.
    ///</summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/svg
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Element/symbol
    ///</remarks>
    let svgSymbolsContainer (symbolData: (Identifier * StreamGeometry * string)[]) =
        svg {
            "xmlns" => SvgUri
            attr.style "display: none;"
            forEach symbolData <| fun (id, d, _) ->
                concat {
                    newLine
                    indent 3
                    d.ToSymbolElement id
                }
            newLine
            indent 2
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
            forEach svgDictionary <| _.Value
        }

    let toSvgUse (attr: HtmlAttributeOrEmpty) (id: Identifier) =
        elt "use" {
            Html.attr.id id.StringValue
            attr.Value
            "xlink:href" => $"#{id.StringValue}"
        }
