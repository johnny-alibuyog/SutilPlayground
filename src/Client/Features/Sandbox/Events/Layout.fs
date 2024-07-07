namespace AlphaConnect.Client.Features.Sandbox.Events

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button
    open AlphaConnect.Client.Context.Navigator

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [

            Html.h1 [ Attr.classes [ "text-4xl"; "font-bold"; "text-primary" ]; Html.text "Events" ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Event Modifiers"
                button.onClick (fun _ -> navigate EventModifiersPage)
            ]

            match route with
            | EventModifiersPage -> EventModifiersPage.view ()
        ]