using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Co_mi_pola
{
    // Joshua Smith
    // 02/02/2026
    //
    // Co mi pola! It's a cool game idea that I'm going to try to finish before Valentine's Day. Woo crunch time!!!
    public class Game1 : Game
    {
        #region Utilities
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;
        private Random _random;
        #endregion

        /// <summary>
        /// The current state that the game is in.
        /// </summary>
        enum GameState
        {
            MainMenu,
            GameScene,
            EndResult
        }
        GameState gameState;

        MouseState ms; // The current state of the mouse.
        MouseState pms; // The previous state of the mouse.
        Point mScaled; // The location of the mouse, scaled down to the resolution of the game.

        SpriteFont jersey10; // The font used to write the title, and debug.
        Texture2D blockTexture; // A 1x1 pixel used to render all blocks.

        #region Colors
        Color foregroundColor; // The color of the player, ground, and everything other than the background.
        Color backgroundColor; // The color of the background.
        Color[] foregroundColors; // All possible colors that can be in the foreground.
        Color[] backgroundColors; // All possible colors that can be in the background.
        bool swapped; // Whether or not we've swapped the color palettes. Could be a cute easter egg.
        #endregion

        Partner partner; // The player's partner.

        Block floor; // The floor used on most, if not all scenes.

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            #region Screen Resolution
            // Grab the detected graphics device's width and height. I might add more resolution options later if it's within
            // scope, but for now this should work just fine.
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
            Window.IsBorderless = true;
            #endregion
        }

        protected override void Initialize()
        {
            #region Utilities
            _renderTarget = new RenderTarget2D(GraphicsDevice, 128, 72); // Create the RenderTarget.
            _random = new();
            #endregion

            gameState = GameState.MainMenu; // Start the player off on the main menu.

            #region Colors
            // Initialize the color palettes, and all color related variables.
            foregroundColors = [new(64, 8, 0), new(79, 61, 0), new(79, 78, 0), new(19, 69, 12), new(0, 64, 66), new(0, 9, 69), new(66, 0, 79), new(74, 0, 41), new(26, 26, 26)];
            backgroundColors = [new(255, 94, 94), new(255, 186, 74), new(237, 225, 90), new(134, 224, 123), new(49, 228, 235), new(62, 83, 222), new(177, 41, 204), new(222, 78, 198), new(214, 214, 214)];
            swapped = false;
            RandomizePalette(swapped);
            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            jersey10 = Content.Load<SpriteFont>($"MediumJersey10");
            blockTexture = Content.Load<Texture2D>($"Block");

            partner = new(new(86, 50, 10, 13), Content.Load<Texture2D>($"Partner"));

            floor = new Block(new(0, 63, 128, 9), blockTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ms = Mouse.GetState();
            mScaled = new((int)(ms.X * (128.0 / _graphics.PreferredBackBufferWidth)), (int)(ms.Y * (72.0 / _graphics.PreferredBackBufferHeight))); // Scale the mouse position based on the resolution.

            switch (gameState)
            {
                case GameState.MainMenu:

                    partner.Update(gameTime);
                    if(partner.ClickedFiveTimes(mScaled, SingleClick(ms, pms)))
                    {
                        swapped = !swapped;
                        SwapColors();
                    }

                    break;

                case GameState.GameScene:



                    break;

                case GameState.EndResult:



                    break;
            }

            pms = ms;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            #region Begin Draw
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(backgroundColor);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            #endregion

            switch(gameState)
            {
                case GameState.MainMenu:

                    floor.Draw(_spriteBatch, foregroundColor);
                    partner.DrawIdle(_spriteBatch, foregroundColor);

                    _spriteBatch.DrawString(jersey10, $"Co mi pola", new(1, 1), foregroundColor);

                    break;

                case GameState.GameScene:



                    break;

                case GameState.EndResult:



                    break;
            }

            #region End Draw
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            base.Draw(gameTime);
            #endregion
            #region Exec Render Call
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, 1920, 1080), Color.White);
            _spriteBatch.End();
            #endregion
        }

        /// <summary>
        /// Randomizes the colors that are used in game. This should be called between scenes, and on game start.
        /// </summary>
        /// <param name="swapped"> Whether or not to swap the foreground and background color palettes, since this might be a cool easter egg. </param>
        public void RandomizePalette(bool swapped)
        {
            if(swapped)
            {
                foregroundColor = backgroundColors[_random.Next(0, 9)];
                backgroundColor = foregroundColors[_random.Next(0, 9)];
            }
            else
            {
                foregroundColor = foregroundColors[_random.Next(0, 9)];
                backgroundColor = backgroundColors[_random.Next(0, 9)];
            }
        }

        /// <summary>
        /// Swap the foreground and background colors.
        /// </summary>
        public void SwapColors()
        {
            (backgroundColor, foregroundColor) = (foregroundColor, backgroundColor);
        }

        /// <summary>
        /// Checks to see if the player has clicked this frame.
        /// </summary>
        /// <param name="ms"> The current state of the mouse. </param>
        /// <param name="pms"> The previous state of the mouse. </param>
        /// <returns> Whether or not the player clicked this frame. </returns>
        public static bool SingleClick(MouseState ms, MouseState pms)
        {
            return ms.LeftButton == ButtonState.Released && pms.LeftButton == ButtonState.Pressed;
        }
    }
}
