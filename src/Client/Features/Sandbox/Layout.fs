namespace AlphaConnect.Client.Features.Sandbox

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button

    let render navigator (route: Route) =

        let navigate = Route.navigate navigator

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
                button.text "Reactivity"
                button.onClick (fun _ -> navigate (ReactivityRoute Reactivity.ReactiveAssignmentPage))
            ]

            match route with
            | IntroRoute page -> Intro.Layout.render navigator page
            | ReactivityRoute page -> Reactivity.Layout.render navigator page
        ]