namespace AlphaConnect.Client.Features.Sandbox

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button
    open AlphaConnect.Client.Context.Navigator

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [

            Html.h1 [
                Attr.classes [ "text-4xl"; "font-bold"; "text-primary" ]
                Html.text "Sandbox"
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Intro"
                button.onClick (fun _ -> navigate (IntroRoute Intro.SamplePage))
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Local Route"
                button.onClick (fun _ -> navigate (LocalRoute LocalRoute.Page1))
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Reactivity"
                button.onClick (fun _ -> navigate (ReactivityRoute Reactivity.ReactiveAssignmentPage))
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Logic"
                button.onClick (fun _ -> navigate (LogicRoute Logic.IfBlockPage))
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Events"
                button.onClick (fun _ -> navigate (EventsRoute Events.EventModifiersPage))
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Transitions"
                button.onClick (fun _ -> navigate (TransitionsRoute Transitions.TransitionPage))
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Elm"
                button.onClick (fun _ -> navigate (ElmRoute Elm.ElmishCounterPage))
            ]

            match route with
            | IntroRoute route' -> Intro.Layout.view env route'
            | LocalRoute _ -> LocalRoute.Layout.view // TODO: this local route this POC is not yet working
            | ReactivityRoute route' -> Reactivity.Layout.view env route'
            | LogicRoute route' -> Logic.Layout.view env route'
            | EventsRoute route' -> Events.Layout.view env route'
            | TransitionsRoute route' -> Transitions.Layout.view env route'
            | ElmRoute route' -> Elm.Layout.view env route'
        ]