namespace SutilPlayground.Client.Features.Dashboard

module DashboardPage =

    open Sutil

    let view () =
        Html.h1 [
            Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
            Html.text "Dashboard"
        ]