namespace XnaIsomRtc.Actors

open System
open Microsoft.Xna.Framework

type Person(spriteBatch : Graphics.SpriteBatch, tileSet : Graphics.Texture2D, spriteSize : int * int, frames : int) = 
    inherit Actor (spriteBatch, tileSet, spriteSize, frames)
    
    [<DefaultValue>] val mutable PreviousGameTime : GameTime
    
    override this.Update (gameTime : GameTime) =
        if this.PreviousGameTime = null then 
            this.PreviousGameTime <- gameTime
        elif gameTime.TotalGameTime.Ticks % (24 |> int64) = (0 |> int64) then
            this.CurrentFrame <- (this.CurrentFrame + 1) % frames
        elif gameTime.TotalGameTime.Ticks % (5 |> int64) = (0 |> int64) then
            this.PointOffset <- new Point (this.PointOffset.X - 1,  + this.PointOffset.Y - 1)
        
    override this.Click () = ()