namespace XnaIsomRtc.UI

open System
open Microsoft.Xna.Framework

type Drawer(game : Game) = 
    member val SpriteFont : Graphics.SpriteFont = null with get, set
    member val SpriteBatch : Graphics.SpriteBatch = new Graphics.SpriteBatch (game.GraphicsDevice) with get, set
    member val LineEffect : Graphics.BasicEffect = new Graphics.BasicEffect(game.GraphicsDevice)
    
    member this.Initialize () =
        this.LineEffect.VertexColorEnabled <- true
        this.LineEffect.Projection <- Matrix.CreateOrthographicOffCenter 
                                        (0.0f, game.GraphicsDevice.Viewport.Width |> float32,
                                         game.GraphicsDevice.Viewport.Height |> float32, 0.0f, 0.0f, 1.0f)  
                                         
                                         
    // Draw an array of lines
    member this.DrawLines (lines : Graphics.VertexPositionColor array) =
        this.LineEffect.CurrentTechnique.Passes.[0].Apply()
        game.GraphicsDevice.DrawUserPrimitives<Graphics.VertexPositionColor>(Graphics.PrimitiveType.LineList, 
                                                                             lines, 0, lines.Length)
    
    // Draw text
    member this.DrawText (text : string, rect : Rectangle, color : Color) =
        this.SpriteBatch.Begin ()
        this.SpriteBatch.DrawString(this.SpriteFont, text, new Vector2 (rect.X |> float32, rect.Y |> float32), color)
        this.SpriteBatch.End ()
        
    // Draw a quad
    member this.DrawRectangle (rect : Rectangle, color : Color) =
        let recta = new Graphics.Texture2D(game.GraphicsDevice, rect.Width, rect.Height)      
        recta.SetData (Array.create (rect.Width*rect.Height) (color))
        
        this.SpriteBatch.Begin ()
        this.SpriteBatch.Draw (recta, new Vector2(rect.X |> float32, rect.Y |> float32), color)        
        this.SpriteBatch.End ()
        
                    