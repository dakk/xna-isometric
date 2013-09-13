namespace XnaIsomRtc.Actors

open System
open Microsoft.Xna.Framework

[<AbstractClass>]
type Actor(spriteBatch : Graphics.SpriteBatch, tileSet : Graphics.Texture2D, spriteSize : int * int, frames : int, worldPosition : Point) = 
    member val Direction : int = 1 with get, set
    member val WorldPosition : Point =  worldPosition with get, set
    member val CurrentFrame : int = 0 with get, set
    member val CurrentSet : int = 0 with get, set
    
    // Draw the actor
    member this.Draw (gameTime : GameTime, screenPosition : Point, tileSize : int) =
        let ny = (snd spriteSize) * this.CurrentFrame
        let nx = (fst spriteSize) * this.Direction
        
        spriteBatch.Draw (tileSet, new Rectangle (screenPosition.X + this.WorldPosition.X, screenPosition.Y + this.WorldPosition.Y, 
                                                  fst spriteSize, snd spriteSize),
                            new Nullable<Rectangle> (new Rectangle (nx, ny, fst spriteSize, snd spriteSize)), 
                            Color.White, 0.0f, Vector2.Zero, Graphics.SpriteEffects.None, 1.0f)
        
        
    // Handle update event
    abstract member Update : GameTime -> unit
    
    // Handle click event on an actor
    abstract member Click : unit -> unit