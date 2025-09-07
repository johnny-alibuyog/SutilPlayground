namespace SutilPlayground.Client.Features.Sandbox.Intro

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
                Button.text "Hellow World"
                Button.onClick (fun _ -> navigate HelloWorldPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Sample"
                Button.onClick (fun _ -> navigate SamplePage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Dynamic Attribute"
                Button.onClick (fun _ -> navigate DynamicAttributePage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Styling"
                Button.onClick (fun _ -> navigate StylingPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Nested Component"
                Button.onClick (fun _ -> navigate NestedComponentPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Html Tags"
                Button.onClick (fun _ -> navigate HtmlTagsPage)
            ]

            match route with
            | HelloWorldPage -> HelloWorldPage.view ()
            | SamplePage -> SamplePage.view ()
            | DynamicAttributePage -> DynamicAttributePage.view ()
            | StylingPage -> StylingPage.view ()
            | NestedComponentPage -> NestedComponentPage.view ()
            | HtmlTagsPage -> HtmlTagsPage.view ()
        ]