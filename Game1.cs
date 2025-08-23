using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata.Ecma335;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Delta;
//Written By Holden Thompson - SandboxSoftware 
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch spriteBatch;
    private TimeSpan accumulator = TimeSpan.Zero;
    private KeyboardState _previousKeyboard;
    private MouseState _previousMouse;
    private GamePadState _previousGamePad;


    public Game1()
    {
        IsFixedTimeStep = true; //Toggle if you want to maximize framerate at the cost of flucuating update intervals
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 60); // 60 FPS
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        // set up services, managers and any non graphics states here.
        base.Initialize();


        _previousKeyboard = Keyboard.GetState();
        _previousMouse = Mouse.GetState();
        _previousGamePad = GamePad.GetState(PlayerIndex.One); //Assuming single player for now

    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // load textures fonts audio via this.Content here.
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here
        //poll input advance game state handle AI and physics


        var currentKeyboard = Keyboard.GetState();
        var currentMouse = Mouse.GetState();
        var currentGamePad = GamePad.GetState(PlayerIndex.One);

        //handle input events here for now until it makes sense to put them into a InputManager class

        bool IsKeyPressed(Keys key) => currentKeyboard.IsKeyDown(key) && _previousKeyboard.IsKeyDown(key);

        bool IsKeyReleased(Keys key) => currentKeyboard.IsKeyDown(key);

        IsKeyPressed(Keys.Space); 
        IsKeyReleased(Keys.Space);

        Point mousePostion = new Point(currentMouse.X, currentMouse.Y);
        if(currentMouse.LeftButton == ButtonState.Pressed &&  _previousMouse.LeftButton == ButtonState.Released)
        {
            //Handle Left Mouse Click
            //Place an object at the mousePosition
            Console.WriteLine($"Left Mouse Clicked at {mousePostion}");
        }

        //Read Scroll Wheel Value
        int scrollDelta = currentMouse.ScrollWheelValue - _previousMouse.ScrollWheelValue;
        if(scrollDelta != 0)
        {
            //Zoom camera in or out.
            Console.WriteLine($"Mouse Wheel Moved by {scrollDelta}");
        }

        //Handle GamePad Input 
        if(currentGamePad.IsConnected)
        {
            // Replace the IsButtonPressed method inside Update with the following:

            bool IsButtonPressed(Buttons button)
            {
                // Use the appropriate property for each button, since GamePadButtons does not support indexing.
                // Example for the A button:
                if (button == Buttons.A)
                    return currentGamePad.Buttons.A == ButtonState.Pressed && _previousGamePad.Buttons.A == ButtonState.Released;
                if (button == Buttons.B)
                    return currentGamePad.Buttons.B == ButtonState.Pressed && _previousGamePad.Buttons.B == ButtonState.Released;
                if (button == Buttons.X)
                    return currentGamePad.Buttons.X == ButtonState.Pressed && _previousGamePad.Buttons.X == ButtonState.Released;
                if (button == Buttons.Y)
                    return currentGamePad.Buttons.Y == ButtonState.Pressed && _previousGamePad.Buttons.Y == ButtonState.Released;
                if (button == Buttons.Back)
                    return currentGamePad.Buttons.Back == ButtonState.Pressed && _previousGamePad.Buttons.Back == ButtonState.Released;
                if (button == Buttons.Start)
                    return currentGamePad.Buttons.Start == ButtonState.Pressed && _previousGamePad.Buttons.Start == ButtonState.Released;
                if (button == Buttons.LeftShoulder)
                    return currentGamePad.Buttons.LeftShoulder == ButtonState.Pressed && _previousGamePad.Buttons.LeftShoulder == ButtonState.Released;
                if (button == Buttons.RightShoulder)
                    return currentGamePad.Buttons.RightShoulder == ButtonState.Pressed && _previousGamePad.Buttons.RightShoulder == ButtonState.Released;
                if (button == Buttons.LeftStick)
                    return currentGamePad.Buttons.LeftStick == ButtonState.Pressed && _previousGamePad.Buttons.LeftStick == ButtonState.Released;
                if (button == Buttons.RightStick)
                    return currentGamePad.Buttons.RightStick == ButtonState.Pressed && _previousGamePad.Buttons.RightStick == ButtonState.Released;
                if (button == Buttons.BigButton)
                    return currentGamePad.Buttons.BigButton == ButtonState.Pressed && _previousGamePad.Buttons.BigButton == ButtonState.Released;
                return false;
            }

            IsButtonPressed(Buttons.A);
            
            
            //Analog Stick Detection
            Vector2 move=currentGamePad.ThumbSticks.Left;



            //Trigger Detection
            float throttle = currentGamePad.Triggers.Right;

            //Rumble Detection
            GamePad.SetVibration(PlayerIndex.One, 0.5f * throttle, 0.5f * throttle); //Set vibration based on right trigger value
        }


        //Store for next frame
        _previousKeyboard = currentKeyboard;
        _previousMouse = currentMouse;
        _previousGamePad = currentGamePad;

        accumulator += gameTime.ElapsedGameTime;
        while (accumulator >= TargetElapsedTime)
        {
            //SimulatePhysics((float)TargetElapsedTime.TotalSeconds); //Create this method in other class definition
            accumulator -= TargetElapsedTime;
        }

            //Game time object contains ElapsedGameTime: Time since the last call. and TotalGameTime: Total runtime since the game started properties 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

    




        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds; //time since last update in seconds
        //float velocity = 10f; //units per second
        //var position += velocity * delta; //simple Euler integration 



        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();
        //spriteBatch.Draw(playerTexture, position, Color.White); // Need Player Texture Configured
        spriteBatch.End(); base.Draw(gameTime);

        // TODO: Add your drawing code here
        // Issue all draw calls here
        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        //dispose unmanaged resources if needed
    }
}
