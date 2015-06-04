// Particle.cs
// Introduced: 2015-04-28
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
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;
using FarseerPhysics;
using Synced.Interface;
using Synced.Actors;

namespace Synced.InGame.Actors
{
    class Particle : Sprite
    {
        #region Variables
        private float _timeSinceStart; // How long time the particle has existed.
        private float _lifetime; // The predefined length of the particle's existence.
        private bool _isMoving;
        private Vector2 _direction;
        private float _speed;
        private float _colorStrength;
        private float _procentualLifetime;
        #endregion

        #region Properties

        public bool IsDead
        {
            get { return _lifetime <= _timeSinceStart; }
        }
        #endregion

        #region Constructors
        public Particle(Texture2D texture, Vector2 position, Color color, Vector2 origin, float scale, float rotation, float lifetime, DrawingHelper.DrawingLevel drawingLevel, Game game)
            : base(texture, position, drawingLevel, game)
        {
            _lifetime = lifetime;
            _timeSinceStart = 0.0f;
            Color = color;
            Origin = origin;
            Scale = scale;
            Rotation = rotation;
            Alpha = 1.0f;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods

        public void Sleep() 
        {
            _timeSinceStart = 0.0f;
        }

        public void WakeTrailParticle(Vector2 position, Color color, float scale, float lifetime) 
        {
            Position = position;
            Color = color;
            Scale = scale;
            _lifetime = lifetime;
        }

        public void WakeRandomParticle(Vector2 position, Color color, float scale, float lifetime, float randomX, float randomY)
        {
            Position = new Vector2(position.X + randomX, position.Y + randomY);
            Color = color;
            Scale = scale;
            _lifetime = lifetime;
        }

        public void WakeLineParticle(Vector2 position, Color color, float scale, float lifetime)
        {
            Position = position;
            Color = color;
            Scale = scale;
            _lifetime = lifetime;
        }

        public void Update(float elapsedTime) 
        {
            _timeSinceStart += elapsedTime;
            _procentualLifetime = _timeSinceStart / _lifetime;
            _colorStrength = 1.0f - _procentualLifetime;

            if (_isMoving)
            {
                Position += (_direction * _speed);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, Position, null, (Color * _colorStrength * Alpha), Rotation, Origin, Scale, SpriteEffects.None, 1.0f); // TODO: use body pos/rot or Sprite pos/rot? 
            _spriteBatch.End();
        }

        public void SetMovement(Vector2 direction, float speed) 
        {
            _direction = direction;
            direction.Normalize();
            _speed = speed;
            _isMoving = true;
        }
        #endregion
    }
}
