namespace AlphaConnect.Client

module Resources =
    open Zanaptak.TypedCssClasses

    type fa = CssClasses<"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/css/all.min.css", Naming.Verbatim>

    type tw = CssClasses<"./public/tailwind.min.css", Naming.Verbatim>
// type tw = CssClasses<"https://cdn.jsdelivr.net/npm/tailwindcss@3.4.3/base.min.css", Naming.Verbatim>
// type tw = CssClasses<"../../node_modules/tailwindcss/tailwind.css", Naming.Verbatim>

// tw.``focus:from-blue-500``
// tw.