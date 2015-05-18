// Particle.cs
// Introduced: 2015-04-28
// Last edited: 2015-04-28
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

namespace Synced.InGame.Actors
{
    class Particle
    {
        #region Variables
        private float _timeSinceStart; // How long time the particle has existed.
        private float _lifetime; // The predefined length of the particle's existence.
        private Texture2D _texture;
        private Vector2 _position;
        private Color _color;
        private Vector2 _origin;
        private float _scale;
        private float _rotation;
        private bool _isMoving;
        private Vector2 _direction;
        private float _speed;
        private float _colorStrength;
        private float _fadeAlpha;
        private float _procentualLifetime;
        #endregion

        #region Properties
        //public float LifeTime 
        //{
        //    get { return _lifetime; }
        //    set { _lifetime = value; }
        //}
        //public Vector2 Position
        //{
        //    get { return _position; }
        //    set { _position = value; }
        //}
        //public bool IsMoving
        //{
        //    get { return _isMoving; }
        //    set { _isMoving = value; }
        //}
        //public bool IsDead
        //{
        //    get { return _lifetime <= _timeSinceStart; }
        //}
        //public float FadeAlpha
        //{
        //    get { return _fadeAlpha; }
        //    set { _fadeAlpha = value; }
        //}
        #endregion

        #region Constructors
        public Particle(Texture2D texture, Vector2 position, Color color, Vector2 origin, float scale, float rotation, float lifetime) 
        {
            _lifetime = lifetime;
            _timeSinceStart = 0.0f;
            _texture = texture;
            _position = position;
            _color = color;
            _origin = origin;
            _scale = scale;
            _rotation = rotation;
            _fadeAlpha = 1.0f;
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
            _position = position;
            _color = color;
            _scale = scale;
            _lifetime = lifetime;
        }

        public void WakeRandomParticle(Vector2 position, Color color, float scale, float lifetime, float randomX, float randomY)
        {
            _position = new Vector2(position.X + randomX, position.Y + randomY);
            _color = color;
            _scale = scale;
            _lifetime = lifetime;
        }

        public void Update(float elapsedTime) 
        {
            _timeSinceStart += elapsedTime;
            _procentualLifetime = _timeSinceStart / _lifetime;
            _colorStrength = 1.0f - _procentualLifetime;
            _scale = 1.0f - (-_procentualLifetime / 2.0f);

            if (_isMoving)
            {
                _position += (_direction * _speed);
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(_texture, _position, null, (_color * _colorStrength * _fadeAlpha), _rotation, _origin, _scale, SpriteEffects.None, 1.0f);
            spriteBatch.End();
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
