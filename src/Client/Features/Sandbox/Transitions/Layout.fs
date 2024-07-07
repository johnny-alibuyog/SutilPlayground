namespace AlphaConnect.Client.Features.Sandbox.Transitions

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
                button.text "Transitions"
                button.onClick (fun _ -> navigate TransitionPage)
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Transition with Parameters"
                button.onClick (fun _ -> navigate TransitionWithParametersPage)
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Animation"
                button.onClick (fun _ -> navigate AnimationPage)
            ]

            match route with
            | TransitionPage -> TransitionPage.view ()
            | TransitionWithParametersPage -> TransitionWithParametersPage.view ()
            | AnimationPage -> AnimationPage.view ()
        ]