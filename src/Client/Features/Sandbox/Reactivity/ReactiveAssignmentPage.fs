namespace AlphaConnect.Client.Features.Sandbox.Reactivity

module ReactiveAssignmentPage =

    open AlphaConnect.Client.Components.Button
    open Sutil
    open Sutil.CoreElements

    let view() =
        let count = Store.make 0

        Html.div [
            disposeOnUnmount [ count ]

            Html.div [
                Attr.classes ["block"]
                Bind.el (count, (fun c -> Html.text $"Count: {c}"))
            ]

            Html.div [
                Attr.classes ["block"]
                button.default' [
                    button.onClick (fun _ -> count <~= (fun c -> c + 1))
                    button.text "+"
                ]
                button.default' [
                    button.onClick (fun _ -> count <~= (fun c -> c - 1))
                    button.text "-"
                ]
            ]
        ]

