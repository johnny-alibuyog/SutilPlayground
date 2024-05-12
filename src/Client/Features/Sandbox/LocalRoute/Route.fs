namespace AlphaConnect.Client.Features.Sandbox.LocalRoute

open Sutil.Router

type Route =
    | Page1
    | Page2

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "local-route"; Route.Query [ "page", "page1" ] ] -> Page1
        | [ "sandbox"; "local-route"; Route.Query [ "page", "Page2" ] ] -> Page2
        | _ -> Page1

    let asUrl route =
        match route with
        | Page1 -> "/sandbox/local-route/page=page1"
        | Page2 -> "/sandbox/local-route/page=page2"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsLocalRoute|_|) segments =
        match segments with
        | "sandbox" :: "local-route" :: _ -> Some(ofUrl segments)
        | _ -> None