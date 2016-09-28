using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace arkanoid
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		const int VIEWPORT_WIDTH = 640;
		const int VIEWPORT_HEIGHT = 480;
		const int PLATFORM_PADDING = 10;
		const int PLATFORM_HEIGHT = 10;

        private Texture2D background;

        private const float timeToNextUpdate = 1.0f / 30.0f;
        private float timeSinceLastUpdate;

		public struct GameObject
		{
			public Texture2D texture;
			public int x;
			public int y;
			public int width;
			public int height;
		}

		private GameObject platform;
		private GameObject ball;

		public Game1() 
        {
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferHeight = VIEWPORT_HEIGHT;
			graphics.PreferredBackBufferWidth = VIEWPORT_WIDTH;

            Content.RootDirectory = "Content";

			platform.width = VIEWPORT_WIDTH / 10;
			platform.height = PLATFORM_HEIGHT;
			platform.x = (VIEWPORT_WIDTH - platform.width) / 2;
			platform.y = VIEWPORT_HEIGHT - PLATFORM_PADDING - platform.height;

			ball.width = 20;
			ball.height = 20;
			ball.x = (VIEWPORT_WIDTH - ball.width) / 2;
			ball.y = VIEWPORT_HEIGHT - PLATFORM_PADDING - platform.height - ball.height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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

            background = this.Content.Load<Texture2D>("background");
			platform.texture = this.Content.Load<Texture2D>("blank");
			ball.texture = this.Content.Load<Texture2D>("ball");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            #if !__IOS__ && !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {         
                Exit();
            }
            #endif

            timeSinceLastUpdate += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastUpdate >= timeToNextUpdate)
            {
                timeSinceLastUpdate = 0;
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin(SpriteSortMode.Immediate);

            spriteBatch.Draw(
				background,
				new Rectangle(0, 0, VIEWPORT_WIDTH, VIEWPORT_HEIGHT),
				Color.White
			);

			spriteBatch.Draw(
				platform.texture, 
				new Rectangle(platform.x, platform.y, platform.width, platform.height), 
				Color.White
			);

			spriteBatch.Draw(
				ball.texture,
				new Rectangle(ball.x, ball.y, ball.width, ball.height),
				Color.White
			);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
