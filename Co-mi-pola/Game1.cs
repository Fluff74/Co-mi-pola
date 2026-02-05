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

        KeyboardState kb; // The current state of the keyboard.
        KeyboardState pkb; // The previous state of the keyboard.
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

        Player player; // The player!!!
        Partner partner; // The player's partner.

        Block floor; // The floor used on most, if not all scenes.
        Block test;

        Vector2 logoPosition; // Where the logo is being written from.
        Button startButton; // Button used to start the game.
        Button settingsButton; // Button used to open settings.
        Button exitButton; // Button used to close the game.

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

            logoPosition = new(36, 13);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            jersey10 = Content.Load<SpriteFont>($"MediumJersey10");
            blockTexture = Content.Load<Texture2D>($"Block");

            player = new(new(3, 50, 9, 13), Content.Load<Texture2D>($"Player"));
            partner = new(new(86, 50, 10, 13), Content.Load<Texture2D>($"Partner"));

            floor = new Block(new(0, 63, 128, 9), blockTexture);
            test = new Block(new(70, 45, 9, 2), blockTexture);

            startButton = new Button(new(40, 40, 8, 8), Content.Load<Texture2D>($"Buttons/StartButtonIdle"), Content.Load<Texture2D>($"Buttons/StartButtonHover"), Content.Load<Texture2D>($"Buttons/StartButtonHeld"));
            settingsButton = new Button(new(60, 40, 8, 8), Content.Load<Texture2D>($"Buttons/SettingsButtonIdle"), Content.Load<Texture2D>($"Buttons/SettingsButtonHover"), Content.Load<Texture2D>($"Buttons/SettingsButtonHeld"));
            exitButton = new Button(new(80, 40, 8, 8), Content.Load<Texture2D>($"Buttons/ExitButtonIdle"), Content.Load<Texture2D>($"Buttons/ExitButtonHover"), Content.Load<Texture2D>($"Buttons/ExitButtonHeld"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (kb.IsKeyDown(Keys.Escape))
                Exit();

            kb = Keyboard.GetState();
            ms = Mouse.GetState();
            mScaled = new((int)(ms.X * (128.0 / _graphics.PreferredBackBufferWidth)), (int)(ms.Y * (72.0 / _graphics.PreferredBackBufferHeight))); // Scale the mouse position based on the resolution.

            switch (gameState)
            {
                case GameState.MainMenu:

                    if(startButton.Update(mScaled, ms, pms) || SingleKeyPress(Keys.Enter))
                    {
                        gameState = GameState.GameScene;
                    }

                    if(settingsButton.Update(mScaled, ms, pms))
                    {

                    }

                    // Closes the game.
                    if(exitButton.Update(mScaled, ms, pms))
                    {
                        Exit();
                    }

                    partner.Update(gameTime);
                    if(partner.ClickedFiveTimes(mScaled, SingleClick()))
                    {
                        swapped = !swapped;
                        SwapColors();
                    }

                    break;

                case GameState.GameScene:

                    player.Update(gameTime, kb, pkb);
                    player.HandleCollisions(floor.Hitbox);
                    player.HandleCollisions(test.Hitbox);

                    break;

                case GameState.EndResult:



                    break;
            }

            pkb = kb;
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

                    startButton.Draw(_spriteBatch, foregroundColor);
                    settingsButton.Draw(_spriteBatch, foregroundColor);
                    exitButton.Draw(_spriteBatch, foregroundColor);

                    floor.Draw(_spriteBatch, foregroundColor);

                    partner.DrawIdle(_spriteBatch, foregroundColor);

                    _spriteBatch.DrawString(jersey10, $"Co mi pola", logoPosition, foregroundColor);

                    break;

                case GameState.GameScene:

                    floor.Draw(_spriteBatch, foregroundColor);
                    test.Draw(_spriteBatch, foregroundColor);

                    player.Draw(_spriteBatch, foregroundColor);

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
        /// <returns> Whether or not the player clicked this frame. </returns>
        public bool SingleClick()
        {
            return ms.LeftButton == ButtonState.Released && pms.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Checks to see if a key has been pressed once.
        /// </summary>
        /// <param name="key"> The key we're checking. </param>
        /// <returns> Whether or not the key was pressed. </returns>
        public bool SingleKeyPress(Keys key)
        {
            return kb.IsKeyDown(key) && pkb.IsKeyUp(key);
        }
    }
}
