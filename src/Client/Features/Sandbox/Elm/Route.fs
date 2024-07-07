namespace AlphaConnect.Client.Features.Sandbox.Elm

type Route =
    | ElmishCounterPage

module Route =
    open AlphaConnect.Client.Context.Router
    open AlphaConnect.Client.Context.Navigator

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [ "emish-counter" ] -> ElmishCounterPage
        | _ -> ElmishCounterPage

    let asUrl route =
        match route with
        | ElmishCounterPage -> "/sandbox/elm/emish-counter"

    let navigate (env: #INavigator) route = route |> asUrl |> env.navigate

    let (|IsElm|_|) (segments: UrlSegments) =
        match segments with
        | "sandbox" :: "elm" :: s -> Some(ofUrl s)
        | _ -> None