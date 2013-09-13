namespace XnaIsomRtc.Actors

open System
open Microsoft.Xna.Framework

type Person(spriteBatch : Graphics.SpriteBatch, tileSet : Graphics.Texture2D, spriteSize : int * int, frames : int) = 
    inherit Actor (spriteBatch, tileSet, spriteSize, frames)
        
    override this.Update (gameTime : GameTime) =
        this.CurrentFrame <- (this.CurrentFrame + 1) % frames
        
        
        
    override this.Click () = ()