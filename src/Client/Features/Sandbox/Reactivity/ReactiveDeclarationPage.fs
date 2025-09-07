namespace SutilPlayground.Client.Features.Sandbox.Reactivity

module ReactiveDeclarationPage =
    open Sutil
    open Sutil.CoreElements

    let view () =
        let count = Store.make 1
        let doubled = count |> Store.map ((*) 2)
        let quadrupled = doubled |> Store.map ((*) 2)

        let handleClick _ = count |> Store.modify ((+) 1)

        Html.div [
            disposeOnUnmount [ count ]

            Html.button [
                Attr.classes [ "block" ]
                Ev.onClick handleClick
                Bind.el (count, (fun x -> Attr.text $"Count: {x}"))
            ]

            Html.p [
                Attr.classes [ "block" ]
                Bind.el2 count doubled (fun (x, y) -> Attr.text $"{x} * 2 = {y}")
            ]

            Html.p [
                Attr.classes [ "block" ]
                Bind.el2 doubled quadrupled (fun (x, y) -> Attr.text $"{x} * 2 = {y}")
            ]
        ]