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
    class Barrier : DrawableGameComponent
    {
        List<Body> _barrierBodies;
        Texture2D _barrierTexture;
        Unit _start, _end;
        SpriteBatch _spriteBatch;

        public Barrier(Texture2D texture, Unit start, Unit end,World world,Game game) 
            : base(game)
        {
            _start = start;
            _end = end;

            Path barrierPath = new Path();
            barrierPath.Add(start.Position);
            barrierPath.Add(end.Position);
            barrierPath.Closed = false;

            Vertices box = PolygonTools.CreateRectangle(0.125f,0.5f);
            PolygonShape shape = new PolygonShape(box,20);

            _barrierBodies = PathManager.EvenlyDistributeShapesAlongPath(world, barrierPath, shape, BodyType.Dynamic, 29);
            _barrierTexture = texture;

            JointFactory.CreateRevoluteJoint(world, start.RigidBody, _barrierBodies[0], Vector2.Zero);
            JointFactory.CreateRevoluteJoint(world,end.RigidBody,_barrierBodies[_barrierBodies.Count-1],Vector2.Zero);

            PathManager.AttachBodiesWithRevoluteJoint(world, _barrierBodies, Vector2.Zero, Vector2.Zero, false, false);

            _spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            for (int i = 0; i < _barrierBodies.Count; i++)
            {
                _barrierBodies[i].CollidesWith = Category.None;
                _barrierBodies[i].UserData = "";
            }

        }

        public override void Draw(GameTime gameTime)
        {
            
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());

            for (int i = 0; i < _barrierBodies.Count; i++)
            {
                _spriteBatch.Draw(_barrierTexture, ConvertUnits.ToDisplayUnits(_barrierBodies[i].Position), null, Color.Magenta, _barrierBodies[i].Rotation, new Vector2(_barrierTexture.Width / 2, _barrierTexture.Height / 2), 1.0f, SpriteEffects.None, 1.0f);
            }
            //_spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, 1.0f, SpriteEffects.None, 1.0f);



            _spriteBatch.End();
        }

    }
}
