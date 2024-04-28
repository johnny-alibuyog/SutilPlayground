namespace AlphaConnect.Client

module Index =

    open Browser
    open Components.Button
    open AlphaConnect.Client.Features.Dashboard
    open AlphaConnect.Client.Features.Home
    open AlphaConnect.Client.Features.Security
    open AlphaConnect.Client.Features.Users
    open Sutil
    open Sutil.CoreElements
    open Sutil.Router

    type Model = { CurrentPage: Route.Page }

    type Message =
        | Navigate of string
        | SetPage of Route.Page

    let init () =
        let currentPage =
            window.location
            |> Router.getCurrentUrl
            |> Route.ofUrl

        { CurrentPage = currentPage }, Cmd.none

    let update (message: Message) (model: Model) : Model * Cmd<Message> =
        match message with
        | Navigate path ->
            model, Router.navigate $"/#{path}"

        | SetPage page ->
            { model with CurrentPage = page }, Cmd.none

    let render () =
        let model, dispatch =
            () |> Store.makeElmish init update ignore

        let routerSubscription =
            Navigable.listenLocation (Router.getCurrentUrl, Route.ofUrl >> SetPage >> dispatch)

        let navigate path =
            dispatch (Navigate path)

        Html.div [
            disposeOnUnmount [ model ]
            unsubscribeOnUnmount [ routerSubscription ]

            Html.div [
                Attr.classes [ "flex"; "min-h-screen"; "w-full"; "flex-col"; "bg-muted/40"; ]
                Html.aside [
                    Attr.classes [ "fixed"; "inset-y-0"; "left-0"; "z-10"; "hidden"; "w-14"; "flex-col"; "border-r"; "bg-background"; "sm:flex" ]
                    Html.nav [
                        Attr.classes [ "flex"; "flex-col"; "items-center"; "gap-4"; "px-2"; "sm:py-5" ]
                        button.render [
                            button.variant.link
                            button.size.icon
                            button.text "Home"
                            button.onClick (fun _ -> navigate "/")
                        ]
                        button.render [
                            button.variant.link
                            button.size.icon
                            button.text "Login"
                            button.onClick (fun _ -> navigate "/login")
                        ]
                        button.render [
                            button.variant.destructive
                            button.size.small
                            button.text "User"
                            button.onClick (fun _ ->
                                ListPage({ page = 1; size = 10 })
                                |> UserRoute.asUrl
                                |> navigate
                            )
                        ]
                    ]
                ]
                Html.div [
                    Attr.classes [ "flex"; "flex-col"; "sm:gap-4"; "sm:py-4"; "sm:pl-14" ]
                    Html.header [
                        Attr.classes [ "sticky"; "top-0"; "z-30"; "flex"; "h-14"; "items-center"; "gap-4"; "border-b"; "bg-background"; "px-4"; "sm:static"; "sm:h-auto"; "sm:border-0"; "sm:bg-transparent"; "sm:px-6" ]
                        // Html.h1 [
                        //     Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
                        //     Html.text "Elmish Fable"
                        // ]

                        Bind.el (model .> _.CurrentPage, fun page ->
                            match page with
                            | Route.HomePage ->
                                HomePage.render ()

                            | Route.LoginPage ->
                                LoginPage.render ()

                            | Route.UserPage page ->
                                UserLayout.render navigate page

                            | Route.NotFound ->
                                Html.h1 "Not found!"
                        )
                    ]
                ]
            ]
        ]
