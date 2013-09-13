namespace XnaIsomRtc.WorldMap

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics


type TileMap(width : int, height : int, spriteBatch : SpriteBatch) = 
    member val Width : int = width with get, set
    member val Height : int = height with get, set
    member val Cells : MapCell array = Array.init (width * height) 
                                                  (fun i -> 
                                                    new MapCell (spriteBatch, CellType.Empty))
    
    // Retrive cell at position (x, y)
    member this.CellAt (x, y) =
        this.Cells.[y * this.Width + x]
    
    
    // indexxy = indicano da che parte della tilemap si deve disegnare
    // startX e startY indicano la posizione del rendering per la cella 0,0
    member this.Draw (index : Point, start : Point, tileSize : int) =
        // dir = 0 -> sin, 1 -> des, 2 -> sindes
        let rec drawLoop x y dir =
            if x < 0 || y < 0 || x >= this.Width || y >= this.Height then ()
            else
                let pos = new Point (start.X + (x + y) * tileSize / 2, 
                                     start.Y + (x - y) * tileSize / 4)
                this.Cells.[y * this.Width + x].Draw (pos, tileSize)
                
                if dir > 0 then
                    drawLoop (x+1) (y) 1
                if dir <> 1 then
                    drawLoop (x) (y-1) 2
                ()
                    
        drawLoop 0 (this.Height - 1) 2
        
        
        