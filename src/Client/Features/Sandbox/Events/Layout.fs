namespace SutilPlayground.Client.Features.Sandbox.Events

module Layout =
    open Sutil
    open SutilPlayground.Client.Components.Button
    open SutilPlayground.Client.Env.Navigation

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [

            Html.h1 [ Attr.classes [ "text-4xl"; "font-bold"; "text-primary" ]; Html.text "Events" ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Event Modifiers"
                Button.onClick (fun _ -> navigate EventModifiersPage)
            ]

            match route with
            | EventModifiersPage -> EventModifiersPage.view ()
        ]