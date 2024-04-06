module Index

open Fable.Remoting.Client
open Sutil
open Shared

type Model = { Todos: Todo list; Input: string }

type Msg =
    | GotTodos of Todo list
    | SetInput of string
    | AddTodo
    | AddedTodo of Todo

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init () =
    let model = { Todos = []; Input = "" }
    let cmd = Cmd.OfAsync.perform todosApi.getTodos () GotTodos
    model, cmd

let update msg model =
    match msg with
    | GotTodos todos -> { model with Todos = todos }, Cmd.none

    | SetInput value -> { model with Input = value }, Cmd.none

    | AddTodo ->
        let todo = Todo.create model.Input
        let cmd = Cmd.OfAsync.perform todosApi.addTodo todo AddedTodo

        { model with Input = "" }, cmd

    | AddedTodo todo ->
        {
            model with
                Todos = model.Todos @ [ todo ]
        },
        Cmd.none

let private todoAction model dispatch =
    Html.div [
        Attr.className "flex flex-col sm:flex-row mt-4 gap-4"
        Html.input [
            Attr.className
                "shadow appearance-none border rounded w-full py-2 px-3 outline-none focus:ring-2 ring-teal-300 text-grey-darker"
            Attr.value model.Input
            Attr.placeholder "What needs to be done?"
            Attr.autoFocus true
            Ev.onChange (SetInput >> dispatch)
            Ev.onKeyPress (fun ev ->
                if ev.key = "Enter" then
                    dispatch AddTodo)
        ]
        Html.button [
            Attr.className
                "flex-no-shrink p-2 px-12 rounded bg-teal-600 outline-none focus:ring-2 ring-teal-300 font-bold text-white hover:bg-teal disabled:opacity-30 disabled:cursor-not-allowed"
            Attr.disabled (Todo.isValid model.Input |> not)
            Ev.onClick (fun _ -> dispatch AddTodo)
            Attr.text "Add"
        ]
    ]

let private todoList model dispatch =
    Html.div [
        Attr.className "bg-white/80 rounded-md shadow-md p-4 w-5/6 lg:w-3/4 lg:max-w-2xl"
        Html.ol [
            Attr.className "list-decimal ml-6"
            for todo in model.Todos do
                Html.li [ Attr.className "my-1"; Attr.text todo.Description ]
        ]

        todoAction model dispatch
    ]

let view () =
    let model, dispatch = () |> Store.makeElmish init update ignore

    Html.section [
        Attr.className "h-screen w-screen"
        Attr.style [
            Css.backgroundSize "cover"
            Css.backgroundImageUrl "https://unsplash.it/1200/900?random"
            Css.backgroundPosition "no-repeat center center fixed"
        ]

        Html.a [
            Attr.href "https://safe-stack.github.io/"
            Attr.className "absolute block ml-12 h-12 w-12 bg-teal-300 hover:cursor-pointer hover:bg-teal-400"
            Html.img [ Attr.src "/favicon.png"; Attr.alt "Logo" ]
        ]

        Html.div [
            Attr.className "flex flex-col items-center justify-center h-full"
            Html.h1 [
                Attr.className "text-center text-5xl font-bold text-white mb-3 rounded-md p-4"
                Attr.text "Alpha Connect"
            ]
            todoList (model |> Store.current) dispatch
        ]
    ]