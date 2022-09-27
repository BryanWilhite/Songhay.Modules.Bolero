namespace Songhay.Modules.Bolero

/// <summary>
/// Functions for visualization with Scalable Vector Graphics.
/// </summary>
/// <remarks>
/// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG
/// </remarks>
module SvgUtility =

    /// <summary>
    /// The SVG XML namespace.
    /// </summary>
    [<Literal>]
    let SvgUri = "http://www.w3.org/2000/svg"

    /// <summary>
    /// Returns SVG viewport coordinates.
    /// </summary>
    /// <remarks>
    /// 📖 https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/viewBox
    ///
    /// The first two coordinates can be thought of as “pan” values; the last two coordinates, “zoom” values.
    /// For icons, we “zoom” to the width and height of the original illustration, usually 16 by 16 pixels.
    /// </remarks>
    let svgViewBox (minX: int, minY: int) (width: int, height: int) = $"{minX} {minY} {width} {height}"

    /// <summary>
    /// Calls <see cref="svgViewBox" /> for square visuals.
    /// </summary>
    let svgViewBoxSquare widthAndHeight = svgViewBox (0,0) (widthAndHeight, widthAndHeight)
