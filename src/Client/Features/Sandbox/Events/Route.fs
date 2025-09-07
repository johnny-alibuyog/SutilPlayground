namespace SutilPlayground.Client.Features.Sandbox.Events

type Route =
    | EventModifiersPage

module Route =
    open SutilPlayground.Client.Env.Navigation

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