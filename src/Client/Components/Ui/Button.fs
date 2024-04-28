namespace Components.Ui


module Button =

    open Sutil

    type ButtonVariant =
        | Default
        | Destructive
        | Outline
        | Secondary
        | Ghost
        | Link

    module ButtonVariant =
        let asClass =
            function
            | Default -> "bg-primary text-primary-foreground hover:bg-primary/90"
            | Destructive -> "bg-destructive text-destructive-foreground hover:bg-destructive/90"
            | Outline -> "border border-input bg-background hover:bg-accent hover:text-accent-foreground"
            | Secondary -> "bg-secondary text-secondary-foreground hover:bg-secondary/80"
            | Ghost -> "hover:bg-accent hover:text-accent-foreground"
            | Link -> "text-primary underline-offset-4 hover:underline"

    type ButtonSize =
        | Default
        | Small
        | Large
        | Icon

    module Size =
        let asClass =
            function
            | Default -> "h-10 px-4 py-2"
            | Small -> "h-9 rounded-md px-3"
            | Large -> "h-11 rounded-md px-8"
            | Icon -> "h-10 w-10"

    type Props = {
        variant: ButtonVariant
        size: ButtonSize
        text: string option
        onClick: unit -> unit
    }

    let render props =
        let className =
            "inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50"

        Html.button [
            Attr.className $"{className} {ButtonVariant.asClass props.variant} {Size.asClass props.size}"
            Attr.text (props.text |> Option.defaultValue System.String.Empty)
            Ev.onClick (fun _ -> props.onClick ())
        ]

module Button2 =

    open Sutil

    type BtnAttr =
        | Variant of string
        | Size of string
        | Text of string
        | Event of (unit -> unit)

    module BtnAttr =
        let isVariant = function | Variant _ -> true | _ -> false
        let isSize = function | Size _ -> true | _ -> false
        let isText = function | Text _ -> true | _ -> false
        let isEvent = function | Event _ -> true | _ -> false

        let variantDefault = Variant "bg-primary text-primary-foreground hover:bg-primary/90"
        let variantDestructive = Variant "bg-destructive text-destructive-foreground hover:bg-destructive/90"
        let variantOutline = Variant "border border-input bg-background hover:bg-accent hover:text-accent-foreground"
        let variantSecondary = Variant "bg-secondary text-secondary-foreground hover:bg-secondary/80"
        let variantGhost = Variant "hover:bg-accent hover:text-accent-foreground"
        let variantLink = Variant "text-primary underline-offset-4 hover:underline"

        let sizeDefault = Size "h-10 px-4 py-2"
        let sizeSmall = Size "h-9 rounded-md px-3"
        let sizeLarge = Size "h-11 rounded-md px-8"
        let sizeIcon = Size "h-10 w-10"

    let render (attrs: BtnAttr list) =
        let className =
            "inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50"

        let variant =
            attrs
            |> List.tryFind BtnAttr.isVariant
            |> Option.defaultValue BtnAttr.variantDefault
            |> function | Variant v -> v | _ -> System.String.Empty

        let size =
            attrs
            |> List.tryFind BtnAttr.isSize
            |> Option.defaultValue BtnAttr.sizeDefault
            |> function | Size s -> s | _ -> System.String.Empty

        let text =
            attrs
            |> List.tryFind BtnAttr.isText
            |> Option.defaultValue (Text "Button")
            |> function | Text t -> t | _ -> System.String.Empty

        let onClick =
            attrs
            |> List.tryFind BtnAttr.isEvent
            |> Option.defaultValue (Event (fun _ -> ()))
            |> function | Event e -> e | _ -> (fun _ -> ())

        Html.button [
            Attr.className $"{className} {variant} {size}"
            Attr.text text
            Ev.onClick (fun _ -> onClick ())
        ]

module Button3 =

    open Sutil

    type ButtonAttr =
        | Variant of string
        | Size of string
        | Text of string
        | Event of (unit -> unit)

    type private ButtonProps = {
        variant: string
        size: string
        text: string
        onClick: unit -> unit
    }

    module button =
        let variantDefault = Variant "bg-primary text-primary-foreground hover:bg-primary/90"
        let variantDestructive = Variant "bg-destructive text-destructive-foreground hover:bg-destructive/90"
        let variantOutline = Variant "border border-input bg-background hover:bg-accent hover:text-accent-foreground"
        let variantSecondary = Variant "bg-secondary text-secondary-foreground hover:bg-secondary/80"
        let variantGhost = Variant "hover:bg-accent hover:text-accent-foreground"
        let variantLink = Variant "text-primary underline-offset-4 hover:underline"

        let sizeDefault = Size "h-10 px-4 py-2"
        let sizeSmall = Size "h-9 rounded-md px-3"
        let sizeLarge = Size "h-11 rounded-md px-8"
        let sizeIcon = Size "h-10 w-10"

        let text t = Text t

        let event e = Event e

    module private ButtonProps =
        let withAttr (props: ButtonProps) (attr: ButtonAttr) =
            match attr with
            | Variant v -> { props with variant = v }
            | Size s -> { props with size = s }
            | Text t -> { props with text = t }
            | Event e -> { props with onClick = e }

        let defaultValue =
            [
                button.variantOutline
                button.sizeDefault
            ]
            |> List.fold withAttr {
                variant = ""
                size = ""
                text = ""
                onClick = (fun _ -> ())
            }

        let ofAttrs (attrs: ButtonAttr list) =
            attrs
            |> List.fold withAttr defaultValue

    let render (attrs: ButtonAttr list) =
        let className =
            "inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50"

        let props' = ButtonProps.ofAttrs attrs

        Html.button [
            Attr.className $"{className} {props'.variant} {props'.size}"
            Attr.text props'.text
            Ev.onClick (fun _ -> props'.onClick ())
        ]