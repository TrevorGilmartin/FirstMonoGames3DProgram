using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DModellingCodingExercise
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager inputManager;
        Camera camera;

        int totalVertices = 0;

        SampleModel shipModel;
        SampleModel earth;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            inputManager = new InputManager(this);
            camera = new Camera(this, new Vector3(0, 0, 1), new Vector3(0, 0, -1));

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            shipModel = new SampleModel("ship", new Vector3(0, 0, -50), 1);
            shipModel.LoadContent(Content);

            earth = new SampleModel("earth", new Vector3(0, 0, -100), 2);
            earth.LoadContent(Content);

            totalVertices += shipModel.vertexCount;

            //Load Other Models Here
            Window.Title = "Vertcices: " + totalVertices;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            shipModel.Draw(camera.View, camera.Projection);
            earth.Draw(camera.View, camera.Projection);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void RotateModelAroundOrigin(SampleModel model, Matrix rotation)
        {
            var location = model.world.Translation;

            model.world *= Matrix.CreateTranslation(-location);
            model.world *= rotation;
            model.world *= Matrix.CreateTranslation(location);
        }
    }
}
