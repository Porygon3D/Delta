using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using MouseState = Microsoft.Xna.Framework.Input.MouseState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using System.Collections.Generic;
using MonoGame.Extended.Graphics;
using MonoGame.Extended;


namespace Delta;
//Written By Holden Thompson - SandboxSoftware 
public class Game1 : Game
{



    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TimeSpan accumulator = TimeSpan.Zero;
    private KeyboardState _previousKeyboard;
    private MouseState _previousMouse;
    private GamePadState _previousGamePad;
    //private Texture2D _playerTexture;
    private readonly Dictionary<string, Texture2D> _textures = new(); //Dictionary to hold loaded textures
    private Texture2DAtlas _atlas; //Example of using MonoGame.Extended for texture atlases
    private const int VirtualWidth = 640;
    private const int VirtualHeight = 480;
    private RenderTarget2D _virtualTarget;
    private RenderTarget2D _sceneTarget;
    private Effect _postProcessEffect;
    //private Effect _shader;
    private Camera _camera;
    private Model myModel;




    public Game1()
    {
        /*
         * Main Game Constructor - set up graphics and game properties here.
         * Order of execution is Initalize > Load > Update(60hz) > Draw(60hz) > UnloadContent > repeat until end of time
         */

        IsFixedTimeStep = true; //Toggle if you want to maximize framerate at the cost of flucuating update intervals
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 60); // 60 FPS
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        /*
         * TODO: Add your initialization logic here
         * set up services, managers and any non graphics states here.
        */

        //Content.RootDirectory = "Content";

        _camera = new Camera(
            new Vector3(0, 250, 1000),
            0f,
            -MathHelper.ToRadians(25f),
            GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height
        );


        _previousKeyboard = Keyboard.GetState();
        _previousMouse = Mouse.GetState();
        _previousGamePad = GamePad.GetState(PlayerIndex.One); //Assuming single player for now

        base.Initialize();

    }

    protected override void LoadContent()
    {
        /*
         load textures fonts audio via this.Content here.
         TODO: use this.Content to load your game content here
        */
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //_shader = Content.Load<Effect>("Effects/MyShader");
        //var texture = Content.Load<Texture2D>("Texture/cards");//Load the texture atlas image
        //_atlas = Texture2DAtlas.Create("CardsAtlas", texture, 32, 32); //Example of using MonoGame.Extended to create a texture atlas

        //string[] assetNames = { "Texture/player" }; // Add more asset names as needed
        //foreach (var name in assetNames) _textures[name] = Content.Load<Texture2D>(name);

        myModel = Content.Load<Model>("Models/Porygon");

        //_playerTexture = Content.Load<Texture2D>("Texture/player"); //Load a player texure to memory
        _sceneTarget = new RenderTarget2D(
            GraphicsDevice,
            VirtualWidth,
            VirtualHeight,
            false,
            GraphicsDevice.PresentationParameters.BackBufferFormat,
            DepthFormat.None
            );

        _virtualTarget = new RenderTarget2D(GraphicsDevice,
            VirtualWidth,
            VirtualHeight,
            false,
            GraphicsDevice.PresentationParameters.BackBufferFormat,
            DepthFormat.None
            );

        //_postProcessEffect = Content.Load<Effect>("Effects/Grayscale"); //Example

    }

    protected override void Update(GameTime gameTime)
    {
        /* 
         TODO: Add your update logic here
         poll input advance game state handle AI and physics
        */

        var currentKeyboard = Keyboard.GetState();
        var currentMouse = Mouse.GetState();
        var currentGamePad = GamePad.GetState(PlayerIndex.One);

        //handle input events here for now until it makes sense to put them into a InputManager class

        bool IsKeyPressed(Keys key) => currentKeyboard.IsKeyDown(key) && _previousKeyboard.IsKeyDown(key);

        bool IsKeyReleased(Keys key) => currentKeyboard.IsKeyDown(key);

        IsKeyPressed(Keys.Space);
        IsKeyReleased(Keys.Space);

        Point mousePostion = new Point(currentMouse.X, currentMouse.Y);
        if (currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released)
        {
            //Handle Left Mouse Click
            //Place an object at the mousePosition
            Console.WriteLine($"Left Mouse Clicked at {mousePostion}");
        }

        //Read Scroll Wheel Value
        int scrollDelta = currentMouse.ScrollWheelValue - _previousMouse.ScrollWheelValue;
        if (scrollDelta != 0)
        {
            //Zoom camera in or out.
            Console.WriteLine($"Mouse Wheel Moved by {scrollDelta}");
        }

        //Handle GamePad Input 
        if (currentGamePad.IsConnected)
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
            Vector2 move = currentGamePad.ThumbSticks.Left;


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
        /*
        TODO: Add your drawing code here
        Issue all draw calls here
        As project grows need to move the drawing code into a separate class to manage layers and spritebatches

        TODO overide OnClientSizeChanged event to update the viewport dimensions
        */

        /* 
         Summary of the drawing code.
        Render to _virutalTarget which is size 800x600.
        Compute Scale Calculate the ratio between the real screen and the virual one on both axis then pick the smaller one this guaranteees the entire canvas fits without cropping
        Letterbox Handling: Offset the center of the scaled canvas and leave black bars on either sides or top/buttom maintaining aspect ratio
        TransformMatrix: Apply the matrix in Spritebatch.begin scales and translates virtual canvas back onto the real back buffer in a single pass
         */

        //Redirect all drawing into the offscreen buffer
        GraphicsDevice.SetRenderTarget(_sceneTarget);
        GraphicsDevice.BlendState = BlendState.Opaque;
        GraphicsDevice.RasterizerState = RasterizerState.CullNone;
        GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        GraphicsDevice.DepthStencilState = DepthStencilState.Default;




        //Step 1 Draw game scene into the virtual camera 
        GraphicsDevice.SetRenderTarget(_virtualTarget);
        //GraphicsDevice.Clear(Color.CornflowerBlue);

        //Matrix wvp = _camera.World *_camera.View * _camera.Projection;
        //_shader.Parameters["WorldViewProj"].SetValue(wvp);

        

        _spriteBatch.Begin(
            sortMode: SpriteSortMode.Deferred, //Calls until end minimizing state changes
            blendState: BlendState.AlphaBlend, //Enable transparency
            samplerState: SamplerState.PointClamp //Gives crisp pixel art scaling
            );
        //spriteBatch.Draw(); //all draw calls use the Virtual coords
        _spriteBatch.End();
        base.Draw(gameTime);

        //Step 2 Reset to the back buffer (the actual screen)
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //Step 3 Compute scaling and letterbox offsets
        float scaleX = (float)GraphicsDevice.Viewport.Width / VirtualWidth;
        float scaleY = (float)GraphicsDevice.Viewport.Height / VirtualHeight;

        //Step 4 Center the Canvas
        float offsetX = (GraphicsDevice.Viewport.Width - VirtualWidth * 2f);
        float offsetY = (GraphicsDevice.Viewport.Height - VirtualHeight * 2f);

        var transform = Matrix.CreateTranslation(-0f, -0f, 0f)
            * Matrix.CreateScale(scaleX, scaleY, 1f)
            * Matrix.CreateScale(offsetX, offsetY, 0f);

        //Step 4 Draw the scaled virtual canvas
        _spriteBatch.Begin(transformMatrix: transform);
        _spriteBatch.Draw(_virtualTarget, Vector2.Zero, Color.White);
        _spriteBatch.End();

        DrawModel(myModel, Matrix.Identity,_camera.ViewMatrix, _camera.ProjectionMatrix);
        base.Draw(gameTime);

    }

    protected override void UnloadContent()
    {
        /*
        dispose unmanaged resources if needed
        */

        foreach (var texutre in _textures.Values) texutre.Dispose();

        //_playerTexture.Dispose(); Not needed until player texture set
        _spriteBatch.Dispose();
    }

    void DrawModel(Model aModel, Matrix aWorld, Matrix aView, Matrix aProjection)
    {
        //Copy any parent transforms
        Matrix[] transforms = new Matrix[aModel.Bones.Count];
        aModel.CopyAbsoluteBoneTransformsTo(transforms);

        //Draw the model, a model can have multiple meshes, so loop
        foreach (ModelMesh mesh in aModel.Meshes)
        {
            //This is where the mesh orientation is set, as well as our camera and projection
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.EnableDefaultLighting();
                effect.PreferPerPixelLighting = true;
                effect.World = transforms[mesh.ParentBone.Index] * aWorld;
                effect.View = aView;
                effect.Projection = aProjection;
            }

            //Draw the mesh, will use the effects set above.
            mesh.Draw();
        }
    }
}
