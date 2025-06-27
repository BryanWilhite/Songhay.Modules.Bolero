namespace Songhay.StudioFloor.Client.Components

open System.Collections.Generic
open Bolero
open Bolero.Html

open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open Songhay.Modules.Bolero.Models
open Songhay.Modules.Bolero.Visuals.Bulma.CssClass
open Songhay.Modules.Bolero.Visuals.Bulma.Element
open Songhay.Modules.Bolero.Visuals.Bulma.Layout
open Songhay.Modules.Models
open Songhay.StudioFloor.Client

type AppSettingsComponent() =
    inherit Component()

    let logger = Songhay.Modules.Bolero.ServiceProviderUtility.getILogger()
    let configuration = Songhay.Modules.Bolero.ServiceProviderUtility.getBlazorService<IConfiguration>()

    static member BComp =
        comp<AppSettingsComponent> { Attr.Empty() }

    override this.Render() =
        //logger.LogInformation("Helooo!")
        //logger.LogInformation("{Instance}", configuration)

        let mutable myDictionary = Dictionary<string, string>()
        (configuration.GetSection "MyDictionary").Bind myDictionary

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
                                $"`{configuration}`" |> text
                            }
                            Html.p {
                                Html.label { "simple string value: " |> text }
                                configuration.GetValue "Greeting" |> text
                            }
                            Html.p {
                                Html.label { "default logging level: " |> text }
                                configuration.GetValue "Logging:LogLevel:Default" |> text
                            }
                            Html.p {
                                Html.label { "value from dictionary: " |> text }
                                myDictionary["two"] |> text
                            }
                        }
                    )
                )
            })
