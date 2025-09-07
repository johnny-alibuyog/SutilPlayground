namespace SutilPlayground.Client.Features.Sandbox.Intro

module StylingPage =

    open Sutil
    open Sutil.Styling
    open type Feliz.length

    let view () =
        let css = [
            rule "p" [
                Css.color "purple"
                Css.fontFamily "'Comic Sans MS', cursive, sans-serif'"
                Css.fontSize (em 20.0)
            ]
        ]

        Html.p [
            Attr.text "Styled"
        ]
        |> withStyle css
