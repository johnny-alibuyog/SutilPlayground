namespace SutilPlayground.Client.Features.Sandbox.Events

module EventModifiersPage =
    open Sutil

    let handleClick _ =
        Browser.Dom.window.alert "You clicked me!"

    let view () =
        Html.div [
            Html.button [
                Ev.onClick handleClick
                Html.text "Click me!"
            ]
        ]