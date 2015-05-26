// TexturePolygon.cs
// Introduced: 2015-05-23
// Last edited: 2015-05-23
// Edited by:
// Dennis Stockhaus
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.InGame.Actors;
using Synced.Static_Classes;
using System.Collections.Generic;

namespace Synced.CollisionShapes
{
    class TexturePolygon : CollidingSprite
    {
        public TexturePolygon(Texture2D texture, Vector2 position, float rotation, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, bool textureCenter)
            : base(texture, position, drawingLevel, game, world)
        {
            // Fetch Texure data
            uint[] data = new uint[texture.Width * texture.Height];
            texture.GetData(data);
            Vertices textureVertices = PolygonTools.CreatePolygon(data, texture.Width, true);

            // Set Polygon Centroid    
            if (textureCenter) // Texture based center
            {
                Vector2 centroid = -textureVertices.GetCentroid();
                textureVertices.Translate(ref centroid);
                Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            }
            else // Vertice based center
            {
                Vector2 centroid = -textureVertices.GetCentroid();
                textureVertices.Translate(ref centroid);
                Origin = new Vector2(-centroid.X, -centroid.Y);
            }

            // Simplify Polygon for performance
            textureVertices = SimplifyTools.ReduceByDistance(textureVertices, 4f);
            List<Vertices> list = Triangulate.ConvexPartition(textureVertices, TriangulationAlgorithm.Bayazit);

            // Convert polygon to sim units
            Vector2 vertScale = new Vector2(ConvertUnits.ToSimUnits(1));
            foreach (Vertices vertice in list)
            {
                vertice.Scale(ref vertScale);
            }

            // create body compound. 
            RigidBody = BodyFactory.CreateCompoundPolygon(world, list, 1f, BodyType.Static);
            RigidBody.BodyType = BodyType.Static;
            RigidBody.Position = ConvertUnits.ToSimUnits(position);
            RigidBody.Rotation = rotation;
        }

        public void setOnCollisionFunction(OnCollisionEventHandler onCollisionFunc)
        {
            RigidBody.OnCollision += onCollisionFunc;
        }

        public void SetCollisionCategory(Category collisionCategory)
        {
            RigidBody.CollisionCategories = collisionCategory;
        }

        public void SetCollideWithCategory(Category collideWithCategory)
        {
            RigidBody.CollidesWith = collideWithCategory;
        }
    }


}
