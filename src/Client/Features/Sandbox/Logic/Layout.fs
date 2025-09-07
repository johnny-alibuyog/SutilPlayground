namespace SutilPlayground.Client.Features.Sandbox.Logic

module Layout =
    open Sutil
    open SutilPlayground.Client.Components.Button
    open SutilPlayground.Client.Env.Navigation

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [

            Html.h1 [
                Attr.classes [ "text-4xl"; "font-bold"; "text-primary" ]
                Html.text "Logic"
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "If Block"
                Button.onClick (fun _ -> navigate IfBlockPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Else Block"
                Button.onClick (fun _ -> navigate ElseBlockPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Else If Block"
                Button.onClick (fun _ -> navigate ElseIfBlockPage)
            ]

            match route with
            | IfBlockPage -> IfBlockPage.view ()
            | ElseBlockPage -> ElseBlockPage.view ()
            | ElseIfBlockPage -> ElseIfBlockPage.view ()
        ]