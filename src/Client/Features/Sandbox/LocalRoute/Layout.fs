namespace AlphaConnect.Client.Features.Sandbox.LocalRoute

module Layout =
    open Browser.Dom
    open Fable.Core.JS
    open Sutil
    open AlphaConnect.Client.Components.Button

    [<AutoOpen>]
    module Types =

        type Page =
            | Page1
            | Page2

        module Page =
            let query =
                function
                | Page1 -> "page=page1"
                | Page2 -> "page=page2"

        type Model = { page: Page }

        module Model =
            let init () = { page = Page1 }, Cmd.none

        type Message =
            | SetPage of Page

    let update (message: Message) (model: Model) : Model * Cmd<Message> =
        match message with
        | SetPage page ->
            // printf $"{JSON.stringify(window.location)}"
            // window.location.search <- model.page |> Page.query
            { model with page = page }, Cmd.none

    let view =

        let model, dispatch = () |> Store.makeElmish Model.init update ignore

        Html.div [
            Html.h1 [
                Attr.text "Local Route Sandbox"
            ]

            button.default' [
                button.text "Page 1"
                button.onClick (fun _ -> dispatch (SetPage Page1))
            ]

            button.default' [
                button.text "Page 2"
                button.onClick (fun _ -> dispatch (SetPage Page2))
            ]

            Bind.el (model, (fun m ->
                match m.page with
                | Page1 -> Page1.view()
                | Page2 -> Page2.view()
            ))
        ]