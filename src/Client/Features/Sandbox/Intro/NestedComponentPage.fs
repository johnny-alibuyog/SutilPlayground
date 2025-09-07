namespace SutilPlayground.Client.Features.Sandbox.Intro

module NestedComponentPage =
    open Sutil
    open Sutil.Styling
    open type Feliz.length

    let Nested() =
        Html.p [
            Html.text "...don't affect this element"
        ]
        |> withStyle []

    let view () =
        let css = [
            rule "p" [
                Css.color "orange"
                Css.fontFamily "'Comic Sans MS', cursive"
                Css.fontSize (em 2.0)
            ]
        ]

        Html.div [
            Html.p [
                Attr.text "These styles..."
            ]
            Nested()
        ]
        |> withStyle css