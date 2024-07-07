namespace AlphaConnect.Client.Features.Users

module ListPage =

    open Sutil

    type Parameters = {
        page: int
        size: int
    }

    let view ({ page = page; size = size }: Parameters) =

        Html.div [
            Html.h1 [
                Attr.classes [ "text-2xl"; "font-bold"; "text-primary" ]
                Html.text "Users"
            ]
            Html.p [
                Html.text $"Page: {page}, Size: {size}"
            ]
        ]