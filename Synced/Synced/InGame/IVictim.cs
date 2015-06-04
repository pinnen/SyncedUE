using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.InGame.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    interface IVictim
    {
        float CircleEffectTimer { get; set; }
        float TriangleEffectTimer { get; set; }
        float HexagonEffectTimer { get; set; }
        float PentagonEffectTimer { get; set; }
        Texture2D VictimTexture { get; }
        Color Color { get; }
        //float ParticleLifetime { get; }
        float LocalTimeScale { get; set; }
        Vector2 Position { get; set; }
        float Rotation { get; set;}
        Vector2 Direction { get; set; }
        bool FadeOut { get; set; }
        float InvisibilityAlpha { get; set; }
        //ParticleEngine TrailEngine { get; }
        Vector2 VictimLinearVelocity { get; }

    }
}
