namespace AlphaConnect.Client.Features.Sandbox.Transitions

module TransitionPage =
    open Sutil
    open Sutil.CoreElements
    open Sutil.Transition

    let view () =
        let hasFaded = Store.make true
        let hasFlown = Store.make true
        let hasShown = Store.make true

        let f (something: int) (anotherThing: int) = something + anotherThing

        Html.div [
            disposeOnUnmount [ hasFaded ]

            Html.div [
                Html.label [
                    Html.input [
                        Attr.type' "checkbox"
                        Bind.attr ("checked", hasFaded)
                    ]
                    Bind.el (hasFaded, function
                        | false -> Html.text " Show"
                        | true -> Html.text " Hide"
                    )
                ]
                Html.p [ Html.text "Fades in and out" ]
                    |> transition [ InOut fade ] hasFaded
            ]

            Html.div [
                Html.label [
                    Html.input [
                        Attr.type' "checkbox"
                        Bind.attr ("checked", hasFlown)
                    ]
                    Bind.el (hasFlown, function
                        | false -> Html.text " Fly in"
                        | true -> Html.text " Fly out"
                    )
                ]
                Html.p [ Html.text "Flies in and out" ]
                |> transition [ InOut(fly |> withProps [ Duration 2000.0; Y 200.0 ]) ] hasFlown
            ]

            Html.div [
                Html.label [
                    Html.input [
                        Attr.type' "checkbox"
                        Bind.attr ("checked", hasShown)
                    ]
                    Bind.el (hasShown, function
                        | false -> Html.text " Show"
                        | true -> Html.text " Hide"
                    )
                ]

                let flyIn = fly |> withProps [ Duration 2000.0; Y 200.0 ]
                let fadeOut = fade |> withProps [ Duration 2000.0 ]

                Html.p [ Html.text "Shows and hides" ]
                |> transition [ In flyIn; Out fadeOut ] hasShown
            ]

        ]