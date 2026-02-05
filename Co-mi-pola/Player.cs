using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Co_mi_pola
{
    // Joshua Smith
    // 02/03/2026
    //
    // The player class is the object that is controlled by the user to interface with the game. It will have the ability to run and
    // jump, but that's it. 
    internal class Player(Rectangle hitbox, Texture2D spritesheet)
    {
        private Rectangle hitbox = hitbox; // The hitbox of the player, and where they must be drawn.
        private Vector2 position = hitbox.Location.ToVector2(); // A more accurate way to track the player's position, since a rectangle cannot store decimal values.
        private readonly Texture2D spritesheet = spritesheet; // The spritesheet used to draw the player.
        private float upVelocity = 0.0f; // The player's current upward velocity.
        private readonly float gravity = 0.1f; // Needed to allow for jumping.
        private bool facingLeft = false; // Whether or not the player is facing left. Used for drawing the sprite.
        private bool grounded = true; // Used to determine if the player is currently able to jump.

        // All of the information needed to animate the player.
        private readonly Rectangle jumpFrame = new(27, 0, 9, 13);
        private Rectangle source = new(0, 0, 9, 13);
        private readonly Vector2 origin = new(0, 0);
        private int curFrame = 0;
        private readonly int totalFrames = 2;
        private double frameTimer = 0.0;
        private readonly double framesPerSecond = 0.05;

        /// <summary>
        /// Updates the player, handling their controls and animation.
        /// </summary>
        /// <param name="gameTime"> The current time elapsed in the game. </param>
        /// <param name="kb"> The current state of the keyboard. </param>
        /// <param name="pkb"> The previous state of the keyboard. </param>
        public void Update(GameTime gameTime, KeyboardState kb, KeyboardState pkb)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds; // Increment frame timer.

            // Increment frame counter, when necessary.
            if(frameTimer >= framesPerSecond)
            {
                curFrame++;
                frameTimer = 0.0; // Reset frame timer.

                // Keep current frame within bounds.
                if(curFrame > totalFrames)
                {
                    curFrame = 0;
                }

                source.X = 9 * curFrame; // Move source rectangle along spritesheet.
            }

            // Handles player input for walking left and right.
            if(kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left))
            {
                hitbox.X -= 1;
                facingLeft = true;

                // Prevent player from walking off the left side of the screen.
                if(hitbox.X < 0)
                {
                    hitbox.X = 0;
                }
            }
            else if(kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right))
            {
                hitbox.X += 1;
                facingLeft = false;

                // Prevents the player from walking off the right side of the screen.
                if(hitbox.X > 119)
                {
                    hitbox.X = 119;
                }
            }
            else
            {
                source.X = 0;
            }

            // Allows the player to jump.
            if(grounded && (SingleKeyPress(kb, pkb, Keys.Space) || SingleKeyPress(kb, pkb, Keys.Up) || SingleKeyPress(kb, pkb, Keys.W)))
            {
                upVelocity = 2f;
                grounded = false;
            }

            // Handles all of the gravity and jumping logic.
            position.Y -= upVelocity;
            hitbox.Y = (int)position.Y;
            if(upVelocity <= -0.5f)
            {
                // Prevents the player from jumping while falling from a platform.
                grounded = false;
            }
            upVelocity -= gravity;
        }

        /// <summary>
        /// Moves the player to a specified location.
        /// </summary>
        /// <param name="x"> The X coordinate of where the player is being moved. </param>
        /// <param name="y"> The Y coordinate of where the player is being moved. </param>
        public void MoveAbsolute(int x, int y)
        {
            hitbox.X = x;
            hitbox.Y = y;
        }

        /// <summary>
        /// Handles things that collide with the player, except for their partner.
        /// </summary>
        /// <param name="other"> The hitbox we're handling collisions with. </param>
        public void HandleCollisions(Rectangle other)
        {
            // Check if there is a collision.
            if(hitbox.Intersects(other))
            {
                // Grab the overlapping rectangle between the two hitboxes.
                Rectangle overlap = Rectangle.Intersect(hitbox, other);

                // Handle X collisions.
                if(overlap.Height >= overlap.Width)
                {
                    if(overlap.X <= hitbox.X)
                    {
                        hitbox.X += overlap.Width;
                    }
                    else
                    {
                        hitbox.X -= overlap.Width;
                    }
                }

                // Handle Y collisions.
                else
                {
                    upVelocity = 0;

                    if(overlap.Y > hitbox.Y)
                    {
                        hitbox.Y -= overlap.Height;
                        grounded = true;
                    }
                    else
                    {
                        hitbox.Y += overlap.Height;
                    }

                    position.Y = hitbox.Y;
                }
            }
        }

        /// <summary>
        /// Draws the player to the screen.
        /// </summary>
        /// <param name="sb"> The SpriteBatch being used to draw the player. </param>
        /// <param name="color"> The color being used to draw the player. </param>
        public void Draw(SpriteBatch sb, Color color)
        {
            // Draw the player walking, or idle.
            if(grounded)
            {
                if(facingLeft)
                {
                    sb.Draw(spritesheet, hitbox, source, color, 0.0f, origin, SpriteEffects.FlipHorizontally, 0.0f);
                }
                else
                {
                    sb.Draw(spritesheet, hitbox, source, color);
                }
            }

            // Draw the player in midair.
            else
            {
                if(facingLeft)
                {
                    sb.Draw(spritesheet, hitbox, jumpFrame, color, 0.0f, origin, SpriteEffects.FlipHorizontally, 0.0f);
                }
                else
                {
                    sb.Draw(spritesheet, hitbox, jumpFrame, color);
                }
            }
        }

        /// <summary>
        /// Checks to see if a key was pressed one time.
        /// </summary>
        /// <param name="kb"> The current state of the keyboard. </param>
        /// <param name="pkb"> The previous state of the keyboard. </param>
        /// <param name="key"> The key we're checking. </param>
        /// <returns> Whether or not that key was pressed one time. </returns>
        public static bool SingleKeyPress(KeyboardState kb, KeyboardState pkb, Keys key)
        {
            return kb.IsKeyDown(key) && pkb.IsKeyUp(key);
        }
    }
}
