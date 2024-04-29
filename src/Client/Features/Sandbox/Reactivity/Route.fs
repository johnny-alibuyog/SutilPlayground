namespace AlphaConnect.Client.Features.Sandbox.Reactivity

type Route =
    | ReactiveAssignmentPage

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "reactivity"; "reactive-assignments" ] -> ReactiveAssignmentPage
        | _ -> ReactiveAssignmentPage

    let asUrl route =
        match route with
        | ReactiveAssignmentPage -> "/sandbox/reactivity/reactive-assignments"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsReactivity|_|) segments =
        match segments with
        | "sandbox" :: "reactivity" :: _ -> Some(ofUrl segments)
        | _ -> None
