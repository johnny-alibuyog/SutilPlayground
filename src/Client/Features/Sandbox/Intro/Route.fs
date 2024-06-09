namespace AlphaConnect.Client.Features.Sandbox.Intro

type Route =
    | HelloWorldPage
    | SamplePage
    | DynamicAttributePage
    | StylingPage
    | NestedComponentPage
    | HtmlTagsPage

module Route =
    let ofUrl segments =
        match segments with
        | [ "sandbox"; "intro"; "hello-world" ] -> HelloWorldPage
        | [ "sandbox"; "intro"; "sample" ] -> SamplePage
        | [ "sandbox"; "intro"; "dynamic-attribute" ] -> DynamicAttributePage
        | [ "sandbox"; "intro"; "styling" ] -> StylingPage
        | [ "sandbox"; "intro"; "nested-component" ] -> NestedComponentPage
        | [ "sandbox"; "intro"; "html-tags" ] -> HtmlTagsPage
        | _ -> SamplePage

    let asUrl route =
        match route with
        | HelloWorldPage -> "/sandbox/intro/hello-world"
        | SamplePage -> "/sandbox/intro/sample"
        | DynamicAttributePage -> "/sandbox/intro/dynamic-attribute"
        | StylingPage -> "/sandbox/intro/styling"
        | NestedComponentPage -> "/sandbox/intro/nested-component"
        | HtmlTagsPage -> "/sandbox/intro/html-tags"

    let navigate navigate route = route |> asUrl |> navigate

    let (|IsIntro|_|) segments =
        match segments with
        | "sandbox" :: "intro" :: _ -> Some(ofUrl segments)
        | _ -> None
