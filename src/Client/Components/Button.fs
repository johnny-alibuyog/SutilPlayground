namespace AlphaConnect.Client.Components

module Button =

    open Sutil
    open Sutil.Core

    type ButtonAttribute =
        | Variant of string list
        | Size of string list
        | Text of string
        | OnClick of (unit -> unit)

    type ButtonProperty = {
        classes: string list
        elements: SutilElement list
    }

    type ButtonVariantEngine() =
        member inline this.default' =
            Variant [ "bg-primary"; "text-primary-foreground"; "hover:bg-primary/90" ]

        member inline this.destructive =
            Variant [ "bg-destructive"; "text-destructive-foreground"; "hover:bg-destructive/90" ]

        member inline this.outline =
            Variant [
                "border"
                "border-input"
                "bg-background"
                "hover:bg-accent"
                "hover:text-accent-foreground"
            ]

        member inline this.secondary =
            Variant [ "bg-secondary"; "text-secondary-foreground"; "hover:bg-secondary/80" ]

        member inline this.ghost =
            Variant [ "hover:bg-accent"; "hover:text-accent-foreground" ]

        member inline this.link =
            Variant [ "text-primary"; "underline-offset-4"; "hover:underline" ]

    type ButtonSizeEngine() =
        member inline this.default' = Size [ "h-10"; "px-4"; "py-2" ]

        member inline this.small = Size [ "h-9"; "rounded-md"; "px-3" ]

        member inline this.large = Size [ "h-11"; "rounded-md"; "px-8" ]

        member inline this.icon = Size [ "h-10"; "w-10" ]

    module ButtonProperty =
        let private withAttribute (prop: ButtonProperty) (attr: ButtonAttribute) =
            match attr with
            | Variant variant -> {
                prop with
                    classes = prop.classes @ variant
              }
            | Size size -> {
                prop with
                    classes = prop.classes @ size
              }
            | Text text -> {
                prop with
                    elements = prop.elements @ [ Html.text text ]
              }
            | OnClick event -> {
                prop with
                    elements = prop.elements @ [ Ev.onClick (fun _ -> event ()) ]
              }

        let private baseClasses = [
            "inline-flex"
            "items-center"
            "justify-center"
            "whitespace-nowrap"
            "rounded-md"
            "text-sm"
            "font-medium"
            "ring-offset-background"
            "transition-colors"
            "focus-visible:outline-none"
            "focus-visible:ring-2"
            "focus-visible:ring-ring"
            "focus-visible:ring-offset-2"
            "disabled:pointer-events-none"
            "disabled:opacity-50"
        ]

        let ofAttributes (attributes: ButtonAttribute list) =
            attributes |> List.fold withAttribute { classes = baseClasses; elements = [] }

        let asElements (property: ButtonProperty) =
            [ Attr.classes property.classes ] @ property.elements

    type ButtonEngine() =
        member inline this.variant = ButtonVariantEngine()

        member inline this.size = ButtonSizeEngine()

        member inline this.text t = Text t

        member inline this.onClick e = OnClick e

        member inline this.render attributes =
            ButtonProperty.ofAttributes attributes
            |> ButtonProperty.asElements
            |> Html.button

        member inline this.default' attrs =
            this.render ([ this.variant.default'; this.size.default' ] @ attrs)

    [<AutoOpen>]
    module EngineContainer =
        let button = ButtonEngine()