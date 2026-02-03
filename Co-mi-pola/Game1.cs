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

        Texture2D blockTexture; // A 1x1 pixel used to render all blocks.

        #region Colors
        Color foregroundColor; // The color of the player, ground, and everything other than the background.
        Color backgroundColor; // The color of the background.
        Color[] foregroundColors; // All possible colors that can be in the foreground.
        Color[] backgroundColors; // All possible colors that can be in the background.
        bool swapped; // Whether or not we've swapped the color palettes. Could be a cute easter egg.
        #endregion

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

            blockTexture = Content.Load<Texture2D>($"Block");
            floor = new Block(new Rectangle(0, 63, 128, 9), blockTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.MainMenu:



                    break;

                case GameState.GameScene:



                    break;

                case GameState.EndResult:



                    break;
            }

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
    }
}
