module main

open System
open Fable.Core
open Fable.Import
module R = Fable.Helpers.React
open R.Props
// Check components.fs to see how to build React components from F#

// Polyfill for ES6 features in old browsers
Node.require.Invoke("core-js") |> ignore

let d (n: int) = ((JS.Math.random() * (float n)) |> int) + 1
let stat() = d 6 + d 6 + d 6
let arrays = [stat();stat();stat();stat();stat();stat();]

type StatsState = {str: int; dex: int; con: int; int: int; wis: int; cha: int}


let newArray() =
    let roll() = d 6 + d 6 + d 6
    { str = roll(); dex = roll(); con = roll(); int = roll(); wis = roll(); cha = roll() }

type StatsComponent() as this =
    inherit React.Component<unit, StatsState>()
    do this.state <- newArray()
    member this.render() =
        R.div [] [
            R.h1[][ unbox "This is an extremely basic random stat roller for 5E. All arrays are point-buy legal" ]
            R.br [][]
            R.div[] [
                R.text[][unbox ("Strength: " + this.state.str.ToString())]
                R.br [][]
                R.text[][unbox ("Dexterity: " + this.state.dex.ToString())]
                R.br [][]
                R.text[][unbox ("Constitution: " + this.state.con.ToString())]
                R.br [][]
                R.text[][unbox ("Intelligence: " + this.state.int.ToString())]
                R.br [][]
                R.text[][unbox ("Wisdom: " + this.state.wis.ToString())]
                R.br [][]
                R.text[][unbox ("Charisma: " + this.state.cha.ToString())]
            ]
            R.br[][]
            R.button[OnClick (fun _ -> this.setState (newArray()))][unbox "Reroll"]
        ]

ReactDom.render(R.com<StatsComponent,_,_>()[], Browser.document.getElementById "content")
|> ignore