// Button.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
//
// 
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Interface
{
    class Button : Controls

    {
        string _texturePath;

        public Button(Game game,Vector2 position, string texturePath) : base(game, texturePath)
        {

            Position = position;

            // Construct any child components here
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null || GetType() != obj.GetType())
        //        return false;

        //    return base.Equals(obj);
        //}
    }
}
