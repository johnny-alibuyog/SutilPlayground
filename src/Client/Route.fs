namespace SutilPlayground.Client

open SutilPlayground.Client.Features

type Route =
    | HomePage
    | LoginPage
    | UserRoute of Users.Route
    | SandboxRoute of Sandbox.Route
    | NotFound

module Route =
    open SutilPlayground.Client.Env.Navigation

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [] -> HomePage
        | Users.Route.IsUser route -> UserRoute route
        | Sandbox.Route.IsSandbox route -> SandboxRoute route
        | [ "login" ] -> LoginPage
        | _ -> NotFound