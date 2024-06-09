namespace AlphaConnect.Client.Features.Sandbox.Intro

module HtmlTagsPage =
    open Sutil

    let stringOfHtml = "here's some <strong>HTML!!!</strong>"

    let view () =
        Html.p [
            Html.parse stringOfHtml
        ]
