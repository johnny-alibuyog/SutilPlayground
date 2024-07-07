namespace AlphaConnect.Client.Features.Sandbox.Intro

type Route =
    | HelloWorldPage
    | SamplePage
    | DynamicAttributePage
    | StylingPage
    | NestedComponentPage
    | HtmlTagsPage

module Route =
    open AlphaConnect.Client.Context.Router
    open AlphaConnect.Client.Context.Navigator

    let ofUrl (segments: UrlSegments) =
        Browser.Dom.console.log segments
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
        | "sandbox" :: "intro" :: s -> Some(ofUrl s)
        | _ -> None