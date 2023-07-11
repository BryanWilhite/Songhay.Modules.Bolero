# Songhay.Modules.Bolero

## shared functions and types for Bolero, supporting CSS 💄, specifically Bulma CSS 🍱🖼

**NuGet package 📦:** [`Songhay.Modules.Bolero`](https://www.nuget.org/packages/Songhay.Modules.Bolero/)

These are the main concerns of this work:

- Utility, mostly sharing functions for Remoting and `IJSRuntime`
- Visuals, a deliberately incomplete coverage of HTML, CSS and [Bulma](https://bulma.io/) by [Jeremy Thomas](https://jgthms.com/)
- Models (Primitives), F♯ primitive obsession, supporting the Visuals

## Utility

There are four utilities:

1. a utility for [Bolero](https://github.com/fsbolero/bolero) [[src](Songhay.Modules.Bolero/BoleroUtility.fs)]
2. a utility for JSON documents [[src](Songhay.Modules.Bolero/JsonDocumentUtility.fs)]
3. a utility for `IJSRuntime` [[src](Songhay.Modules.Bolero/JsRuntimeUtility.fs)]
4. a utility for `IRemoteService` [[src](Songhay.Modules.Bolero/RemoteHandlerUtility.fs)]

## Visuals and Models

Here is my first attempt at building a <acronym title="Domain-Specific Language">DSL</acronym> for HTML, CSS and Bulma CSS on top of Bolero. For example, here is an expression [[GitHub](https://github.com/BryanWilhite/Songhay.Modules.Bolero/blob/main/Songhay.StudioFloor.Client/Components/TabsElmishComponent.fs#L24)], rendering [Bulma tabs](https://bulma.io/documentation/components/tabs/), overriding the Bolero `ElmishComponent<_,_>`:

```fsharp
override this.View model dispatch =
    concat {

        let tabs = [
            (text "README", ReadMeTab)
            (concat { text "Bolero "; code { text "IJsRuntime" } }, BoleroJsRuntimeTab)
            (text "Bulma Visuals", BulmaVisualsTab)
        ]

        bulmaTabs
            (HasClasses <| CssClasses [ ColorEmpty.BackgroundCssClassLight; "is-toggle"; "is-fullwidth"; SizeLarge.CssClass ])
            (fun pg -> model.tab = pg)
            (fun pg _ -> SetTab pg |> dispatch)
            tabs

        cond model.tab <| function
        | ReadMeTab ->
            if model.readMeData.IsNone then
                text "loading…"
            else
                bulmaContainer
                    ContainerWidthFluid
                    NoCssClasses
                    (bulmaNotification
                        (HasClasses <| CssClasses [ ColorPrimary.CssClass ])
                        (rawHtml model.readMeData.Value))

        | BoleroJsRuntimeTab ->
            bulmaContainer
                ContainerWidthFluid
                NoCssClasses
                (BoleroJsRuntimeElmishComponent.EComp model dispatch)

        | BulmaVisualsTab ->
            bulmaContainer
                ContainerWidthFluid
                NoCssClasses
                (BulmaVisualsElmishComponent.EComp model dispatch)
    }
```

>**Warning**
>
>This approach to the <acronym title="Domain-Specific Language">DSL</acronym> is wrapping functions around <acronym title="Computation Expression">CE</acronym>s instead of wrapping <acronym title="Computation Expression">CE</acronym>s with <acronym title="Computation Expression">CE</acronym>s. Research in this direction is underway… on and off…
>

The preference here (at the moment) is to have more types than functions for the <acronym title="Domain-Specific Language">DSL</acronym>. These types are grouped into four models of primitives:

1. Bulma Primitives [[src](Songhay.Modules.Bolero/Models/BulmaPrimitives.fs)]
2. CSS Primitives [[src](Songhay.Modules.Bolero/Models/CssPrimitives.fs)]
3. HTML Primitives [[src](Songhay.Modules.Bolero/Models/HtmlPrimitives.fs)]
4. SVG Primitives [[src](Songhay.Modules.Bolero/Models/SvgPrimitives.fs)]

These models support the functions of the Visuals:

1. HTML Body Visuals [[src](Songhay.Modules.Bolero/Visuals/BodyElement.fs)]
2. CSS Declarations [[src](Songhay.Modules.Bolero/Visuals/CssDeclaration.fs)]
3. HTML Head Elements [[src](Songhay.Modules.Bolero/Visuals/HeadElement.fs)]
4. Bulma Component Visuals [[src](Songhay.Modules.Bolero/Visuals/Bulma/Component.fs)]
5. Bulma CSS Class names [[src](Songhay.Modules.Bolero/Visuals/Bulma/CssClass.fs)]
6. Bulma Element Visuals [[src](Songhay.Modules.Bolero/Visuals/Bulma/Element.fs)]
7. Bulma Layout Visuals [[src](Songhay.Modules.Bolero/Visuals/Bulma/Layout.fs)]

The coverage of HTML is quite limited because Bulma itself has its own, excellent HTML <acronym title="Domain-Specific Language">DSL</acronym> on which this work depends heavily. The _generic_ <acronym title="Cascading Style Sheets">CSS</acronym> coverage is starting off with typography. The Bulma-specific coverage is the most extensive but lacking in the following areas:

- [the Bulma form](https://bulma.io/documentation/form/)
- [Bulma pagination](https://bulma.io/documentation/components/pagination/)

I look forward to working a bit more on this Bulma coverage, likely starting with Bulma pagination.

### custom JavaScript is needed for the Bulma Navbar

In order to toggle the Bulma Navbar [burger](https://bulma.io/documentation/components/navbar/#navbar-burger) and its dropdown menu, the following JavaScript is needed:

```javascript
(() => {

    window.addEventListener('DOMContentLoaded', () => {
        const burger = document.querySelector('.navbar-burger');
        const nav = document.querySelector(`#${burger.dataset.target}`);
        const isActiveCssClass = 'is-active';

        burger.addEventListener('click', () => {
            burger.classList.toggle(isActiveCssClass);
            nav.classList.toggle(isActiveCssClass);
        });
    });

})();
```

This JavaScript is similar to [the code provided in the Bulma documentation](https://bulma.io/documentation/components/navbar/#navbar-menu).

## the ‘studio floor’ for this Solution

The `Songhay.StudioFloor.Client` project [[GitHub](https://github.com/BryanWilhite/Songhay.Modules.Bolero/tree/main/Songhay.StudioFloor.Client)] has at least two purposes:

1. demonstrate how the [Bulma](https://bulma.io/) components and elements look and operate
2. provide a “reference” <acronym title="Sassy CSS">SCSS</acronym> and Typescript `npm` pipeline

![the ‘studio floor’ for this Solution](https://github.com/BryanWilhite/Songhay.Modules.Bolero/tree/main/Songhay.StudioFloor.Client/Songhay.StudioFloor.Client.gif)

@[BryanWilhite](https://twitter.com/BryanWilhite)
