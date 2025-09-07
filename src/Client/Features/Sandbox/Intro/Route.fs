namespace SutilPlayground.Client.Features.Sandbox.Intro

type Route =
    | HelloWorldPage
    | SamplePage
    | DynamicAttributePage
    | StylingPage
    | NestedComponentPage
    | HtmlTagsPage

module Route =
    open SutilPlayground.Client.Env.Navigation

    let ofUrl (segments: UrlSegments) =
        match segments with
        | [ "hello-world" ] -> HelloWorldPage
        | [ "sample" ] -> SamplePage
        | [ "dynamic-attribute" ] -> DynamicAttributePage
        | [ "styling" ] -> StylingPage
        | [ "nested-component" ] -> NestedComponentPage
        | [ "html-tags" ] -> HtmlTagsPage
        | _ -> SamplePage

    let asUrl route =
        match route with
        | HelloWorldPage -> "/sandbox/intro/hello-world"
        | SamplePage -> "/sandbox/intro/sample"
        | DynamicAttributePage -> "/sandbox/intro/dynamic-attribute"
        | StylingPage -> "/sandbox/intro/styling"
        | NestedComponentPage -> "/sandbox/intro/nested-component"
        | HtmlTagsPage -> "/sandbox/intro/html-tags"

    let navigate (env: #INavigator) route = route |> asUrl |> env.navigate

    let (|IsIntro|_|) (segments: UrlSegments) =
        match segments with
        | "sandbox" :: "intro" :: s ->
            let parent = segments |> List.except s
            Some(ofUrl s)
        | _ -> None