namespace XnaIsomRtc.UI

open System
open Microsoft.Xna.Framework

type UiManager(game : Game) = 
    member val Windows : Window list = [] with get, set
    member val SpriteBatch : Graphics.SpriteBatch = new Graphics.SpriteBatch (game.GraphicsDevice) with get, set
    member val DrawerHelp : Drawer = new Drawer (game) with get, set
    
    member this.Initialize () =
        this.DrawerHelp.Initialize ()
                
    member this.LoadContent () =
        this.DrawerHelp.SpriteFont <- game.Content.Load<Graphics.SpriteFont>("Fonts/SpriteFont_2")
        
        
    // Add a window    
    member this.AddWindow (win : Window) =
        this.Windows <- this.Windows @ [ win ]
        win.DrawerHelp <- this.DrawerHelp
        
                    
    // Draw UI
    member this.Draw (gameTime) =
        let rec drawWindows (wins : Window list) =                  
            match wins with
                | [] -> ()
                | w::wl -> w.Draw (); drawWindows wl
                
        this.SpriteBatch.Begin ()                  
        drawWindows this.Windows
        this.SpriteBatch.End ()