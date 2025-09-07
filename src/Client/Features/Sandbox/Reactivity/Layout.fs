namespace SutilPlayground.Client.Features.Sandbox.Reactivity

module Layout =
    open Sutil
    open SutilPlayground.Client.Components.Button
    open SutilPlayground.Client.Env.Navigation

    let view (env: #INavigator) (route: Route) =

        let navigate = Route.navigate env

        Html.div [

            Html.h1 [
                Attr.classes [ "text-4xl"; "font-bold"; "text-primary" ]
                Html.text "Reactivity"
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Reactive Assignments"
                Button.onClick (fun _ -> navigate ReactiveAssignmentPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Reactive Declarations"
                Button.onClick (fun _ -> navigate ReactiveDeclarationPage)
            ]

            Button.create [
                Button.variant.ghost
                Button.size.default'
                Button.text "Reactive Statements"
                Button.onClick (fun _ -> navigate ReactiveStatementPage)
            ]

            match route with
            | ReactiveAssignmentPage -> ReactiveAssignmentPage.view ()
            | ReactiveDeclarationPage -> ReactiveDeclarationPage.view ()
            | ReactiveStatementPage -> ReactiveStatementPage.view ()
        ]