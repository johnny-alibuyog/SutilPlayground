namespace SutilPlayground.Client.Features.Sandbox.Intro

module SamplePage =

    open Sutil
    open Sutil.CoreElements
    open SutilPlayground.Client.Components.Button

    [<AutoOpen>]
    module Types =

        type Model = { counter: int }

        module Model =
            let init () = { counter = 0 }

        type Message =
            | Increment
            | Decrement

    let update (message: Message) (model: Model) =

        match message with
        | Increment -> {
            model with
                counter = model.counter + 1
          }
        | Decrement -> {
            model with
                counter = model.counter - 1
          }

    let view () =

        let model, dispatch = () |> Store.makeElmishSimple Model.init update ignore

        Html.div [
            disposeOnUnmount [ model ]

            style [ Css.fontFamily "Arial, Helvetica, sans-serif"; Css.margin 20 ]

            Html.h1 [
                Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
                Html.text "Sample Page"
            ]

            Bind.el (model |> Store.map _.counter, (fun counter -> Html.text $"Counter: {counter}"))

            Html.div [
                Button.create [
                    Button.text "+"
                    Button.onClick (fun _ -> dispatch Increment)
                ]

                Button.create [
                    Button.text "-"
                    Button.onClick (fun _ -> dispatch Decrement)
                ]
            ]
        ]