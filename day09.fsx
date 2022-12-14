open System;
open System.Numerics

let move = function | 'D' -> (0, -1) | 'L' -> (-1, 0) | 'R' -> (1, 0) | 'U' -> (0, 1)
let add (fromX, fromY) (addX, addY) = (fromX + addX, fromY + addY)
let clampDeltaByDistance distance delta  = if (distance < 2.0f) then 0 else Math.Clamp(delta, -1, 1)
let computeMovement (x1, y1) (x2, y2) =
    let distance = Vector2.Distance(Vector2((float32 x1), (float32 y1)), Vector2((float32 x2), (float32 y2)))
    clampDeltaByDistance distance (x2 - x1), clampDeltaByDistance distance (y2 - y1)

let head = Seq.scan (add) (0, 0)
let tail = Seq.scan (fun from target -> (from, target) ||> computeMovement |> add from) (0, 0)

let ops = System.IO.File.ReadLines "inputs/day9_r.txt" |> Seq.map (fun s -> s[0], int s[2..]) |> Seq.collect (fun (dir, amount) -> [|1..amount|] |> Seq.map (fun _ -> move dir))

ops |> (head >> tail >> Seq.distinct >> Seq.length) |> printfn "Answer 1: %A"
ops |> (head >> tail >> tail >> tail >> tail >> tail >> tail >> tail >> tail >> tail >> Seq.distinct >> Seq.length) |> printfn "Answer 2: %A"
