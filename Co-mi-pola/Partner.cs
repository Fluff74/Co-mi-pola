using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Co_mi_pola
{
    // Joshua Smith
    // 02/03/2026
    //
    // This is the player's romantic partner in the game, and basically the point that they need to reach in order to finish
    // each of the scenes.
    internal class Partner(Rectangle hitbox, Texture2D spritesheet)
    {
        /// <summary>
        /// The hitbox of the partner.
        /// </summary>
        public Rectangle Hitbox { get; set; } = hitbox;

        // All of the information needed to animate the partner.
        private readonly Rectangle firstFrame = new(0, 0, 10, 13);
        private Rectangle source = new(0, 0, 10, 13);
        private int curFrame = 0;
        private readonly int totalFrames = 1;
        private double frameTimer = 0.0;
        private readonly double framesPerSecond = 1.0;

        // The total times that the partner has been clicked on. Needed for palette swap easter egg.
        private ushort totalClicks;

        /// <summary>
        /// Moves the partner someplace else on screen.
        /// </summary>
        /// <param name="x"> The X coordinate we're moving the partner to. </param>
        /// <param name="y"> The Y coordinate we're moving the partner to. </param>
        public void MoveAbsolute(int x, int y)
        {
            Rectangle newHitbox = new(x, y, 10, 13);
            Hitbox = newHitbox;
        }

        /// <summary>
        /// Updates the partner's animation.
        /// </summary>
        /// <param name="gameTime"> The elapsed time in the game. </param>
        public void Update(GameTime gameTime)
        {
            // Increment the frame timer based on elapsed time.
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Increment the frame counter, if necessary.
            if(frameTimer >= framesPerSecond)
            {
                frameTimer = 0.0; // Reset frame timer.
                curFrame++;

                // Prevent frame counter from overflowing.
                if(curFrame > totalFrames)
                {
                    curFrame = 0;
                }

                // Move the source rectangle accordingly.
                source.X = 10 * curFrame;
            }
        }

        /// <summary>
        /// Enables the player to click their partner to swap the color palettes, as an easter egg.
        /// </summary>
        /// <param name="mScaled"> The location of the mouse, scaled down to 128x72 resolution. </param>
        /// <param name="clicked"> Whether or not the player clicked this frame. </param>
        /// <returns> Whether or not the partner has been clicked five times. </returns>
        public bool ClickedFiveTimes(Point mScaled, bool clicked)
        {
            // Increment the total clicks, if the player clicked their partner.
            if(Hitbox.Contains(mScaled) && clicked)
            {
                totalClicks++;
            }

            // If the partner has been clicked five times, return true.
            if(totalClicks >= 5)
            {
                totalClicks = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Draws the partner without the idle animation.
        /// </summary>
        /// <param name="sb"> The SpriteBatch being used to draw the partner. </param>
        /// <param name="color"> The color being used to draw the partner. </param>
        public void DrawStill(SpriteBatch sb, Color color)
        {
            sb.Draw(spritesheet, Hitbox, firstFrame, color);
        }

        /// <summary>
        /// Draws the partner doing an idle animation.
        /// </summary>
        /// <param name="sb"> The SpriteBatch being used to draw the partner. </param>
        /// <param name="color"> The color being used to draw the partner. </param>
        public void DrawIdle(SpriteBatch sb, Color color)
        {
            sb.Draw(spritesheet, Hitbox, source, color);
        }
    }
}
