namespace Songhay.StudioFloor.Client.Components

open System.Collections.Generic
open Bolero
open Bolero.Html

open Microsoft.AspNetCore.Components
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.BodyElement
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

        let myDictionary = Dictionary<string, string>()
        (this.configuration.GetSection "MyDictionary").Bind myDictionary

        let restApiMetadata = "PlayerApi" |> RestApiMetadata.fromConfiguration this.configuration

        bulmaColumnsContainer
            (HasClasses <| CssClasses [ m (All, L4) ])
            (concat {
                bulmaColumn NoCssClasses (bulmaNotification
                    (HasClasses <| CssClasses [ ColorPrimary.CssClass ])
                    (
                        concat {
                            h1 { "Blazor Configuration" |> text }
                            para {
                                Html.label { "type name: " |> text }
                                $"`{this.configuration}`" |> text
                            }
                            para {
                                Html.label { "simple string value: " |> text }
                                this.configuration.GetValue "Greeting" |> text
                            }
                            para {
                                Html.label { "default logging level: " |> text }
                                this.configuration.GetValue "Logging:LogLevel:Default" |> text
                            }
                            para {
                                Html.label { "value from dictionary: " |> text }
                                myDictionary["two"] |> text
                            }
                            h2 { "conventional RestApiMetadata" |> text }
                            para {
                                Html.label { "string representation: " |> text }
                                restApiMetadata.ToString() |> text
                            }
                            para {
                                Html.label { "endpoint-prefix claim: " |> text }
                                restApiMetadata.GetClaim "endpoint-prefix" |> Option.defaultWith (fun() -> "[missing!]") |> text
                            }
                        }
                    )
                )
            })
