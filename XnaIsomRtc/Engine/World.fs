namespace XnaIsomRtc

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Storage
open Microsoft.Xna.Framework.Input

type World(graphics, window, content) = 
    member val TileMap : WorldMap.TileMap = new WorldMap.TileMap (0, 0, null) with get, set
    member val MapTiles : Map<int,(Graphics.Texture2D * Vector2)> = Map.empty with get, set // Texture * Offset
    
    member val Actors : Actors.Actor list = [] with get, set
    
    member val Graphics : GraphicsDeviceManager = graphics with get, set
    member val SpriteBatch : Graphics.SpriteBatch = new Graphics.SpriteBatch (graphics.GraphicsDevice) with get, set
    member val Window : GameWindow = window with get, set
    member val Content : Content.ContentManager = content with get, set
    
    member val ViewPosition : Point = new Point (0, 0) with get, set


    member this.Initialize () =
        this.ViewPosition <- new Point (0, this.Window.ClientBounds.Height / 2 - 16)
        
        
    member this.LoadContent () =
        // Create empty 2x2 tilemap
        this.TileMap <- new WorldMap.TileMap (11, 11, this.SpriteBatch)
        
        // Load maptiles
        this.MapTiles <- this.MapTiles.Add (1, (this.Content.Load<Graphics.Texture2D>("Tiles/Map/grass"), Vector2.Zero))
        this.MapTiles <- this.MapTiles.Add (2, (this.Content.Load<Graphics.Texture2D>("Tiles/Map/sand"), Vector2.Zero))
        this.MapTiles <- this.MapTiles.Add (3, (this.Content.Load<Graphics.Texture2D>("Tiles/Map/tree1_00"), new Vector2 (8.0f, -16.0f)))
        this.MapTiles <- this.MapTiles.Add (4, (this.Content.Load<Graphics.Texture2D>("Tiles/Map/tree2_00"), new Vector2 (8.0f, -16.0f)))
        this.MapTiles <- this.MapTiles.Add (5, (this.Content.Load<Graphics.Texture2D>("Tiles/Map/tree3_00"), new Vector2 (8.0f, -16.0f)))
        this.MapTiles <- this.MapTiles.Add (6, (this.Content.Load<Graphics.Texture2D>("Tiles/Map/tree4_00"), new Vector2 (8.0f, -16.0f)))
        this.MapTiles <- this.MapTiles.Add (7, (this.Content.Load<Graphics.Texture2D>("Tiles/Map/tree7_00"), new Vector2 (8.0f, -16.0f)))
        this.MapTiles <- this.MapTiles.Add (8, (this.Content.Load<Graphics.Texture2D>("Tiles/Map/tree8_00"), new Vector2 (8.0f, -16.0f)))
        
        // Create the map. This will be replaced with a map loader
        let rand = new Random()
        for x in 0 .. this.TileMap.Width - 1 do
            for y in 0 .. this.TileMap.Height - 1 do
                (this.TileMap.CellAt (x, y)).Type <- WorldMap.CellType.Empty
                //(this.map.CellAt (x, y)).BaseColor <- Color.FromNonPremultiplied(150+x*10, y*5, 0, 255)
                (this.TileMap.CellAt (x, y)).AddBaseTile (this.MapTiles.[1]) |> ignore
                if (rand.Next() |> float) % 10.0 > 7.0 then
                    let tree = (((rand.Next() |> float) % 5.0) |> int) + 3
                    (this.TileMap.CellAt (x, y)).AddTilesLevel ([this.MapTiles.[tree]]) |> ignore
                    
        // Put some persons
        this.Actors <- 
          [ 
            new Actors.Person (this.SpriteBatch, this.Content.Load<Graphics.Texture2D>("Actors/Person/Set1"), (11,19), 6, new Point (200, 100)) 
          ]
                                          
    member this.Update (gameTime) =
        let rec update_actors (acts : Actors.Actor list) =
            match acts with
                | [] -> ()
                | a::al -> a.Update (gameTime); update_actors al
                
        update_actors this.Actors
        
                                
    member this.Draw (gameTime) =
        // Draw the tilemap
        this.SpriteBatch.Begin ()
        this.TileMap.Draw (new Point (0, 0), this.ViewPosition, 64)
        
        let rec draw_actors (acts : Actors.Actor list) =
            match acts with
                | [] -> ()
                | a::al -> a.Draw (gameTime, this.ViewPosition, 12); draw_actors al
                
        draw_actors this.Actors
        
        this.SpriteBatch.End ()
        
        