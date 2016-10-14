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

// arrayspace courtesy of https://amp.reddit.com/r/DnD/comments/2epkdi/5e_here_is_a_complete_list_of_valid_ability_score/
let arraySpace = [
    15, 15, 15, 8, 8, 8

    15, 15, 14, 10, 8, 8

    15, 15, 14, 9, 9, 8

    15, 15, 13, 12, 8, 8

    15, 15, 13, 11, 9, 8

    15, 15, 13, 10, 10, 8

    15, 15, 13, 10, 9, 9

    15, 15, 12, 12, 9, 8

    15, 15, 12, 11, 10, 8

    15, 15, 12, 11, 9, 9

    15, 15, 12, 10, 10, 9

    15, 15, 11, 11, 11, 8

    15, 15, 11, 11, 10, 9

    15, 15, 11, 10, 10, 10

    15, 14, 14, 12, 8, 8

    15, 14, 14, 11, 9, 8

    15, 14, 14, 10, 10, 8

    15, 14, 14, 10, 9, 9

    15, 14, 13, 13, 9, 8

    15, 14, 13, 12, 10, 8

    15, 14, 13, 12, 9, 9

    15, 14, 13, 11, 11, 8

    15, 14, 13, 11, 10, 9

    15, 14, 13, 10, 10, 10

    15, 14, 12, 12, 11, 8

    15, 14, 12, 12, 10, 9

    15, 14, 12, 11, 11, 9

    15, 14, 12, 11, 10, 10

    15, 14, 11, 11, 11, 10

    15, 13, 13, 13, 11, 8

    15, 13, 13, 13, 10, 9

    15, 13, 13, 12, 12, 8

    15, 13, 13, 12, 11, 9

    15, 13, 13, 12, 10, 10

    15, 13, 13, 11, 11, 10

    15, 13, 12, 12, 12, 9

    15, 13, 12, 12, 11, 10

    15, 13, 12, 11, 11, 11

    15, 12, 12, 12, 12, 10

    15, 12, 12, 12, 11, 11

    14, 14, 14, 13, 9, 8

    14, 14, 14, 12, 10, 8

    14, 14, 14, 12, 9, 9

    14, 14, 14, 11, 11, 8

    14, 14, 14, 11, 10, 9

    14, 14, 14, 10, 10, 10

    14, 14, 13, 13, 11, 8

    14, 14, 13, 13, 10, 9

    14, 14, 13, 12, 12, 8

    14, 14, 13, 12, 11, 9

    14, 14, 13, 12, 10, 10

    14, 14, 13, 11, 11, 10

    14, 14, 12, 12, 12, 9

    14, 14, 12, 12, 11, 10

    14, 14, 12, 11, 11, 11

    14, 13, 13, 13, 13, 8

    14, 13, 13, 13, 12, 9

    14, 13, 13, 13, 11, 10

    14, 13, 13, 12, 12, 10

    14, 13, 13, 12, 11, 11

    14, 13, 12, 12, 12, 11

    14, 12, 12, 12, 12, 12

    13, 13, 13, 13, 13, 10

    13, 13, 13, 13, 12, 11

    13, 13, 13, 12, 12, 12
]
let newArray() =
    let roll() = d 6 + d 6 + d 6
    match arraySpace.[(d (arraySpace.Length) - 1)] with
        | a,b,c,e,f,g ->
            let swap (a: _[]) x y =
                let tmp = a.[x]
                a.[x] <- a.[y]
                a.[y] <- tmp
            // shuffle an array (in-place)
            let shuffle (a: int []) =
                Array.iteri (fun i _ -> swap a i (d (a.Length) - 1)) a
                a
            let stats = shuffle [|a;b;c;e;f;g|]
            { str = stats.[0]; dex = stats.[1]; con = stats.[2]; int = stats.[3]; wis = stats.[4]; cha = stats.[5] }

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