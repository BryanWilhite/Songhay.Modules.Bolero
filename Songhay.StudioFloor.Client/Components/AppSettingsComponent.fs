namespace Songhay.StudioFloor.Client.Components

open System.Collections.Generic
open Bolero
open Bolero.Html

open Microsoft.AspNetCore.Components
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout
open Songhay.Modules.Models

type AppSettingsComponent() =
    inherit Component()

    [<Inject>]
    member val private configuration = Unchecked.defaultof<IConfiguration> with get, set

    [<Inject>]
    member val private logger = Unchecked.defaultof<ILogger<AppSettingsComponent>> with get, set

    static member BComp =
        comp<AppSettingsComponent> { Attr.Empty() }

    override this.Render() =
        this.logger.LogDebug("Log debug! (LogLevel in appsettings.json is ignored.)")
        this.logger.LogWarning("`builder.Logging.SetMinimumLevel` must be set for logging level to be recognized.")

        let mutable myDictionary = Dictionary<string, string>()
        (this.configuration.GetSection "MyDictionary").Bind myDictionary

        bulmaColumnsContainer
            (HasClasses <| CssClasses [ m (All, L4) ])
            (concat {
                bulmaColumn NoCssClasses (bulmaNotification
                    (HasClasses <| CssClasses [ ColorPrimary.CssClass ])
                    (
                        concat {
                            h1 { "Blazor Configuration" |> text }
                            Html.p {
                                Html.label { "type name: " |> text }
                                $"`{this.configuration}`" |> text
                            }
                            Html.p {
                                Html.label { "simple string value: " |> text }
                                this.configuration.GetValue "Greeting" |> text
                            }
                            Html.p {
                                Html.label { "default logging level: " |> text }
                                this.configuration.GetValue "Logging:LogLevel:Default" |> text
                            }
                            Html.p {
                                Html.label { "value from dictionary: " |> text }
                                myDictionary["two"] |> text
                            }
                        }
                    )
                )
            })
