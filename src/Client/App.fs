namespace AlphaConnect.Client

module App =

    open Sutil
    open Fable.Core.JsInterop

    importSideEffects "./index.css"

    Index.view () |> Program.mount