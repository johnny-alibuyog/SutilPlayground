namespace AlphaConnect.Client.Features.Sandbox.Elm

type Route =
    | ElmishCounterPage

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "elm"; "emish-counter" ] -> ElmishCounterPage
        | _ -> ElmishCounterPage

    let asUrl route =
        match route with
        | ElmishCounterPage -> "/sandbox/elm/emish-counter"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsElm|_|) segments =
        match segments with
        | "sandbox" :: "elm" :: _ -> Some(ofUrl segments)
        | _ -> None