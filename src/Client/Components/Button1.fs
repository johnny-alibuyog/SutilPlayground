namespace SutilPlayground.Client.Components

module Button1 =
    open Sutil

    type Variant =
        | Default
        | Destructive
        | Outline
        | Secondary
        | Ghost
        | Link

    type Size =
        | Default
        | Small
        | Large
        | Icon

    type Attribute =
        | Variant of Variant
        | Size of Size
        | Text of string
        | OnClick of (unit -> unit)

    type Property = {
        variant: string list
        size: string list
        text: string
        onClick: unit -> unit
    }

    module Variant =
        let toPropertyValue value=
            match value with
            | Variant.Default -> [ "bg-primary"; "text-primary-foreground"; "hover:bg-primary/90" ]
            | Variant.Destructive -> [ "bg-destructive"; "text-destructive-foreground"; "hover:bg-destructive/90" ]
            | Variant.Outline -> [ "border"; "border-input"; "bg-background"; "hover:bg-accent"; "hover:text-accent-foreground" ]
            | Variant.Secondary -> [ "bg-secondary"; "text-secondary-foreground"; "hover:bg-secondary/80" ]
            | Variant.Ghost -> [ "hover:bg-accent"; "hover:text-accent-foreground" ]
            | Variant.Link -> [ "text-primary"; "underline-offset-4"; "hover:underline" ]

    module Size =
        let toPropertyValue value=
            match value with
            | Size.Default -> [ "h-10"; "px-4"; "py-2" ]
            | Size.Small -> [ "h-9"; "rounded-md"; "px-3" ]
            | Size.Large -> [ "h-11"; "rounded-md"; "px-8" ]
            | Size.Icon -> [ "h-10"; "w-10" ]

    module Property =

        let default' = {
            variant = Variant.toPropertyValue Variant.Default
            size = Size.toPropertyValue Size.Default
            text = ""
            onClick = ignore
        }

        let withAttribute (prop: Property) (attr: Attribute) =
            match attr with
            | Variant variant -> { prop with variant = Variant.toPropertyValue variant }
            | Size size -> { prop with size = Size.toPropertyValue size }
            | Text text -> { prop with text = text }
            | OnClick onClick -> { prop with onClick = onClick }

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

        member inline this.variant = {|
            default' = Variant Variant.Default
            destructive = Variant Variant.Destructive
            outline = Variant Variant.Outline
            secondary = Variant Variant.Secondary
            ghost = Variant Variant.Ghost
            link = Variant Variant.Link
        |}

        member inline this.size = {|
            default' = Size Size.Default
            small = Size Size.Small
            large = Size Size.Large
            icon = Size Size.Icon
        |}

        member inline this.text value = Text value

        member inline this.onClick click = OnClick click

        member inline this.render(attrs: Attribute list) =
            let prop = attrs |> List.fold Property.withAttribute Property.default'

            Html.button [
                Attr.classes (this.baseClasses @ prop.variant @ prop.size)
                Ev.onClick (fun _ -> prop.onClick ())
                Html.text prop.text
            ]

    let button = ButtonEngine()