module AlphaConnect.Client.Route

open AlphaConnect.Client.Features.Users
open AlphaConnect.Client.Features.Security

type Page =
    | HomePage
    | LoginPage
    | UserPage of UserRoute
    | NotFound

let ofUrl (segments: string list) =
    match segments with
    | [] ->
        HomePage

    | UserRoute params'  ->
        UserPage params'

    | [ "login" ] ->
        LoginPage

    | _ ->
        NotFound

// let navigate dispatch (page: Page) =
//     match page with
//     | Home ->
//         "/"

//     | Login ->
//         "/login"

//     | User page ->
//         Users.Route.navigate page

//     | NotFound ->
//         "/not-found"