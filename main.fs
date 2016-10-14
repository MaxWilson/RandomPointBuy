module main

open System
open Fable.Core
open Fable.Import
module R = Fable.Helpers.React

// Check components.fs to see how to build React components from F#

// Polyfill for ES6 features in old browsers
Node.require.Invoke("core-js") |> ignore



ReactDom.render(R.text[][unbox "hello world"], Browser.document.getElementById "content")
|> ignore