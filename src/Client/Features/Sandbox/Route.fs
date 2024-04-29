namespace AlphaConnect.Client.Features.Sandbox

type Route =
    | IntroRoute of Intro.Route
    | ReactivityRoute of Reactivity.Route

module Route =
    let ofUrl segments =
        match segments with
        | Intro.Route.IsIntro route -> IntroRoute route
        | Reactivity.Route.IsReactivity route -> ReactivityRoute route
        | _ -> IntroRoute Intro.SamplePage

    let asUrl route =
        match route with
        | IntroRoute page -> Intro.Route.asUrl page
        | ReactivityRoute page -> Reactivity.Route.asUrl page

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsSandbox|_|) segments =
        match segments with
        | "sandbox" :: _ -> Some(ofUrl segments)
        | _ -> None