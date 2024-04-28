namespace AlphaConnect.Client.Features.Users

[<AutoOpen>]
module Route =
    open Sutil.Router

    type UserRoute =
        | ProfilePage of ProfilePage.Params
        | ListPage of ListPage.Params

    module UserRoute =
        let ofUrl segments =
            match segments with
            | [ "users"; Route.Int userId ] -> ProfilePage({ userId = userId })

            | [ "users"; Route.Query [ "page", Route.Int page; "size", Route.Int size ] ] ->
                ListPage({ page = page; size = size })

            | _ -> ListPage({ page = 1; size = 10 })

        let asUrl route =
            match route with
            | ProfilePage({ userId = userId }) -> $"/users/{userId}"

            | ListPage({ page = page; size = size }) -> $"/users?page={page}&size={size}"

        let navigate navigate route = route |> asUrl |> navigate

    let (|UserRoute|_|) segments =
        match segments with
        | "users" :: _ -> Some(UserRoute.ofUrl segments)
        | _ -> None

module UserLayout =
    open Sutil
    open AlphaConnect.Client.Components.Button

    let render navigator (route: UserRoute) =

        let navigate = UserRoute.navigate navigator

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
            | ListPage params' -> ListPage.render params'
            | ProfilePage params' -> ProfilePage.render params'
        ]