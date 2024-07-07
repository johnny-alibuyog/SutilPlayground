namespace AlphaConnect.Client.Features.Sandbox.Logic

type Route =
    | IfBlockPage
    | ElseBlockPage
    | ElseIfBlockPage

module Route =
    open AlphaConnect.Client.Context.Router
    open AlphaConnect.Client.Context.Navigator

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [ "if-block" ] -> IfBlockPage
        | [ "else-block" ] -> ElseBlockPage
        | [ "else-if-block" ] -> ElseIfBlockPage
        | _ -> IfBlockPage

    let asUrl route =
        match route with
        | IfBlockPage -> "/sandbox/logic/if-block"
        | ElseBlockPage -> "/sandbox/logic/else-block"
        | ElseIfBlockPage -> "/sandbox/logic/else-if-block"

    let navigate (env: #INavigator) route = route |> asUrl |> env.navigate

    let (|IsLogic|_|) (segments: UrlSegments) =
        match segments with
        | "sandbox" :: "logic" :: s -> Some(ofUrl s)
        | _ -> None