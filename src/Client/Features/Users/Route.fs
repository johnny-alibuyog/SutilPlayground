namespace AlphaConnect.Client.Features.Users

open Sutil.Router

type Route =
    | ProfilePage of ProfilePage.Parameters
    | ListPage of ListPage.Parameters

module Route =
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

    let (|IsUser|_|) segments =
        match segments with
        | "users" :: _ -> Some(ofUrl segments)
        | _ -> None