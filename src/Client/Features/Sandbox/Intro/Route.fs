namespace AlphaConnect.Client.Features.Sandbox.Intro

type Route =
    | DynamicAttributePage
    | StylingPage
    | SamplePage

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "intro"; "dynamic-attribute" ] -> DynamicAttributePage
        | [ "sandbox"; "intro"; "styling" ] -> StylingPage
        | [ "sandbox"; "intro"; "sample" ] -> SamplePage
        | _ -> SamplePage

    let asUrl route =
        match route with
        | DynamicAttributePage -> "/sandbox/intro/dynamic-attribute"
        | StylingPage -> "/sandbox/intro/styling"
        | SamplePage -> "/sandbox/intro/sample"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsIntro|_|) segments =
        match segments with
        | "sandbox" :: "intro" :: _ -> Some(ofUrl segments)
        | _ -> None
