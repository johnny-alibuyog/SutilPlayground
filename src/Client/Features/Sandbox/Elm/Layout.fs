namespace AlphaConnect.Client.Features.Sandbox.Elm

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button

    let view navigator (route: Route) =

        let navigate = Route.navigate navigator

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