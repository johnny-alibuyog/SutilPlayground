namespace AlphaConnect.Client.Features.Sandbox.LocalRoute

module Page1 =
    open Sutil

    let view () =
        Html.div [
            Html.p [
                Attr.text "Page 1"
            ]
        ]