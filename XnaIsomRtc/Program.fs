module XnaIsomRtc.Main

open System


[<EntryPoint>]
let main args = 
    let ob = new IsoGame()
    do
        ob.Run ()
    0
    
