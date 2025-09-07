namespace SutilPlayground.Client.Features.Users

type Route =
    | ProfilePage of ProfilePage.Parameters
    | ListPage of ListPage.Parameters


module Route =
    open Sutil.Router
    open SutilPlayground.Client.Env.Navigation

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [ Route.Int userId ] -> ProfilePage({ userId = userId })
        | [ Route.Query [ "page", Route.Int page; "size", Route.Int size ] ] -> ListPage { page = page; size = size }
        | _ -> ListPage { page = 1; size = 10 }

    let asUrl route =
        match route with
        | ProfilePage({ userId = userId }) -> $"/users/{userId}"
        | ListPage({ page = page; size = size }) -> $"/users?page={page}&size={size}"

    let navigate (env: #INavigator) route = route |> asUrl |> env.navigate

    let (|IsUser|_|) (segments: UrlSegments) =
        match segments with
        | "users" :: s -> Some(ofUrl s)
        | _ -> None