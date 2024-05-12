namespace AlphaConnect.Client

open AlphaConnect.Client.Features

type Route =
    | HomePage
    | LoginPage
    | UserRoute of Users.Route
    | SandboxRoute of Sandbox.Route
    | NotFound

module Route =

    let ofUrl (segments: string list) =
        match segments with
        | [] -> HomePage
        | Users.Route.IsUser route -> UserRoute route
        | Sandbox.Route.IsSandbox route -> SandboxRoute route
        | [ "login" ] -> LoginPage
        | _ -> NotFound