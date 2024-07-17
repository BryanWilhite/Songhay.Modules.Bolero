namespace Songhay.Modules.Bolero.Models

/// <summary>
/// defines Elmish <see cref="HtmlRef"/> <c>dispatch</c> policies
/// </summary>
type ElmishComponentHtmlRefPolicy =
    /// <summary> <c>dispatch</c> conditionally when <see cref="ElmishComponent.View"/> is called</summary>
    | DispatchConditionally
    /// <summary> <c>dispatch</c> when <see cref="ElmishComponent.View"/> is called</summary>
    | DispatchForEveryView
    /// <summary> do not <c>dispatch</c> when <see cref="ElmishComponent.View"/> is called</summary>
    | DoNotDispatch

    /// <summary>
    /// Evaluates an instance the <see cref="ElmishComponentHtmlRefPolicy"/>
    /// </summary>
    /// <param name="dispatch">the <see cref="Elmish.Dispatch{'message}"/></param>
    /// <param name="message">the Elmish <c>'message</c></param>
    /// <param name="condition">the optional condition for <see cref="ElmishComponentHtmlRefPolicy.DispatchConditionally"/></param>
    member this.Evaluate (dispatch: Elmish.Dispatch<'message>, message: 'message, ?condition: bool) =
        match this with
        | DispatchConditionally -> if condition.IsSome && condition.Value then dispatch message
        | DispatchForEveryView -> dispatch message
        | DoNotDispatch -> ()
