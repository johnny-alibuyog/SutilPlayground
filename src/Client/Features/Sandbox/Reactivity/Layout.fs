namespace AlphaConnect.Client.Features.Sandbox.Reactivity

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button

    let render navigator (route: Route) =

        let navigate = Route.navigate navigator

        Html.div [

            Html.h1 [
                Attr.classes [ "text-4xl"; "font-bold"; "text-primary" ]
                Html.text "Reactivity"
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Reactive Assignments"
                button.onClick (fun _ -> navigate ReactiveAssignmentPage)
            ]

            match route with
            | ReactiveAssignmentPage -> ReactiveAssignmentPage.view ()
        ]