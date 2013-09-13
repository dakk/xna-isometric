namespace XnaIsomRtc.Actors

open System
open Microsoft.Xna.Framework

[<AbstractClass>]
type Actor(spriteBatch : Graphics.SpriteBatch, tileSet : Graphics.Texture2D, spriteSize : int * int, frames : int) = 
    member val Direction : int = 0 with get, set
    member val WorldCoordinate : Point = new Point (0, 0) with get, set
    member val CurrentFrame : int = 0 with get, set
    member val CurrentSet : int = 0 with get, set
    
    // Draw the actor
    member this.Draw (screenPosition : Point, tileSize : int) =
        let ny = snd (spriteSize) * this.CurrentFrame
        let nx = (fst spriteSize) * this.Direction
        
        spriteBatch.Draw (tileSet, new Rectangle (nx, ny, nx + fst spriteSize, ny + snd spriteSize), Color.White)
        
        
    // Handle update event
    abstract member Update : GameTime -> unit
    
    // Handle click event on an actor
    abstract member Click : unit -> unit