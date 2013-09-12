namespace XnaIsomRtc.WorldMap

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type MapCell(spriteBatch : SpriteBatch, cellType : CellType) =
    member val Type : CellType = cellType with get, set
    member val BaseTile : (Texture2D * Vector2) list = [] with get, set
    member val HeightTiles : (Texture2D * Vector2) list list = [] with get, set
    member val BaseColor : Color = Color.White with get, set
    
    member this.AddBaseTile (tile : (Texture2D * Vector2)) =
        this.BaseTile <- this.BaseTile @ [ tile ]
        this

    member this.AddTilesLevel (tiles : (Texture2D * Vector2) list) =
        this.HeightTiles <- this.HeightTiles @ [ tiles ]
        this

    // <param n="tileSize">x size of the tile 32x16, 64x32, 128x64</param>
    member this.Draw (screenPosition : Point, tileSize : int) =
        let rec draw_base (texList : (Texture2D * Vector2) list) (position : Vector2) =
            match texList with
                | [] -> ()
                | tex::texList' ->
                    // devo scalarlo
                    spriteBatch.Draw (fst tex, position + (snd tex), this.BaseColor)
                    draw_base texList' position
                    
        let rec draw_heights (texList : (Texture2D * Vector2) list list) (position : Vector2) =
            match texList with
                | [] -> ()
                | tex::texList' ->
                    draw_base tex position
                    draw_heights texList' position //pos up!
                           
        draw_base this.BaseTile (new Vector2 (screenPosition.X |> float32, screenPosition.Y |> float32))
        draw_heights this.HeightTiles (new Vector2 (screenPosition.X |> float32, screenPosition.Y |> float32))
        
        
        
        