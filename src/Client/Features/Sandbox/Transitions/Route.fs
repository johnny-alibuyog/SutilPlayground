namespace AlphaConnect.Client.Features.Sandbox.Transitions

type Route =
    | TransitionPage
    | TransitionWithParametersPage
    | AnimationPage

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "transitions"; "transition" ] -> TransitionPage
        | [ "sandbox"; "transitions"; "transition-with-parameters" ] -> TransitionWithParametersPage
        | [ "sandbox"; "transitions"; "animation" ] -> AnimationPage
        | _ -> TransitionPage

    let asUrl route =
        match route with
        | TransitionPage -> "/sandbox/transitions/transition"
        | TransitionWithParametersPage -> "/sandbox/transitions/transition-with-parameters"
        | AnimationPage -> "/sandbox/transitions/animation"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsTransitions|_|) segments =
        match segments with
        | "sandbox" :: "transitions" :: _ -> Some(ofUrl segments)
        | _ -> None