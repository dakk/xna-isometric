namespace XnaIsomRtc.UI

open System
open Microsoft.Xna.Framework

type Window(?bound : Rectangle, ?title : string) = 
    member val Title : string = (defaultArg title "Window") with get, set
    member val Bound : Rectangle = (defaultArg bound (new Rectangle(100, 100, 100, 100))) with get, set
    
    [<DefaultValue>] val mutable DrawerHelp : Drawer
    
    
    member this.Draw () =
        this.DrawerHelp.DrawRectangle (this.Bound, new Color(183, 195, 195))
        this.DrawerHelp.DrawRectangle (new Rectangle (this.Bound.X + 1, this.Bound.Y + 1, 
                                                      this.Bound.Width - 2, this.Bound.Height - 2), 
                                       new Color(131, 151, 151))
                                       
        
        this.DrawerHelp.DrawRectangle (new Rectangle (this.Bound.X + 3, this.Bound.Y + 3, 
                                                      this.Bound.Width - 6, 16), 
                                       new Color(91, 115, 115))

        this.DrawerHelp.DrawText (this.Title, new Rectangle (this.Bound.X + 5, this.Bound.Y + 2, 0, 0), Color.White)
