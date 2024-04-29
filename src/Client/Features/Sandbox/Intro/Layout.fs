namespace AlphaConnect.Client.Features.Sandbox.Intro

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button

    let render navigator (route: Route) =

        let navigate = Route.navigate navigator

        Html.div [
            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Sample"
                button.onClick (fun _ -> navigate SamplePage)
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Dynamic Attribute"
                button.onClick (fun _ -> navigate DynamicAttributePage)
            ]

            match route with
            | DynamicAttributePage -> DynamicAttributePage.view ()
            | SamplePage -> SamplePage.view ()
        ]