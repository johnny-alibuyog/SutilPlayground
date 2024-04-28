module Features.Users.Route

open Sutil.Router
open Features.Users

(*
    module Route =
        type UserRoute =
            | Home
            | Login
            | User of Users.Route.Page
            | NotFound

        module UserRoute =
            let ofUrl (segments: string list) =
                match segments with
                | [] ->
                    Home

                | Users.Route.Page page  ->
                    User page

                | [ "login" ] ->
                    Login

                | _ ->
                    NotFound

            let navigate navigator (page: UserRoute) =
                match page with
                | Home ->
                    navigator "/#/"

                | Login ->
                    navigator "/#/login"

                | User page ->
                    Users.Route.navigate navigator page

                | NotFound ->
                    navigator "/not-found"
*)


// type Page =
//     | Profile of ProfilePage.Params
//     | List of ListPage.Params

// let ofUrl segments =
//     match segments with
//     | [ "users"; Route.Int userId ] ->
//         Profile(ProfilePage.Params userId)

//     | [ "users"; Route.Query [ "page", Route.Int page; "size", Route.Int size ] ] ->
//         List(ListPage.Params(page, size))

//     | _ ->
//         List(ListPage.Params(1, 10))

// let (|Page|_|) segments =
//     match segments with
//     | "users" :: _ -> Some(ofUrl segments)
//     | _ -> None