namespace AlphaConnect.Client.Features.Sandbox.Reactivity

type Route =
    | ReactiveAssignmentPage
    | ReactiveDeclarationPage
    | ReactiveStatementPage

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "reactivity"; "reactive-assignments" ] -> ReactiveAssignmentPage
        | [ "sandbox"; "reactivity"; "reactive-declarations" ] -> ReactiveDeclarationPage
        | [ "sandbox"; "reactivity"; "reactive-statements" ] -> ReactiveStatementPage
        | _ -> ReactiveAssignmentPage

    let asUrl route =
        match route with
        | ReactiveAssignmentPage -> "/sandbox/reactivity/reactive-assignments"
        | ReactiveDeclarationPage -> "/sandbox/reactivity/reactive-declarations"
        | ReactiveStatementPage -> "/sandbox/reactivity/reactive-statements"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsReactivity|_|) segments =
        match segments with
        | "sandbox" :: "reactivity" :: _ -> Some(ofUrl segments)
        | _ -> None
