namespace XnaIsomRtc

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Storage
open Microsoft.Xna.Framework.Input

type IsoGame() as this = 
    inherit Game ()
    do
        this.Content.RootDirectory <- "Content"
        //this.graphics.IsFullScreen <- true
        
    member val graphics : GraphicsDeviceManager = new GraphicsDeviceManager (this) with get, set
    member val spriteBatch : SpriteBatch = null with get, set
    
    [<DefaultValue>] val mutable uiManager : UI.UiManager
    [<DefaultValue>] val mutable world : World
    
    
    override this.Initialize () =
        // Create and initialize the world ovject
        this.world <- new World (this.graphics, this.Window, this.Content)
        this.uiManager <- new UI.UiManager (this)
        
        this.world.Initialize ()

        // Create UI
        this.uiManager.Initialize ()
        //this.uiManager.AddWindow (new UI.Window (new Rectangle (90, 50, 200, 100), "Hello Window"))
        
        base.Initialize ()
        
        
        
    // Load content, graphics and audio
    override this.LoadContent () =
        this.spriteBatch <- new SpriteBatch (this.graphics.GraphicsDevice)
            
        // Load world content
        this.world.LoadContent ()
        this.uiManager.LoadContent ()      
        
        
        
        
        
    override this.Update (gameTime) =
        this.world.Update (gameTime)
        
        if GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed then
            this.Exit()
        
        let keyboardState = Keyboard.GetState ()
        
        // should be proportionated with the framerate: it doesn't
        if keyboardState.IsKeyDown(Keys.Down) then
            this.world.ViewPosition <- 
                new Point (this.world.ViewPosition.X, this.world.ViewPosition.Y - 1)
            
        elif keyboardState.IsKeyDown(Keys.Up) then
            this.world.ViewPosition <- 
                new Point (this.world.ViewPosition.X, this.world.ViewPosition.Y + 1)
                
                
        if keyboardState.IsKeyDown(Keys.Right) then
            this.world.ViewPosition <- 
                new Point (this.world.ViewPosition.X - 1, this.world.ViewPosition.Y)
            
        elif keyboardState.IsKeyDown(Keys.Left) then
            this.world.ViewPosition <- 
                new Point (this.world.ViewPosition.X + 1, this.world.ViewPosition.Y)
            
        base.Update (gameTime)
        
        
        
    override this.Draw (gameTime) =
        this.graphics.GraphicsDevice.Clear (Color.CornflowerBlue)
        
        // Draw world and ui
        this.world.Draw (gameTime)
        this.uiManager.Draw (gameTime)
        
        
        base.Draw (gameTime)