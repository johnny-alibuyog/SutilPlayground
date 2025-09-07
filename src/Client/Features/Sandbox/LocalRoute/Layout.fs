namespace SutilPlayground.Client.Features.Sandbox.LocalRoute

module Layout =
    open Sutil
    open SutilPlayground.Client.Components.Button

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

            Button.create [
                Button.text "Page 1"
                Button.onClick (fun _ -> dispatch (SetPage Page1))
            ]

            Button.create [
                Button.text "Page 2"
                Button.onClick (fun _ -> dispatch (SetPage Page2))
            ]

            Bind.el (model, (fun m ->
                match m.page with
                | Page1 -> Page1.view()
                | Page2 -> Page2.view()
            ))
        ]