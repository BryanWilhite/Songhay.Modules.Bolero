namespace Songhay.Modules.Bolero.Models

/// <summary> magic string type for the <c>BlazorInteropUtility</c> JavaScript class </summary>
/// <remarks>
/// <c>BlazorInteropUtility.css</c> is tree-shaken from:
///
/// 📖 https://github.com/BryanWilhite/songhay-core/blob/master/src/utilities/css.utility.ts
///
/// The conventional TypeScript project generating <c>BlazorInteropUtility</c>:
///
/// 📖 https://github.com/BryanWilhite/Songhay.Modules.Bolero/tree/main/Songhay.StudioFloor.Client/src
///
/// </remarks>
type BlazorInteropUtility() =

    [<Literal>]
    static let getComputedStylePropertyValue' = "getComputedStylePropertyValue"

    [<Literal>]
    static let getComputedStylePropertyValueById' = "getComputedStylePropertyValueById"

    [<Literal>]
    static let getComputedStylePropertyValueByQuery' = "getComputedStylePropertyValueByQuery"

    [<Literal>]
    static let setComputedStylePropertyValue' = "setComputedStylePropertyValue"

    /// <summary> returns the magic string representing the JavaScript member of interop </summary>
    static member getComputedStylePropertyValue =
        $"{nameof BlazorInteropUtility}.{getComputedStylePropertyValue'}"

    /// <summary> returns the magic string representing the JavaScript member of interop </summary>
    static member getComputedStylePropertyValueById =
        $"{nameof BlazorInteropUtility}.{getComputedStylePropertyValueById'}"

    /// <summary> returns the magic string representing the JavaScript member of interop </summary>
    static member getComputedStylePropertyValueByQuery =
        $"{nameof BlazorInteropUtility}.{getComputedStylePropertyValueByQuery'}"

    /// <summary> returns the magic string representing the JavaScript member of interop </summary>
    static member setComputedStylePropertyValue =
        $"{nameof BlazorInteropUtility}.{setComputedStylePropertyValue'}"
