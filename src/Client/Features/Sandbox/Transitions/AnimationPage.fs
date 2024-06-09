namespace AlphaConnect.Client.Features.Sandbox.Transitions

module AnimationPage =

    open Browser.Types
    open Sutil
    open Sutil.Core
    open Sutil.CoreElements
    open Sutil.DomHelpers
    open Sutil.Styling
    open Sutil.Transition
    open type Feliz.length

    [<AutoOpen>]
    module Types =
        type Status =
            | Done
            | Pending

        type Todo = {
            id: int
            status: Status
            description: string
        }

        module Todo =
            let create id description = {
                id = id
                status = Pending
                description = description
            }

            let toggle todo = {
                todo with
                    status =
                        match todo.status with
                        | Done -> Pending
                        | Pending -> Done
            }

            let toggelWhenMatchedWith id todo =
                if todo.id = id then toggle todo else todo

            let isSame id todo = todo.id = id

            let isNotSame id todo = todo.id <> id

            let isDone todo = todo.status = Done

            let isPending todo = todo.status = Pending

            let createId todos =
                match todos with
                | [] -> 1
                | _ -> todos |> List.map _.id |> List.max |> (+) 1

            let compplete todo = { todo with status = Done }

        type Model = { todos: Todo list; sorted: bool }

        module Model =
            let toString model =
                $"The Model: {model.todos.Length} - {model.sorted}"

            let init () = {
                todos = [
                    Todo.create 1 "Learn F#"
                    Todo.create 2 "Learn Elm"
                    Todo.create 3 "Learn ReasonML"
                    Todo.create 4 "Learn Rust"
                    Todo.create 5 "Learn Haskell"
                    Todo.create 6 "Learn Idris"
                    Todo.create 7 "Learn PureScript"
                    Todo.create 8 "Learn Scala"
                    Todo.create 9 "Learn Kotlin"
                    Todo.create 10 "Learn Swift"
                    Todo.create 11 "Learn Clojure"
                    Todo.create 12 "Learn Elixir"
                ]
                sorted = false
            }

            let addTodo description model =
                let id = Todo.createId model.todos
                let todo = Todo.create id description

                {
                    model with
                        todos = model.todos @ [ todo ]
                }

            let toggleTodo id model = {
                model with
                    todos = model.todos |> List.map (Todo.toggelWhenMatchedWith id)
            }

            let deleteTodo id model = {
                model with
                    todos = model.todos |> List.filter (Todo.isNotSame id)
            }

            let sortTodos sort model = { model with sorted = sort }

            let completeAll model = {
                model with
                    todos = model.todos |> List.map Todo.compplete
            }

    type Message =
        | AddTodo of description: string
        | ToggleTodo of id: int
        | DeleteTodo of id: int
        | SortTodos of sort: bool
        | CompleteAll

    let update (message: Message) (model: Model) : Model =
        match message with
        | AddTodo description -> model |> Model.addTodo description
        | ToggleTodo id -> model |> Model.toggleTodo id
        | DeleteTodo id -> model |> Model.deleteTodo id
        | SortTodos sort -> model |> Model.sortTodos sort
        | CompleteAll -> model |> Model.completeAll

    let style = [
        rule ".new-todo" [ Css.fontSize (em 1.4); Css.width (percent 100.0) ]

        rule ".board" [ Css.maxWidth (em 36.0); Css.margin (px 0, auto) ]

        rule ".todo, .done" [
            Css.width (percent 50)
            Css.padding (zero, em 1, zero, zero)
            Css.boxSizingBorderBox
        ]

        rule ".title" [ Css.marginTop (px 24) ]

        rule "h2" [ Css.fontSize (em 2.0); Css.fontWeight 200; Css.userSelectNone ]

        rule "label" [
            Css.top 0
            Css.left 0
            Css.displayBlock
            Css.width (percent 100.0)
            Css.fontSize (em 1.0)
            Css.lineHeight 1
            Css.padding (em 0.5)
            Css.margin (zero, auto, em 0.5, auto)
            Css.borderRadius (px 2.0)
            Css.backgroundColor "#eee"
            Css.userSelectNone
        ]

        rule "input" [ Css.margin (0) ]

        rule ".done label" [ Css.backgroundColor "rgb(180,240,100)" ]

        rule "label>button" [
            Css.floatRight
            Css.height (em 1.0)
            Css.boxSizingBorderBox
            Css.padding (zero, em 0.5)
            Css.lineHeight 1
            Css.backgroundColor "transparent"
            Css.borderStyleNone
            Css.color "rgb(170,30,30)"
            Css.opacity 0.0
            Css.transition "opacity 0.2s"
        ]

        rule "label:hover button" [ Css.opacity 1.0 ]

        rule ".row" [ Css.displayFlex ]

        rule ".kudos" [ Css.fontSize (percent 80); Css.color "#888" ]

        rule "div.complete-all-container" [ Css.displayFlex; Css.justifyContentSpaceBetween; Css.marginTop (px 4.0) ]

        rule ".complete-all-container a" [
            Css.cursorPointer
            Css.textDecorationNone

            Css.fontSize (percent 80)
            Css.color "#888"
        ]

        rule ".complete-all-container a:hover" [ Css.textDecorationUnderline ]
    ]

    let fader element =
        element |> transition [ InOut(fade |> withProps [ Duration 300.0 ]) ]

    let slider element =
        element |> transition [ InOut(slide |> withProps [ Duration 300.0 ]) ]

    let todoList title criteria transitionIn transitionOut model dispatch =

        let filterTodos todos = todos |> List.filter criteria

        let sortTodos sorted todos =
            match sorted with
            | true -> todos |> List.sortBy _.description
            | false -> todos

        let todos =
            model |> Store.map (fun m -> m.todos |> filterTodos |> sortTodos m.sorted)

        Html.divc title [
            Html.h2 [ Html.text title ]
            Bind.each (
                todos
                , fun todo ->
                    Html.label [
                        Html.input [
                            Attr.type' "checkbox"
                            Bind.attrInit ("checked", todo |> Todo.isDone, (fun _ -> ToggleTodo todo.id |> dispatch))
                        ]
                        Html.text $" ${todo.description}"
                        Html.button [ Html.text "x"; Ev.onClick (fun _ -> DeleteTodo todo.id |> dispatch) ]
                    ]
                , _.id
                , [ In transitionIn; Out transitionOut ]
            )
        ]

    let makeStore arg =
        Store.makeElmishSimple Model.init update ignore arg

    let fallback (props: TransitionProp list) (node: HTMLElement) =
        fun _ ->
            let transform = computedStyleTransform node

            {
                (applyProps props Transition.Default) with
                    Duration = 600.0
                    Ease = Easing.quintOut
                    CssGen = Some(fun t _ -> $"transform: {transform} scale({t}); opacity: {t}")
            }

    let view () : SutilElement =
        let (send, receive) = crossfade [ Fallback fallback ]

        let model, dispatch = makeStore ()

        let completed = model |> Store.map (fun m -> m.todos |> List.filter Todo.isDone)

        let lotsDone = completed |> Store.map (fun t -> t.Length > 5)

        Html.divc "board" [
            disposeOnUnmount [ model ]

            Html.inputc "new-todo" [
                Attr.placeholder "what needs to be done?"
                Ev.onKeyDown (fun e ->
                    // This isn't the right test for mobile users
                    if e.key = "Enter" then
                        (e.currentTarget :?> HTMLInputElement).value |> AddTodo |> dispatch

                    printfn ($"{e.key}"))
            ]

            Html.divc "complete-all-container" [
                Bind.el (
                    model,
                    fun m ->
                        Html.a [
                            Attr.href "#"
                            Html.text "toggle sort"
                            Ev.onClick (fun e ->
                                e.preventDefault ()
                                SortTodos(not m.sorted) |> dispatch)
                        ]
                )
                Html.a [
                    Attr.href "#"
                    Html.text "complete all"
                    Ev.onClick (fun e ->
                        e.preventDefault ()
                        dispatch CompleteAll)
                ]
                Html.spanc "kudos" [
                    Bind.el (completed, (fun x -> Html.text $"{x.Length} tasks completed! Good job!"))
                ]
                |> fader lotsDone
            ]

            Html.div [
                class' "row"
                todoList "todo" Todo.isPending receive send model dispatch
                todoList "done" Todo.isDone receive send model dispatch
            ]
        ]
        |> withStyle style