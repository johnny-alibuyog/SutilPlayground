namespace AlphaConnect.Client.Features.Sandbox.Logic

type Route =
    | IfBlockPage
    | ElseBlockPage
    | ElseIfBlockPage

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "logic"; "if-block" ] -> IfBlockPage
        | [ "sandbox"; "logic"; "else-block" ] -> ElseBlockPage
        | [ "sandbox"; "logic"; "else-if-block" ] -> ElseIfBlockPage
        | _ -> IfBlockPage

    let asUrl route =
        match route with
        | IfBlockPage -> "/sandbox/logic/if-block"
        | ElseBlockPage -> "/sandbox/logic/else-block"
        | ElseIfBlockPage -> "/sandbox/logic/else-if-block"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsLogic|_|) segments =
        match segments with
        | "sandbox" :: "logic" :: _ -> Some(ofUrl segments)
        | _ -> None