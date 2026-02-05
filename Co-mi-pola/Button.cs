using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Co_mi_pola
{
    // Joshua Smith
    // 02/03/2026
    //
    // The button class is pretty straightforward! It functions as a button, that the player can click on to activate a function
    // of some kind.
    internal class Button(Rectangle hitbox, Texture2D icon, Texture2D hovered, Texture2D held)
    {

        // Booleans to help track the button states.
        bool holding;
        bool hovering;

        /// <summary>
        /// Updates the button based on player input.
        /// </summary>
        /// <param name="mScaled"> The position of the mouse, scaled to match the game resolution. </param>
        /// <param name="ms"> The current state of the mouse. </param>
        /// <param name="pms"> The previous state of the mouse. </param>
        /// <returns> Whether or not the button was clicked this frame. </returns>
        public bool Update(Point mScaled, MouseState ms, MouseState pms)
        {
            hovering = hitbox.Contains(mScaled);
            holding = ms.LeftButton == ButtonState.Pressed;
            return hovering && !holding && pms.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Draw the button in any of its various draw states.
        /// </summary>
        /// <param name="sb"> The SpriteBatch being used to draw the button. </param>
        /// <param name="color"> The color being used to draw the button. </param>
        public void Draw(SpriteBatch sb, Color color)
        {
            // Check if player is hovering over button.
            if(hovering)
            {
                // Check if they're holding down on the button.
                if(holding)
                {
                    sb.Draw(held, hitbox, color);
                }
                else
                {
                    sb.Draw(hovered, hitbox, color);
                }
            }

            // Draw button like normal.
            else
            {
                sb.Draw(icon, hitbox, color);
            }
        }
    }
}
