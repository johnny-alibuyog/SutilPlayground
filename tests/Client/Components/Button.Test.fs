namespace SutilPlayground.Client.Components.Button

open SutilPlayground.Client.Components.Button
open Expect
open Expect.Dom
open WebTestRunner

[<AutoOpen>]
module Mock =

    type ButtonBuilder() =
        member _.Yield _ = []

        [<CustomOperation("variant")>]
        member _.Variant(state, select: VariantEngine -> Attribute) =
            state @ [ select(Button.variant) ]

        [<CustomOperation("size")>]
        member _.Size(state, select: SizeEngine -> Attribute) =
            state @ [ select(Button.size) ]

        [<CustomOperation("text")>]
        member _.Text(state, value: string) =
            state @ [ Button.text value ]

        [<CustomOperation("onClick")>]
        member _.OnClick(state, value: unit -> unit) =
            state @ [ Button.onClick value ]

        [<CustomOperation("disabled")>]
        member _.Disabled(state, value: bool) =
            state @ [ Button.disabled value ]

        member _.Run(state) = Button.create state

    let builder = ButtonBuilder()

    let x = builder {
        variant _.default'
        size _.default'
        text "Click me"
        onClick (fun _ -> printfn "Clicked")
    }

    printfn $"Button: { x }"

    // type User = {
    //     Name: string
    //     Address: string
    //     Birthday: System.DateTime option
    // }

    // type UserBuilder() =
    //     [<CustomOperation("name")>]
    //     member _.Name(user: User, value: string) =
    //         printf $"name: {value}\n"
    //         { user with Name = value }

    //     [<CustomOperation("address")>]
    //     member _.Address(user: User, value: string) =
    //         printf $"address: {value}\n"
    //         { user with Address = value }

    //     [<CustomOperation("birthday")>]
    //     member _.Birthday(user: User, value: System.DateTime) =
    //         printf $"birthday: {value}\n"
    //         { user with Birthday = Some value }

    //     member _.Yield(something) =
    //         printf $"yield: {something}\n"
    //         { Name = ""; Address = ""; Birthday = None }

    //     member _.Run(state) =
    //         printf $"run: {state}\n"
    //         state

    // let user = UserBuilder()

    // let johnny =
    //     user {
    //         name "Johnny"
    //         address "Some Address"
    //     }

    // printfn $"User: { johnny }"

module Test =
    let render component' =
        let container = Container.New()
        Sutil.Program.mount (container.El, component') |> ignore
        container

    describe "button.text" (fun () ->

        it "should not contain label by default" (fun () -> promise {
            use container = Button.create [] |> render
            let button = container.El.getButton ""

            button |> Expect.innerText ""
        })

        it "should have label when set" (fun () -> promise {
            use container = Button.create [ Button.text "Click me" ] |> render
            let button = container.El.getButton "Click me"

            button |> Expect.innerText "Click me"
        })
    )

    describe "button.disabled" (fun () ->

        it "should not be disabled by default" (fun () -> promise {
            use container = Button.create [ Button.text "Click me" ] |> render
            let button = container.El.getButton "Click me"

            button.disabled |> Expect.equal false
        })

        it "should be disabled when set to boolean true" (fun () -> promise {
            use container =
                Button.create [ Button.text "Click me"; Button.disabled true ] |> render

            let button = container.El.getButton "Click me"

            button.disabled |> Expect.equal true
        })
    )

    describe "button.onClick" (fun () ->

        it "should be called when button is clicked" (fun () -> promise {
            let mutable clicked = false

            use container =
                Button.create [
                    Button.onClick (fun _ -> clicked <- true)
                    Button.text "Click me"
                ]
                |> render

            let button = container.El.getButton "Click me"

            button.click ()

            clicked |> Expect.equal true
        })

        it "should not be called when button disabled" (fun () -> promise {
            let mutable clicked = false

            use container =
                Button.create [
                    Button.onClick (fun _ -> clicked <- true)
                    Button.text "Click me"
                    Button.disabled true
                ]
                |> render

            let button = container.El.getButton "Click me"

            button.click ()

            clicked |> Expect.equal false
        })
    )
// let x = 1

// let buttonFixture () =
//     testList "button.text" [
//         testCase "should be reflected" (fun _ ->
//             // let button = button.render [ button.text "Click me" ]
//             // let element = createTestApp button

//             Expect.equal "Click me" "Click me" "Should be equal to 'Click me'"
//         )
//     ]

(*
{
    "private": true,
    "scripts": {
        "install": "dotnet tool restore",
        "bundle": "cd src/Fable.Expect && esbuild queries.js --bundle --outfile=queries.bundle.js --format=esm",
        "publish": "dotnet fsi build.fsx publish",
        "test": "dotnet fable . --define HEADLESS --run web-test-runner *\\*Test.fs.js --node-resolve",
        "test:watch1": "dotnet fable watch . --define HEADLESS --run web-test-runner *\\*Test.fs.js --node-resolve",
        "test:watch":  "dotnet fable watch test -o build/test --define HEADLESS --run web-test-runner *\\*Test.fs.js --node-resolve --watch",
        "test-in-browser:build": "dotnet fable test --run webpack --config webpack.config.tests.js",
        "test-in-browser": "dotnet fable watch test --run webpack serve --config webpack.config.tests.js "
    },
    "devDependencies": {
        "@web/test-runner": "^0.13.18",
        "@web/test-runner-commands": "^0.5.13",
        "aria-query": "^5.0.0",
        "dom-accessibility-api": "^0.5.7",
        "esbuild": "^0.13.4"
    }
}
*)


(*

    "client", dotnet [ "fable"; "."; "--define"; "HEADLESS"; "--run"; "web-test-runner"; "*\\*Test.fs.js"; "--node-resolve" ] clientTestsPath


dotnet fable . --define HEADLESS --run web-test-runner *\*Test.fs.js --node-resolve --watch

dotnet fable --define HEADLESS --run web-test-runner "*\*Test.fs.js" --node-resolve
dotnet fable . --define HEADLESS --run web-test-runner *\*Test.fs.js --node-resolve


dotnet fable watch --define HEADLESS --run web-test-runner "*\*Test.fs.js" --node-resolve --watch

*)