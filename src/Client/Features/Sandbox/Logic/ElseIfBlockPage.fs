namespace SutilPlayground.Client.Features.Sandbox.Logic

module ElseIfBlockPage =
    open Sutil

    let x = 7

    let view () =
        Html.div [
            if x > 10 then Html.text $"{x} is greater than 10"
            elif x < 10 then Html.text $"{x} is less than 10"
            else Html.text $"{x} is equal to 10"
        ]