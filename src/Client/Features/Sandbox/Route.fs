namespace AlphaConnect.Client.Features.Sandbox

type Route =
    | IntroRoute of Intro.Route
    | LocalRoute of LocalRoute.Route
    | ReactivityRoute of Reactivity.Route
    | LogicRoute of Logic.Route
    | EventsRoute of Events.Route
    | TransitionsRoute of Transitions.Route
    | ElmRoute of Elm.Route


module Route =
    let ofUrl segments =
        match segments with
        | Intro.Route.IsIntro route -> IntroRoute route
        | LocalRoute.Route.IsLocalRoute route -> LocalRoute route
        | Reactivity.Route.IsReactivity route -> ReactivityRoute route
        | Logic.Route.IsLogic route -> LogicRoute route
        | Events.Route.IsEvents route -> EventsRoute route
        | Transitions.Route.IsTransitions route -> TransitionsRoute route
        | Elm.Route.IsElm route -> ElmRoute route
        | _ -> IntroRoute Intro.SamplePage

    let asUrl route =
        match route with
        | IntroRoute page -> Intro.Route.asUrl page
        | LocalRoute page -> LocalRoute.Route.asUrl page
        | ReactivityRoute page -> Reactivity.Route.asUrl page
        | LogicRoute page -> Logic.Route.asUrl page
        | EventsRoute page -> Events.Route.asUrl page
        | TransitionsRoute page -> Transitions.Route.asUrl page
        | ElmRoute page -> Elm.Route.asUrl page

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsSandbox|_|) segments =
        match segments with
        | "sandbox" :: _ -> Some(ofUrl segments)
        | _ -> None