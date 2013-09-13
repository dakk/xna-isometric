namespace XnaIsomRtc.Actors

open System
open Microsoft.Xna.Framework

[<AbstractClass>]
type Actor(spriteBatch : Graphics.SpriteBatch, tileSet : Graphics.Texture2D, spriteSize : int * int, frames : int) = 
    member val Direction : int = 0 with get, set
    member val WorldCoordinate : Point = new Point (0, 0) with get, set
    member val CurrentFrame : int = 0 with get, set
    member val CurrentSet : int = 0 with get, set
    member val PointOffset : Point = new Point (0, 0) with get, set //cazzatona!
    
    // Draw the actor
    member this.Draw (screenPosition : Point, tileSize : int) =
        let ny = (snd spriteSize) * this.CurrentFrame
        let nx = (fst spriteSize) * this.Direction
        
        spriteBatch.Begin ()
        spriteBatch.Draw (tileSet, new Rectangle (screenPosition.X + this.PointOffset.X, screenPosition.Y + this.PointOffset.Y, 
                                                  fst spriteSize, snd spriteSize),
                            new Nullable<Rectangle> (new Rectangle (nx, ny, fst spriteSize, snd spriteSize)), 
                            Color.White, 0.0f, Vector2.Zero, Graphics.SpriteEffects.None, 1.0f)
        spriteBatch.End ()
        
        
    // Handle update event
    abstract member Update : GameTime -> unit
    
    // Handle click event on an actor
    abstract member Click : unit -> unit