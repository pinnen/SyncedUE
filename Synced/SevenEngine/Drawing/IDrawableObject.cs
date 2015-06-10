using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SevenEngine.Drawing
{
    public interface IDrawableObject
    {
        Vector2 Position { get; }
        Texture2D Texture { get; }
    }
}
