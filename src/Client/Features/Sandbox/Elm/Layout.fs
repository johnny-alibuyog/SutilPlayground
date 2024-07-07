namespace AlphaConnect.Client.Features.Sandbox.Elm

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button
    open AlphaConnect.Client.Context.Navigator

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [
            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Elmish Counter"
                button.onClick (fun _ -> navigate ElmishCounterPage)
            ]

            match route with
            | ElmishCounterPage -> ElmishCounterPage.view ()
        ]