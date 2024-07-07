namespace AlphaConnect.Client.Features.Sandbox.LocalRoute

type Route =
    | Page1
    | Page2

module Route =
    open AlphaConnect.Client.Context.Router
    open Sutil.Router
    open AlphaConnect.Client.Context.Navigator

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [ Route.Query [ "page", "page1" ] ] -> Page1
        | [ Route.Query [ "page", "Page2" ] ] -> Page2
        | _ -> Page1

    let asUrl route =
        match route with
        | Page1 -> "/sandbox/local-route/page=page1"
        | Page2 -> "/sandbox/local-route/page=page2"

    let navigate (env: #INavigator) route = route |> asUrl |> env.navigate

    let (|IsLocalRoute|_|) (segments: UrlSegments) =
        match segments with
        | "sandbox" :: "local-route" :: s -> Some(ofUrl s)
        | _ -> None