namespace AlphaConnect.Client

module Context =

    // type DataContext = { currentPage: Route }

    // type OperationContext = { currentPage: Route }

    // type INavigator =
    //     // abstract member changed : (unit -> string) * (string -> unit) -> unit
    //     abstract member navigate: string -> unit
    //     abstract member navigated: string -> unit

    // type Navigator() =
    //     interface INavigator with
    //         member this.navigate path = printfn "Navigating to %s" path
    //         member this.navigated path = printfn "Navigated to %s" path


    // let navigator = Navigator()

    // type IEnvA =
    //     abstract member doWorkA: unit -> unit

    // type IEnvB =
    //     abstract member doWorkB: unit -> unit

    // type IEnvC =
    //     abstract member doWorkC: unit -> unit

    // let doWorkA (env: IEnvA) = env.doWorkA ()

    // let doWorkB (env: IEnvB) = env.doWorkB ()

    // let doWork env =
    //     doWorkA env
    //     doWorkB env

    // let doWork2 (env: #IEnvA & #IEnvB & #IEnvC) = doWork env

    // type DoWorkEnv() =
    //     interface IEnvA with
    //         member this.doWorkA() = printfn "Doing work A"

    //     interface IEnvB with
    //         member this.doWorkB() = printfn "Doing work B"

    // let env = DoWorkEnv()

    // doWork env
    // // doWork2 env


    // module Db =
    //     open System

    //     type ICreate<'T> =
    //         abstract member create: 'T -> Result<Guid, string>

    //     type Create<'T>() =
    //         interface ICreate<'T> with
    //             member this.create value = Ok(Guid.NewGuid())

    // module User =

    //     module Create =
    //         open System

    //         type UserEntity = { Id: Guid; Name: string }

    //         let handler (env: #Db.ICreate<UserEntity>) (name: string) =
    //             let result = env.create { Id = Guid.NewGuid(); Name = name }

    //             match result with
    //             | Ok id -> printfn "User created with id %A" id
    //             | Error error -> printfn "Error creating user: %s" error


    module Navigator =
        type INavigator =
            abstract member navigate: path: string -> unit

        type NavigatorEnv (dispatch: string -> unit) =
            interface INavigator with
                member _.navigate path = dispatch path

    module Router =
        open Browser
        open Sutil
        open Sutil.Router

        type UrlSegments = string list

        type IRouter =
            abstract member currentUrl: unit -> UrlSegments
            abstract member redirect: string -> Cmd<'a>
            abstract member subscribe: dispatch: (UrlSegments -> unit) -> (unit -> unit)

        type RouterEnv () =
            interface IRouter with
                member _.currentUrl () =
                    window.location |> Router.getCurrentUrl

                member _.redirect path =
                    Router.navigate path

                member _.subscribe dispatch =
                    Navigable.listenLocation (Router.getCurrentUrl, dispatch)

        type IRoute<'t> =
            abstract member ofUrl: UrlSegments -> 't
            abstract member asUrl: 't -> string
            abstract member navigate: (string -> unit) -> 't -> unit
