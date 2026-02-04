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
    internal class Player
    {
        private Rectangle hitbox; // The hitbox of the player, and where they must be drawn.
        private Vector2 position; // A more accurate way to track the player's position, since a rectangle cannot store decimal values.
        private Texture2D walkingSpritesheet;
        private readonly float gravity; // Needed to allow for jumping.
        private bool facingLeft; // Whether or not the player is facing left. Used for drawing the sprite.
        private bool walking; // Used to reset the animation when the player stops walking.
        private bool grounded; // Used to determine if the player is currently able to jump.

        // All of the information needed to animate the player.
        private readonly Rectangle firstFrame = new(0, 0, 10, 13);
        private Rectangle source = new(0, 0, 10, 13);
        private int curFrame = 0;
        // private readonly int totalFrames = 1; Update once spritesheet is completed.
        private double frameTimer = 0.0;
        private readonly double framesPerSecond = 1.0;

        public Player(Rectangle hitbox, Texture2D walkingSpritesheet)
        {

        }

        public void Update(GameTime gameTime, KeyboardState kb, KeyboardState pkb)
        {

        }

        public void MoveAbsolute(int x, int y)
        {

        }

        public void HandleCollisions(Rectangle other)
        {

        }

        public void Draw(SpriteBatch sb, Color color)
        {

        }
    }
}
