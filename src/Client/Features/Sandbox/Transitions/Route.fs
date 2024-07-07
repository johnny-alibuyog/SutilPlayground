namespace AlphaConnect.Client.Features.Sandbox.Transitions

type Route =
    | TransitionPage
    | TransitionWithParametersPage
    | AnimationPage

module Route =
    open AlphaConnect.Client.Context.Router
    open AlphaConnect.Client.Context.Navigator

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [ "transition" ] -> TransitionPage
        | [ "transition-with-parameters" ] -> TransitionWithParametersPage
        | [ "animation" ] -> AnimationPage
        | _ -> TransitionPage

    let asUrl route =
        match route with
        | TransitionPage -> "/sandbox/transitions/transition"
        | TransitionWithParametersPage -> "/sandbox/transitions/transition-with-parameters"
        | AnimationPage -> "/sandbox/transitions/animation"

    let navigate (env: #INavigator) route = route |> asUrl |> env.navigate

    let (|IsTransitions|_|) (segments: UrlSegments) =
        match segments with
        | "sandbox" :: "transitions" :: s -> Some(ofUrl s)
        | _ -> None