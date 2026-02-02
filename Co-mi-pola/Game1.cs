using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            #endregion

            switch(gameState)
            {
                case GameState.MainMenu:



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
    }
}
