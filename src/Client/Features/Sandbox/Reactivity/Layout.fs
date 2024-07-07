namespace AlphaConnect.Client.Features.Sandbox.Reactivity

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button
    open AlphaConnect.Client.Context.Navigator

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

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

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Reactive Declarations"
                button.onClick (fun _ -> navigate ReactiveDeclarationPage)
            ]

            button.render [
                button.variant.ghost
                button.size.default'
                button.text "Reactive Statements"
                button.onClick (fun _ -> navigate ReactiveStatementPage)
            ]

            match route with
            | ReactiveAssignmentPage -> ReactiveAssignmentPage.view ()
            | ReactiveDeclarationPage -> ReactiveDeclarationPage.view ()
            | ReactiveStatementPage -> ReactiveStatementPage.view ()
        ]