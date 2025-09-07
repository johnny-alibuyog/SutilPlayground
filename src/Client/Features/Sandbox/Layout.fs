namespace SutilPlayground.Client.Features.Sandbox

module Layout =
    open Sutil
    open SutilPlayground.Client.Components.Button
    open SutilPlayground.Client.Env.Navigation

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [

            Html.h1 [
                Attr.classes [ "text-4xl"; "font-bold"; "text-primary" ]
                Html.text "Sandbox"
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Intro"
                Button.onClick (fun _ -> navigate (IntroRoute Intro.SamplePage))
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Local Route"
                Button.onClick (fun _ -> navigate (LocalRoute LocalRoute.Page1))
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Reactivity"
                Button.onClick (fun _ -> navigate (ReactivityRoute Reactivity.ReactiveAssignmentPage))
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Logic"
                Button.onClick (fun _ -> navigate (LogicRoute Logic.IfBlockPage))
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Events"
                Button.onClick (fun _ -> navigate (EventsRoute Events.EventModifiersPage))
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Transitions"
                Button.onClick (fun _ -> navigate (TransitionsRoute Transitions.TransitionPage))
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Elm"
                Button.onClick (fun _ -> navigate (ElmRoute Elm.ElmishCounterPage))
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