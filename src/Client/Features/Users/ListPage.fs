namespace AlphaConnect.Client.Features.Users

module ListPage =

    open Sutil

    type Params = {
        page: int
        size: int
    }

    let render (params': Params) =
        Html.div [
            Html.h1 [
                Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
                Html.text "Users"
            ]
            Html.p [
                Html.text $"Page: {params'.page}, Size: {params'.size}"
            ]
        ]

