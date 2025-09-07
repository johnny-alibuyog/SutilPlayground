namespace SutilPlayground.Client

module Env =

    [<AutoOpen>]
    module Navigation =
        type UrlSegments = string list

        type INavigator =
            abstract member navigate: path: string -> unit

        // type INavigatorEnv =
        //     abstract member navigator: INavigator

        type Navigator (dispatch) =
            interface INavigator with
                member _.navigate path = dispatch path

    type DataEnv = {
        navigator: INavigator
    }


    let dataEnv = {
        navigator = Navigator (fun _ -> ())
    }

    // module Router =
    //     open Browser
    //     open Sutil
    //     open Sutil.Router

    //     type UrlSegments = string list

    //     type IRouter =
    //         abstract member currentUrl: unit -> UrlSegments
    //         abstract member redirect: string -> Cmd<'a>
    //         abstract member subscribe: dispatch: (UrlSegments -> unit) -> (unit -> unit)

    //     type RouterEnv () =
    //         interface IRouter with
    //             member _.currentUrl () =
    //                 window.location |> Router.getCurrentUrl

    //             member _.redirect path =
    //                 Router.navigate path

    //             member _.subscribe dispatch =
    //                 Navigable.listenLocation (Router.getCurrentUrl, dispatch)
