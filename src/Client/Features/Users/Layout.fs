namespace SutilPlayground.Client.Features.Users

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
                Button.text "User List"
                Button.onClick (fun _ -> navigate (ListPage { page = 1; size = 10 }))
            ]

            Button.create [
                Button.variant.outline
                Button.size.default'
                Button.text "User Profile"
                Button.onClick (fun _ -> navigate (ProfilePage { userId = 1 }))
            ]

            Button.create [
                Button.variant.link
                Button.text "User List"
                Button.onClick (fun _ -> navigate (ListPage { page = 1; size = 10 }))
            ]

            match route with
            | ListPage params' -> ListPage.view params'
            | ProfilePage params' -> ProfilePage.view params'
        ]