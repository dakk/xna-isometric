namespace XnaIsomRtc.Actors

open System
open Microsoft.Xna.Framework

type Person(spriteBatch : Graphics.SpriteBatch) = 
    inherit Actor (spriteBatch)
        
    //override this.Draw (screenPosition : Point, tileSize : int) =
    //    ()