namespace AlphaConnect.Client

module Index =

    open AlphaConnect.Client.Features
    open Components.Button
    open Context.Router
    open Context.Navigator
    open Sutil
    open Sutil.CoreElements

    let router: IRouter = RouterEnv()

    [<AutoOpen>]
    module Types =
        type Model = { currentPage: Route }

        type Message =
            | Navigate of string
            | SetPage of Route

        let init () =
            let currentPage = router.currentUrl () |> Route.ofUrl

            { currentPage = currentPage }, Cmd.none

    let update (message: Message) (model: Model) : Model * Cmd<Message> =
        match message with
        | Navigate path -> model, router.redirect $"/#{path}"
        | SetPage page -> { model with currentPage = page }, Cmd.none

    let view () =
        let model, dispatch = () |> Store.makeElmish init update ignore

        let navigationSubscription = router.subscribe (Route.ofUrl >> SetPage >> dispatch)

        let navigator: INavigator = NavigatorEnv (Navigate >> dispatch)

        Html.div [
            disposeOnUnmount [ model ]
            unsubscribeOnUnmount [ navigationSubscription ]

            Html.div [
                Attr.classes [ "flex"; "min-h-screen"; "w-full"; "flex-col"; "bg-muted/40" ]
                Html.aside [
                    Attr.classes [
                        "fixed"
                        "inset-y-0"
                        "left-0"
                        "z-10"
                        "hidden"
                        "w-14"
                        "flex-col"
                        "border-r"
                        "bg-background"
                        "sm:flex"
                    ]
                    Html.nav [
                        Attr.classes [ "flex"; "flex-col"; "items-center"; "gap-4"; "px-2"; "sm:py-5" ]
                        button.render [
                            button.variant.link
                            button.size.icon
                            button.text "Home"
                            button.onClick (fun _ -> navigator.navigate "/")
                        ]
                        button.render [
                            button.variant.link
                            button.size.icon
                            button.text "Login"
                            button.onClick (fun _ -> navigator.navigate "/login")
                        ]
                        button.render [
                            button.variant.destructive
                            button.size.small
                            button.text "User"
                            button.onClick (fun _ ->
                                Users.Route.ListPage({ page = 1; size = 10 })
                                |> Users.Route.asUrl
                                |> navigator.navigate
                            )
                        ]
                        button.render [
                            button.variant.destructive
                            button.size.small
                            button.text "SndBx"
                            button.onClick (fun _ ->
                                Sandbox.Intro.Route.SamplePage
                                |> Sandbox.Intro.Route.asUrl
                                |> navigator.navigate
                            )
                        ]
                    ]
                ]
                Html.div [
                    Attr.classes [ "flex"; "flex-col"; "sm:gap-4"; "sm:py-4"; "sm:pl-14" ]
                    Html.header [
                        Attr.classes [
                            "sticky"
                            "top-0"
                            "z-30"
                            "flex"
                            "h-14"
                            "items-center"
                            "gap-4"
                            "border-b"
                            "bg-background"
                            "px-4"
                            "sm:static"
                            "sm:h-auto"
                            "sm:border-0"
                            "sm:bg-transparent"
                            "sm:px-6"
                        ]
                        Bind.el (model .> _.currentPage, fun page ->
                            match page with
                            | Route.HomePage -> Home.HomePage.render ()
                            | Route.LoginPage -> Security.LoginPage.render ()
                            | Route.UserRoute route -> Users.Layout.view navigator route
                            | Route.SandboxRoute route -> Sandbox.Layout.view navigator route
                            | Route.NotFound -> Html.h1 "Not found!"
                        )
                    ]
                ]
            ]
        ]