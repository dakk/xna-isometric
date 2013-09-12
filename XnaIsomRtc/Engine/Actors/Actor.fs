namespace XnaIsomRtc.Actors

open System
open Microsoft.Xna.Framework

[<AbstractClass>]
type Actor(spriteBatch : Graphics.SpriteBatch) = 
    member val Direction : int = 0 with get, set
    member val WorldCoordinate : Point = new Point (0, 0) with get, set
    
    // this function could be implemented here, using a system to select which tile render
    //abstract member Draw : (Point -> int) -> unit
    
    // handle click event on an actor
    //abstract member OnClick