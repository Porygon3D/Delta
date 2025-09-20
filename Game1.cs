using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Delta;
//Written By Holden Thompson - SandboxSoftware 
public class Game1 : Game
{
    //Variables
    private GraphicsDeviceManager _graphics;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      //
        base.Initialize();
    }

    protected override void LoadContent()
    {
        //Load Game Assets
        //TODO 
        // Get one game asset rendering on screen.

    }

    protected override void Update(GameTime gameTime)
    {
        //Camera Control
        //play sound()
        //AI logic
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        //Rendering Code Only 

        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {

    }
}



//THIS IS BASE CAMP. OSCAR GOLF FOXTROT DELTA 8. MESSAGE SEND.