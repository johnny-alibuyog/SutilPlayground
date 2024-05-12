namespace AlphaConnect.Client.Features.Users

module Layout =
    open Sutil
    open AlphaConnect.Client.Components.Button

    let view navigator (route: Route) =

        let navigate = Route.navigate navigator

        Html.div [
            button.render [
                button.variant.ghost
                button.size.default'
                button.text "User List"
                button.onClick (fun _ -> navigate (ListPage { page = 1; size = 10 }))
            ]

            button.render [
                button.variant.outline
                button.size.default'
                button.text "User Profile"
                button.onClick (fun _ -> navigate (ProfilePage { userId = 1 }))
            ]

            button.render [
                button.variant.link
                button.text "User List"
                button.onClick (fun _ -> navigate (ListPage { page = 1; size = 10 }))
            ]

            match route with
            | ListPage params' -> ListPage.view params'
            | ProfilePage params' -> ProfilePage.view params'
        ]