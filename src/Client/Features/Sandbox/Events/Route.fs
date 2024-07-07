namespace AlphaConnect.Client.Features.Sandbox.Events

type Route =
    | EventModifiersPage

module Route =
    open AlphaConnect.Client.Context.Router
    open AlphaConnect.Client.Context.Navigator

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [ "event-modifiers" ] -> EventModifiersPage
        | _ -> EventModifiersPage

    let asUrl route =
        match route with
        | EventModifiersPage -> "/sandbox/events/event-modifiers"

    let navigate (env: #INavigator) route = route |> asUrl |> env.navigate

    let (|IsEvents|_|) (segments: UrlSegments) =
        match segments with
        | "sandbox" :: "events" :: s -> Some(ofUrl s)
        | _ -> None