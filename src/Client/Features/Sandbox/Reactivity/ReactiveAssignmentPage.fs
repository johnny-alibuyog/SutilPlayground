namespace SutilPlayground.Client.Features.Sandbox.Reactivity

module ReactiveAssignmentPage =

    open SutilPlayground.Client.Components.Button
    open Sutil
    open Sutil.CoreElements

    let view () =
        let count = Store.make 0

        Html.div [
            disposeOnUnmount [ count ]

            Html.div [
                Attr.classes [ "block" ]
                Bind.el (count, (fun c -> Html.text $"Count: {c}"))
            ]

            Html.div [
                Attr.classes [ "block" ]
                Button.create [
                    Button.onClick (fun _ -> count <~= (fun c -> c + 1))
                    Button.text "+"
                ]
                Button.create [
                    Button.onClick (fun _ -> count <~= (fun c -> c - 1))
                    Button.text "-"
                ]
            ]
        ]