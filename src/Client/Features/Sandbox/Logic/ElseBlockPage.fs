namespace AlphaConnect.Client.Features.Sandbox.Logic

module ElseBlockPage =
    open Sutil
    open Sutil.CoreElements

    [<AutoOpen>]
    module Types =
        type User = { loggedIn: bool }

        module User =
            let init = { loggedIn = false }
            let toggle user = { user with loggedIn = not user.loggedIn }

    let view () =
        let user = Store.make User.init

        let toggle _ = user |> Store.modify User.toggle

        Html.div [
            Html.div [
                disposeOnUnmount [ user ]

                Bind.el (user .> _.loggedIn, fun loggedIn ->
                    if loggedIn then
                        Html.button [ Ev.onClick toggle; Html.text "Log Out" ]
                    else
                        Html.button [ Ev.onClick toggle; Html.text "Log In" ]
                )
            ]
        ]