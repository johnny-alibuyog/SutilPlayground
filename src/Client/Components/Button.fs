namespace SutilPlayground.Client.Components

module Button =

    open Sutil

    type Classes = string list

    type Attribute =
        | Variant of Classes
        | Size of Classes
        | Text of string
        | OnClick of (unit -> unit)
        | Disabled of bool

    type Property = {
        variant: Classes
        size: Classes
        text: string
        onClick: unit -> unit
        disabled: bool
    }

    type VariantEngine() =
        member inline _.default' = Variant [ "bg-primary"; "text-primary-foreground"; "hover:bg-primary/90" ]

        member inline _.destructive = Variant [ "bg-destructive"; "text-destructive-foreground"; "hover:bg-destructive/90" ]

        member inline _.outline = Variant [ "border"; "border-input"; "bg-background"; "hover:bg-accent"; "hover:text-accent-foreground" ]

        member inline _.secondary = Variant [ "bg-secondary"; "text-secondary-foreground"; "hover:bg-secondary/80" ]

        member inline _.ghost = Variant [ "hover:bg-accent"; "hover:text-accent-foreground" ]

        member inline _.link = Variant [ "text-primary"; "underline-offset-4"; "hover:underline" ]

        member inline _.unwrap attribute =
            match attribute with
            | Variant classes -> classes
            | _ -> []

    type SizeEngine() =
        member inline _.default' = Size [ "h-10"; "px-4"; "py-2" ]

        member inline _.small = Size [ "h-9"; "rounded-md"; "px-3" ]

        member inline _.large = Size [ "h-11"; "rounded-md"; "px-8" ]

        member inline _.icon = Size [ "h-10"; "w-10" ]

        member inline _.unwrap attribute =
            match attribute with
            | Size classes -> classes
            | _ -> []

    module Property =
        let inline withAttribute (prop: Property) (attr: Attribute) =
            match attr with
            | Variant variant ->
                { prop with variant = variant }
            | Size size ->
                { prop with size = size }
            | Text text ->
                { prop with text = text }
            | OnClick onClick ->
                { prop with onClick = onClick}
            | Disabled disabled ->
                { prop with disabled = disabled }

        let inline create (variant: VariantEngine) (size: SizeEngine) =
            {
                variant = variant.unwrap variant.default'
                size =  size.unwrap size.default'
                text = ""
                onClick = ignore
                disabled = false
            }

    type ButtonEngine() =
        member inline private this.baseClasses = [
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
        member inline this.variant = VariantEngine()

        member inline this.size = SizeEngine()

        member inline this.text t = Text t

        member inline this.onClick e = OnClick e

        member inline this.disabled d = Disabled d

        member inline this.create attrs =
            let prop = attrs |> List.fold Property.withAttribute (Property.create this.variant this.size)

            Html.button [
                Attr.classes (names = this.baseClasses @ prop.variant @ prop.size)
                Attr.text (s = prop.text)
                Ev.onClick (handler = fun _ -> prop.onClick())
                Attr.disabled (value = prop.disabled)
            ]

    let Button = ButtonEngine()

