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
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;

namespace Synced.InGame.Actors
{
    class ParticleEngine : DrawableGameComponent
    {
        #region Variables
        List<Particle> _particles;
        Queue<Particle> _sleepingParticles;
        int _particleAmount; // Number of particles to generate each update
        Random random;
        Texture2D _particleTexture;
        bool isOneShot;
        float OneShotLifeTime;

        //Data for particles:
        Vector2 _particlePosition;
        Color _particleColor;

        public Color ParticleColor
        {
            get { return _particleColor; }
            set { _particleColor = value; }
        }
        Vector2 _particleOrigin;
        float _particleScale;
        float _particleRotation;
        float _particleLifetime;
        public float ParticleLifetime
        {
            get { return _particleLifetime; }
        }
        

        Particle currentParticle;
        DrawingHelper.DrawingLevel dLevel;
        #endregion

        #region Constructors
        public ParticleEngine(int particleAmount, Texture2D particleTexture, Vector2 position, Color color, Vector2 origin, float scale, float rotation, float lifetime, DrawingHelper.DrawingLevel drawingLevel, Game game)
            : base(game)
        {
            Initialize();
            _particles = new List<Particle>();
            _sleepingParticles = new Queue<Particle>();
            _particleAmount = particleAmount;
            _particlePosition = position;
            _particleColor = color;
            _particleOrigin = origin;
            _particleScale = scale;
            _particleRotation = rotation;
            _particleLifetime = lifetime;
            _particleTexture = particleTexture;
            _particleOrigin.X = _particleTexture.Width / 2;
            _particleOrigin.Y = _particleTexture.Height / 2;
            dLevel = drawingLevel;
            isOneShot = false;
        }
        #endregion

        #region Public Methods
        public override void Initialize()
        {
            base.Initialize();
            random = new Random();
        }

        public void UpdatePosition(Vector2 newPosition) 
        {
            _particlePosition = newPosition;
        }


        /// <summary>
        /// Starts the particle engine for trails.
        /// </summary>
        /// 
        public void GenerateTrailParticles() 
        {
            GenerateTrailParticles(_particleScale, _particleLifetime);
        }
        public void GenerateTrailParticles(float lifetime)
        {
            GenerateTrailParticles(_particleScale, lifetime);
        }
        public void GenerateTrailParticles(float scale, float lifetime) 
        {

            _particleRotation = MathHelper.ToRadians(random.Next(0, 360));

            Particle tempP;
            for (int i = 0; i < _particleAmount; i++)
            {
                if (_sleepingParticles.Count == 0)
                {
                    tempP = new Particle(_particleTexture, _particlePosition, _particleColor, _particleOrigin, scale, _particleRotation, lifetime,dLevel,Game);
                    _particles.Add(tempP);
                    SyncedGameCollection.ComponentCollection.Add(tempP);
                    continue;
                }
                currentParticle = _sleepingParticles.Dequeue();
                currentParticle.WakeTrailParticle(_particlePosition, _particleColor, scale, lifetime);
                _particles.Add(currentParticle);
            }
        }
        /// <summary>
        /// Starts the particle engine for effects.
        /// </summary>
        /// 
        public void GenerateEffectParticles() 
        {
            GenerateEffectParticles(_particleScale, _particleLifetime);
        }
        public void GenerateEffectParticles(float scale, float lifetime) 
        {
            
            int effectSize = 80; // TODO: ta in som parameter?

            for (int i = 0; i < _particleAmount; i++)
            {
                if (_sleepingParticles.Count == 0)
                {
                    Vector2 randomPosition = new Vector2(_particlePosition.X + (float)random.Next(-effectSize,effectSize),_particlePosition.Y + (float)random.Next(-effectSize*50,effectSize*50));
                    Particle tempP = new Particle(_particleTexture,randomPosition,_particleColor,_particleOrigin,scale,0.0f,lifetime,dLevel,Game);
                    _particles.Add(tempP);
                    SyncedGameCollection.ComponentCollection.Add(tempP);
                    continue;
                }
                currentParticle = _sleepingParticles.Dequeue();
                currentParticle.WakeRandomParticle(_particlePosition, _particleColor, scale, lifetime, (float)random.Next(-effectSize, effectSize), (float)random.Next(-effectSize, effectSize));
                _particles.Add(currentParticle);
            }
        }

        public void GenerateLineParticles(float scale, float lifetime, Vector2 startPosition, Vector2 endPosition) 
        {
            // Get direction between positions.
            Vector2 tmpDirection = Vector2.Zero;
            tmpDirection.X = endPosition.X - startPosition.X;
            tmpDirection.Y = endPosition.Y - startPosition.Y;
            tmpDirection.Normalize();

            // Get distance between the positions.
            float s_x = (endPosition.X - startPosition.X) * (endPosition.X - startPosition.X);
            float s_y = (endPosition.Y - startPosition.Y) * (endPosition.Y - startPosition.Y);
            float distance = (float)Math.Sqrt(s_x + s_y);

            // Get distance between particles.
            float particleDistance = distance / _particleAmount;

            //Split the distance between the particles that should be drawn.
            for (int i = 0; i <= _particleAmount; i++)
            {
                Vector2 tmpPosition = startPosition + (tmpDirection * particleDistance * i);
                

                if (_sleepingParticles.Count == 0)
                {
                    Particle tempP = new Particle(_particleTexture, tmpPosition, _particleColor, _particleOrigin, scale, 0.0f, lifetime,dLevel, Game);
                    _particles.Add(tempP);
                    SyncedGameCollection.ComponentCollection.Add(tempP);
                }
                else
                {
                    currentParticle = _sleepingParticles.Dequeue();
                    currentParticle.WakeLineParticle(tmpPosition,_particleColor,scale,lifetime);
                    _particles.Add(currentParticle);
                }
            }
			}

        public void GenerateClusterParticles()
        {
            int clusterParticleAmount = 50;

            for (int i = 0; i < clusterParticleAmount; i++)
            {
                Vector2 randomPosition = _particlePosition;
                float randomRotation = random.Next();
                Particle tempP = new Particle(_particleTexture, randomPosition, _particleColor, _particleOrigin, _particleScale, randomRotation, _particleLifetime, dLevel, Game);
                _particles.Add(tempP);
                SyncedGameCollection.ComponentCollection.Add(tempP);
            }

        }

        public void GenerateDynamicParticles(List<Vector2> positions, float scale, float lifetime)
        {
            _particleAmount = positions.Count;

            for (int i = 0; i < _particleAmount; i++)
            {
                if (_sleepingParticles.Count == 0)
                {
                    Particle tempP = new Particle(_particleTexture, positions[i], _particleColor, _particleOrigin, scale, 0.0f, lifetime, dLevel, Game);
                    _particles.Add(tempP);
                    SyncedGameCollection.ComponentCollection.Add(tempP);
                }
                else
                {
                    currentParticle = _sleepingParticles.Dequeue();
                    currentParticle.WakeLineParticle(positions[i], _particleColor, scale, lifetime);
                    _particles.Add(currentParticle);
                }
            }
        }

        /// <summary>
        /// Shatters the particles
        /// </summary>
        /// 
        public void ShatterParticles() 
        {
            ShatterParticles(100, 20);
        }
        public void ShatterParticles(int shatterDirection,int shatterSpeed) 
        {
            Vector2 direction;
            float speed;
            foreach (Particle p in _particles)
            {
                direction = new Vector2(random.Next(-shatterDirection, shatterDirection), random.Next(-shatterDirection, shatterDirection));
                direction.Normalize();
                speed = random.Next(-shatterSpeed, shatterSpeed);
                p.SetMovement(direction, speed);
            }
        }
        public void ExpandAndRotate()
        {
            ExpandAndRotate(5.0f, 5.0f);
        }
        public void ExpandAndRotate(float rotation, float expansion)
        {
            foreach (Particle p in _particles)
            {
                p.Rotation += rotation;
                p.Scale += expansion;
            }
        }
        public void AddOffset(Vector2 positionOffset) 
        {
            foreach (Particle p in _particles)
            {
                p.Position += positionOffset;
            }
        }
        public void SetParticleFadeAlpha(float alpha) 
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Alpha = alpha;
            }
            foreach (Particle p in _sleepingParticles)
            {
                p.Alpha = alpha;
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds; //TODO: gångra med timescalevariabel? Se gamla koden.
            
            for (int i = 0; i < _particles.Count; i++)
            {
                if (!_particles[i].IsDead) // Update particle if it's alive.
                {
                    _particles[i].Update(elapsedTime);
                }
                else
                {
                    _particles[i].Sleep();
                    _sleepingParticles.Enqueue(_particles[i]); // Add particle to sleeping queue
                    _particles.RemoveAt(i);
                }
            }

            if (isOneShot)
            {
                OneShotLifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (OneShotLifeTime <= 0)
                {
                    Delete();
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //for (int i = 0; i < _particles.Count; i++)
            //{
            //    _particles[i].Draw(gameTime);
            //}           
        }

        public void SetParticleEngineOneShot(float _timeSeconds)
        {
            OneShotLifeTime = _timeSeconds;
            isOneShot = true;
        }
        private void Delete()
        {
            // delete all particles
            while (_particles.Count > 0)
            {
                SyncedGameCollection.ComponentCollection.Remove(_particles[0]);
                _particles.RemoveAt(0);
            }
            foreach (Particle p in _sleepingParticles)
            {
                SyncedGameCollection.ComponentCollection.Remove(p);
            }
            _particles.Clear();
            _sleepingParticles.Clear();
            SyncedGameCollection.ComponentCollection.Remove(this);
        }
        public void DeleteParticleEngine()
        {
            // delete all particles
            while (_particles.Count > 0)
            {
                SyncedGameCollection.ComponentCollection.Remove(_particles[0]);
                _particles.RemoveAt(0);
            }
            foreach (Particle p in _sleepingParticles)
            {
                SyncedGameCollection.ComponentCollection.Remove(p);
            }
            _particles.Clear();
            _sleepingParticles.Clear();
        }

        #endregion
    }
}

