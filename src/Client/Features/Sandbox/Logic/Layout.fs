namespace AlphaConnect.Client.Features.Sandbox.Logic

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button
    open AlphaConnect.Client.Context.Navigator

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [

            Html.h1 [
                Attr.classes [ "text-4xl"; "font-bold"; "text-primary" ]
                Html.text "Logic"
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "If Block"
                button.onClick (fun _ -> navigate IfBlockPage)
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Else Block"
                button.onClick (fun _ -> navigate ElseBlockPage)
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Else If Block"
                button.onClick (fun _ -> navigate ElseIfBlockPage)
            ]

            match route with
            | IfBlockPage -> IfBlockPage.view ()
            | ElseBlockPage -> ElseBlockPage.view ()
            | ElseIfBlockPage -> ElseIfBlockPage.view ()
        ]