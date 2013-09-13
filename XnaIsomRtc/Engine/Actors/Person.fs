namespace XnaIsomRtc.Actors

open System
open Microsoft.Xna.Framework

type Person(spriteBatch : Graphics.SpriteBatch, tileSet : Graphics.Texture2D, spriteSize : int * int, frames : int, ?worldPosition : Point) = 
    inherit Actor (spriteBatch, tileSet, spriteSize, frames, (defaultArg worldPosition (Point (0, 0))))
    
    let rand = new Random ()
    
    [<DefaultValue>] val mutable PreviousGameTime : GameTime
    
    override this.Update (gameTime : GameTime) =
        if this.PreviousGameTime = null then 
            this.PreviousGameTime <- gameTime
        elif gameTime.TotalGameTime.Ticks % (24 |> int64) = (0 |> int64) then
            this.CurrentFrame <- (this.CurrentFrame + 1) % frames
        elif gameTime.TotalGameTime.Ticks % (5 |> int64) = (0 |> int64) then
            match this.Direction with
                | 0 -> this.WorldPosition <- new Point (this.WorldPosition.X - 1,  + this.WorldPosition.Y - 1)
                | 1 -> this.WorldPosition <- new Point (this.WorldPosition.X + 1,  + this.WorldPosition.Y + 1)
                | 2 -> this.WorldPosition <- new Point (this.WorldPosition.X + 1,  + this.WorldPosition.Y - 1)
                | 3 -> this.WorldPosition <- new Point (this.WorldPosition.X - 1,  + this.WorldPosition.Y + 1)
        elif gameTime.TotalGameTime.Ticks % (171 |> int64) = (0 |> int64) then
            this.Direction <- (this.Direction + 1) % 4
        
    override this.Click () = ()