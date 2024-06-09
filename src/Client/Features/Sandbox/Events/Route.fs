namespace AlphaConnect.Client.Features.Sandbox.Events

type Route =
    | EventModifiersPage

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "events"; "event-modifiers" ] -> EventModifiersPage
        | _ -> EventModifiersPage

    let asUrl route =
        match route with
        | EventModifiersPage -> "/sandbox/events/event-modifiers"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsEvents|_|) segments =
        match segments with
        | "sandbox" :: "events" :: _ -> Some(ofUrl segments)
        | _ -> None