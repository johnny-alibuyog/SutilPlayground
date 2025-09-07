namespace SutilPlayground.Client.Features.Sandbox.Transitions

module Layout =
    open Sutil
    open SutilPlayground.Client.Components.Button
    open SutilPlayground.Client.Env.Navigation

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [
            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Transitions"
                Button.onClick (fun _ -> navigate TransitionPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Transition with Parameters"
                Button.onClick (fun _ -> navigate TransitionWithParametersPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Animation"
                Button.onClick (fun _ -> navigate AnimationPage)
            ]

            match route with
            | TransitionPage -> TransitionPage.view ()
            | TransitionWithParametersPage -> TransitionWithParametersPage.view ()
            | AnimationPage -> AnimationPage.view ()
        ]