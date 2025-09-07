namespace SutilPlayground.Client.Features.Sandbox.Transitions

module TransitionWithParametersPage =
    open Sutil
    open Sutil.CoreElements
    open Sutil.Transition

    let view () =
        let visible = Store.make true
        let status = Store.make "Waiting..."

        Html.div [
            disposeOnUnmount [ visible; status ]

            Html.p [
                Attr.classes [ "block" ]
                Attr.text "Status: "
                Bind.el (status, Html.text)
            ]
            Html.label [
                Html.input [
                    type' "checkbox"
                    Bind.attr ("checked", visible)
                ]
                Html.text " Visible"
            ]

            Html.p [
                on "introstart" (fun _ -> status <~ "intro started") []
                on "introend" (fun _ -> status <~ "intro ended") []
                on "outrostart" (fun _ -> status <~ "outro started") []
                on "outroend" (fun _ -> status <~ "outro ended") []
                Html.text "Fades in and out"
            ]
            |> transition [ InOut(fly |> withProps [ Duration 2000.0; Y 200.0 ]) ] visible
        ]