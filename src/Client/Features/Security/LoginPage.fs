namespace AlphaConnect.Client.Features.Security

module LoginPage =

    open Sutil

    let render () =
        Html.h1 [
            Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
            Html.text "Login"
        ]