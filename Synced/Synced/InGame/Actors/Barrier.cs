using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors
{
    class Barrier : CollidingSprite
    {
        #region Variables
        List<Body> _barrierBodies;
        Unit _start, _end;
        Body hiddenBody1;
        Body hiddenBody2;
        bool isActive;
        ParticleEngine EffectParticles;
        #endregion

        #region Properties
        #endregion

        public Barrier(Texture2D texture, Unit start, Unit end, World world,Game game, Color color) 
            : base(texture, Vector2.Zero, DrawingHelper.DrawingLevel.High, game, world)
        {
            Color = color;
            EffectParticles = new ParticleEngine(30, texture, Vector2.Zero, color, Vector2.Zero, 1, 0, 10, DrawingHelper.DrawingLevel.Top, game);
            SyncedGameCollection.ComponentCollection.Add(EffectParticles);

            // Units to follow
            _start = start;
            _end = end;

            // create two static hiddenBodies that mirror the position of the units
            hiddenBody1 = BodyFactory.CreateCircle(world, texture.Width / 2, 1, _start.RigidBody.Position);//_start.RigidBody;
            hiddenBody2 = BodyFactory.CreateCircle(world, texture.Width / 2, 1, _end.RigidBody.Position);//_end.RigidBody;
            hiddenBody1.CollisionCategories = Category.None;
            hiddenBody2.CollisionCategories = Category.None;
            hiddenBody1.CollidesWith = Category.None;
            hiddenBody2.CollidesWith = Category.None;
            hiddenBody1.BodyType = BodyType.Static;
            hiddenBody2.BodyType = BodyType.Static;
            hiddenBody1.Position = _start.RigidBody.Position;
            hiddenBody2.Position = _end.RigidBody.Position;
            hiddenBody1.UserData = hiddenBody2.UserData = "";

            // create path object and set it to the init position. 
            Path barrierPath = new Path();
            barrierPath.Add(start.RigidBody.Position);
            barrierPath.Add(end.RigidBody.Position);
            barrierPath.Closed = false;

            // create barrier particle
            Vertices barrierParticle = PolygonTools.CreateCircle(ConvertUnits.ToSimUnits(texture.Width * 4), 8);
            Shape shape = new PolygonShape(barrierParticle, 0f);

            // distribute barrierParticle positions along the path between the two units. 
            _barrierBodies = PathManager.EvenlyDistributeShapesAlongPath(world, barrierPath, shape, BodyType.Dynamic, 30);

            // fix the shapes together with the end and start point
            JointFactory.CreateRevoluteJoint(world, hiddenBody1, _barrierBodies[0], new Vector2(0f, 0.01f));
            JointFactory.CreateRevoluteJoint(world, hiddenBody2, _barrierBodies[_barrierBodies.Count - 1], new Vector2(0f, 0f));

            // fix all the barrierParticles together in the path
            PathManager.AttachBodiesWithRevoluteJoint(world, _barrierBodies, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), false, false);

            // Set up OnCollision fale safe. 
            for (int i = 0; i < _barrierBodies.Count; i++)
            {
                _barrierBodies[i].CollisionCategories = Category.Cat10;
                _barrierBodies[i].CollidesWith = Category.Cat5; /* should only collide with grabbables*/
                _barrierBodies[i].UserData = "BARRIER";
                _barrierBodies[i].OnCollision += OnCollision;
            }
            //Deactivate();
        }

        public override bool OnCollision(Fixture f1, Fixture f2, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            CollidingSprite other = SyncedGameCollection.GetCollisionComponent(f2);

            if (other != null)
            {
                if (other.Tag == TagCategories.CRYSTAL)
                {
                    return true;
                }
            }
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                // update hiddenbody position to match units. 
                hiddenBody1.Position = _start.RigidBody.Position;
                hiddenBody2.Position = _end.RigidBody.Position;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (isActive)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());

                for (int i = 0; i < _barrierBodies.Count; i++)
                {
                    _spriteBatch.Draw(Texture, ConvertUnits.ToDisplayUnits(_barrierBodies[i].Position), null, Color, _barrierBodies[i].Rotation, Origin, 1.0f, SpriteEffects.None, 1f);
                }

                _spriteBatch.End();
            }
        }

        public void Activate()
        {
            isActive = true;
            hiddenBody1.Position = _start.RigidBody.Position;
            hiddenBody2.Position = _end.RigidBody.Position;
            for (int i = 0; i < _barrierBodies.Count; i++)
            {
                _barrierBodies[i].Enabled = true;
                _barrierBodies[i].Position = hiddenBody1.Position;
            }
            // TODO: creating animation? sound?
        }

        public void Deactivate()
        {
            isActive = false;
            List<Vector2> positions = new List<Vector2>();
            for (int i = 0; i < _barrierBodies.Count; i++)
            {
                _barrierBodies[i].Enabled = false;
                positions.Add(ConvertUnits.ToDisplayUnits(_barrierBodies[i].Position));
            }
            // TODO: destroy animation? sound?
            EffectParticles.GenerateDynamicParticles(positions, 1, 10);
            EffectParticles.ShatterParticles();
        }

    }
}
