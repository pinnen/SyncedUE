// ParticleEngine.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-18
// Edited by:
// Lina Juuso
//
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Synced.InGame.Actors
{
    class ParticleEngine : DrawableGameComponent
    {
        #region Variables
        List<Particle> _particles;
        Queue<Particle> _sleepingParticles;
        int _particleAmount; // Number of particles to generate each update
        Random random;
        //Data for particles:
        Vector2 _particlePosition;
        string _particleTextureName;
        Color _particleColor;
        Vector2 _particleOrigin;
        float _particleScale;
        float _particleRotation;
        float _particleLifetime;
        #endregion

        #region Constructors
        public ParticleEngine(Game game, int particleAmount, string textureName, Vector2 position, Color color, Vector2 origin, float scale, float rotation, float lifetime) 
            : base(game) 
        {
            _particles = new List<Particle>();
            _sleepingParticles = new Queue<Particle>();
            _particleAmount = particleAmount;
            _particlePosition = position;
            _particleTextureName = textureName;
            _particleColor = color;
            _particleOrigin = origin;
            _particleScale = scale;
            _particleRotation = rotation;
            _particleLifetime = lifetime;
            game.Components.Add(this);
        }
        #endregion

        #region Public Methods

        #endregion
    }
}

