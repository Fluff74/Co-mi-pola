using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Co_mi_pola
{
    // Joshua Smith
    // 02/03/2026
    //
    // A block is just a rectangle that can be stepped on or walked into. This can be a platform, a ledge, or even just the ground.
    internal class Block(Rectangle hitbox, Texture2D texture)
    {
        /// <summary>
        /// The hitbox of the Block object.
        /// </summary>
        public Rectangle Hitbox { get; set; } = hitbox;

        /// <summary>
        /// Draws the block to the screen.
        /// </summary>
        /// <param name="sb"> The SpriteBatch being used to draw the block. </param>
        /// <param name="color"> The color of the block. </param>
        public void Draw(SpriteBatch sb, Color color)
        {
            sb.Draw(texture, Hitbox, color);
        }
    }
}
