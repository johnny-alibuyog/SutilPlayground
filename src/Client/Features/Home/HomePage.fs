namespace SutilPlayground.Client.Features.Home

module HomePage =

    open Sutil

    [<AutoOpen>]
    module Types =
        type Messsage =
            | DoSomething of string
            | DoSomethingElse of int

        type Model = {
            something: string
            somethingElse: int
        }

        let init () = {
            something = "something"
            somethingElse = 0
        }


    let update (model: Model) (message: Messsage) =
        match message with
        | DoSomething something ->
            { model with something = something }, Cmd.none
        | DoSomethingElse somethingElse ->
            { model with somethingElse = somethingElse }, Cmd.none

    let render () =
        Html.h1 [
            Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
            Html.text "Home"
        ]
