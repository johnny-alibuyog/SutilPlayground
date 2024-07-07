namespace AlphaConnect.Client.Features.Sandbox.Intro

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
                button.text "Hellow World"
                button.onClick (fun _ -> navigate HelloWorldPage)
            ]

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

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Styling"
                button.onClick (fun _ -> navigate StylingPage)
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Nested Component"
                button.onClick (fun _ -> navigate NestedComponentPage)
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Html Tags"
                button.onClick (fun _ -> navigate HtmlTagsPage)
            ]

            match route with
            | HelloWorldPage -> HelloWorldPage.view ()
            | SamplePage -> SamplePage.view ()
            | DynamicAttributePage -> DynamicAttributePage.view ()
            | StylingPage -> StylingPage.view ()
            | NestedComponentPage -> NestedComponentPage.view ()
            | HtmlTagsPage -> HtmlTagsPage.view ()
        ]