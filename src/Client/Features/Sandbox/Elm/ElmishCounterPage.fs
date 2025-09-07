namespace SutilPlayground.Client.Features.Sandbox.Elm

module ElmishCounterPage =

    open Sutil
    open Sutil.Styling
    open Sutil.CoreElements

    type Model = { count: int }

    module Model =
        let init () = { count = 0 }

    type Message =
        | Increment
        | Decrement

    let update msg model =
        match msg with
        | Increment -> { model with count = model.count + 1 }
        | Decrement -> { model with count = model.count - 1 }

    let view () =
        let model, dispatch = () |> Store.makeElmishSimple Model.init update ignore

        let css = [
            rule "*" [ Css.fontFamily "Arial, Helvetica, sans-serif"; Css.margin 10 ]
            rule "count-display" [ Css.userSelectNone ]
        ]

        Html.div [
            disposeOnUnmount [ model ]

            Html.spanc "counter-display" [
                Bind.el (model |> Store.map _.count, (fun count ->
                    Html.text $"Counter: {count}"
                ))
            ]

            Html.buttonc "button" [ Html.text "-"; Ev.onClick (fun _ -> dispatch Decrement) ]
            Html.buttonc "button" [ Html.text "+"; Ev.onClick (fun _ -> dispatch Increment) ]
        ]
        |> withStyle css