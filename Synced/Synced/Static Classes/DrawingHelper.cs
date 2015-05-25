// Drawinghelper.cs
// Introduced: 2015-04-26
// Last edited: 2015-04-26
// Edited by:
// Pontus Magnusson
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Synced.Static_Classes
{
    static class DrawingHelper
    {
        public enum DrawingLevel { Back = 1, Low = 2, Medium = 3 , High = 4, Top = 5 }
        
        [Flags]
        public enum Alignment { Center = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 }

        public static void DrawString(SpriteBatch spritebatch, SpriteFont font, string text, Rectangle bounds, Alignment align, float scale, Color color)
        {
            Vector2 size = font.MeasureString(text);
            Vector2 pos = new Vector2(bounds.Center.X, bounds.Center.Y);
            Vector2 origin = size * 0.5f;

            if (align.HasFlag(Alignment.Left))
                origin.X += bounds.Width / 2 - size.X / 2;

            else if (align.HasFlag(Alignment.Right))
                origin.X -= bounds.Width / 2 - size.X / 2;

            else if (align.HasFlag(Alignment.Top))
                origin.Y += bounds.Height / 2 - size.Y / 2;

            else if (align.HasFlag(Alignment.Bottom))
                origin.Y -= bounds.Height / 2 - size.Y / 2;

            spritebatch.DrawString(font, text, pos, color, 0, origin, scale, SpriteEffects.None, 0);
        }
    }
}
