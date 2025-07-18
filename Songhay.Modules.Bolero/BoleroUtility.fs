namespace Songhay.Modules.Bolero

open System
open Microsoft.Extensions.Configuration

open Bolero.Html

/// <summary>
/// Utility functions mostly for <see cref="Node" />.
/// </summary>
module BoleroUtility =

    /// <summary> conventional name of server-side class used in appsetings.json</summary>
    [<Literal>]
    let RestApiMetadata = "RestApiMetadata"

    /// <summary>
    /// Wraps <see cref="Environment.NewLine" /> to format markup.
    /// </summary>
    let newLine = rawHtml $"{Environment.NewLine}"

    /// <summary>
    /// Return spaces of indentation to format markup.
    /// </summary>
    let indent level =
        let numberOfSpaces = 4
        let spaceChar = ' '
        let charArray =
            spaceChar
            |> Array.replicate (numberOfSpaces * level)

        String(charArray) |> text
