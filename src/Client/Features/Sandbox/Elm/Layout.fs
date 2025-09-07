namespace SutilPlayground.Client.Features.Sandbox.Elm

module Layout =
    open Sutil
    open SutilPlayground.Client.Components.Button1
    open SutilPlayground.Client.Env.Navigation

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