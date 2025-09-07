namespace SutilPlayground.Client.Features.Sandbox.LocalRoute

module Page2 =
    open Sutil

    let view () =
        Html.div [
            Html.p [
                Attr.text "Page 2"
            ]
        ]