namespace AlphaConnect.Client.Features.Users

module ProfilePage =

    open Sutil

    type Params = {
        userId: int
    }

    let render (params': Params) =
        let { userId = userId } = params'

        Html.div [
            Html.h1 [
                Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
                Html.text "User Profile"
            ]
            Html.p [
                Html.text $"User ID: {userId}"
            ]
        ]