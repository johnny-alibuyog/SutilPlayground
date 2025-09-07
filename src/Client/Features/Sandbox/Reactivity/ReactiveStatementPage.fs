namespace SutilPlayground.Client.Features.Sandbox.Reactivity

module ReactiveStatementPage =
    open Sutil
    open Sutil.CoreElements

    let view () =
        let inc n = n + 1
        let pluralize n = if n = 1 then "" else "s"
        let count = Store.make 1

        let IsDangerouslyHigh x = x > 10

        let disposable =
            count
            |> Store.iter (fun x ->
                if (IsDangerouslyHigh x) then
                    Browser.Dom.window.alert "Dangerously high count!"
                    count <~ 9
            )

        let handleClick _ = count <~= inc

        Html.div [
            disposeOnUnmount [ count; disposable ]

            Html.button [
                onClick handleClick []
                Bind.el (count, (fun x -> Html.text $"Clicked {x} time{pluralize x}"))
            ]
        ]