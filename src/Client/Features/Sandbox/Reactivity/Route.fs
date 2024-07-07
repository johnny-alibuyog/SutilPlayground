namespace AlphaConnect.Client.Features.Sandbox.Reactivity

type Route =
    | ReactiveAssignmentPage
    | ReactiveDeclarationPage
    | ReactiveStatementPage

module Route =
    open AlphaConnect.Client.Context.Router
    open AlphaConnect.Client.Context.Navigator

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [ "reactive-assignments" ] -> ReactiveAssignmentPage
        | [ "reactive-declarations" ] -> ReactiveDeclarationPage
        | [ "reactive-statements" ] -> ReactiveStatementPage
        | _ -> ReactiveAssignmentPage

    let asUrl route =
        match route with
        | ReactiveAssignmentPage -> "/sandbox/reactivity/reactive-assignments"
        | ReactiveDeclarationPage -> "/sandbox/reactivity/reactive-declarations"
        | ReactiveStatementPage -> "/sandbox/reactivity/reactive-statements"

    let navigate (env: #INavigator) route = route |> asUrl |> env.navigate

    let (|IsReactivity|_|) (segments: UrlSegments) =
        match segments with
        | "sandbox" :: "reactivity" :: s -> Some(ofUrl s)
        | _ -> None