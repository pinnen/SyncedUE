// Crystal.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-23
// Edited by:
// Pontus Magnusson
// Göran Forsström
// Dennis Stockhaus
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;


namespace Synced.InGame
{
    class Crystal : Grabbable
    {
        #region Variables
        // General Variables
        //MovableCollidable _owner = null;
        //float _distanceToOwner;
        #endregion

        public Crystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, Color color)
            : base(texture, position, drawingLevel, game, world, color)
        {
            ///* Setting up Farseer Physics */
            //RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position)); // TODO: size to some scale? 
            //RigidBody.BodyType = BodyType.Dynamic;
            //RigidBody.CollisionCategories = Category.Cat1; /* Crystal Category */ // TODO: fix collisionCategory system. 
            //RigidBody.CollidesWith = Category.All;
            //RigidBody.Mass = 1f; // TODO: fix hardcoded value
            //RigidBody.LinearDamping = 0.5f; // TODO: fix hardcoded value
            //RigidBody.Restitution = 1f; // TODO: fix hardcoded value
            //Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            RigidBody.CollidesWith = Category.All ^ Category.Cat9;

            /* Setting up Crystal */
            //_distanceToOwner = 50; // TODO: fix hardcoded distance
            Tag = "CRYSTAL";
        }

        //public override Grabbable PickUp(MovableCollidable owner)
        //{
        //    _owner = owner;
        //    Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalPickUp);
        //    return this;
        //}
        
        //public override void Release()
        //{
        //    _owner = null;
        //}

        //public override void Shoot()
        //{
        //    Release();
        //    Direction = -Direction;
        //    Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalShoot);
        //}

        //public override void Update(GameTime gameTime)
        //{
        //    float rotval = (float)0.002 * gameTime.ElapsedGameTime.Milliseconds; // TODO: Fix hardcode value
        //    RigidBody.Rotation += rotval;

        //    if (_owner != null) // TODO a better formula for a more consistent Crystal Position
        //    {
        //        if (_owner.Direction  != Vector2.Zero)
        //        {

        //            Position = new Vector2(_owner.Position.X - (_distanceToOwner * _owner.Direction.X),
        //                                   _owner.Position.Y - (_distanceToOwner * -_owner.Direction.Y));
        //        }
        //    }
            
        //    base.Update(gameTime);
        //}

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {    
            return true;
        }
    }
}
