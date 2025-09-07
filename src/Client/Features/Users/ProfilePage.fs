namespace SutilPlayground.Client.Features.Users

module ProfilePage =

    open Sutil

    type Parameters = { userId: int }

    let view ({ userId = userId }: Parameters) =

        Html.div [
            Html.h1 [
                Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
                Html.text "User Profile"
            ]
            Html.p [ Html.text $"User ID: {userId}" ]
        ]