using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGPJC
{
    /// <summary>
    /// A parent class with a bunch of methods and fields that are shared between the player, pets and enemies
    /// </summary>
    public class GameObject : Component, ICloneable
    {
        #region Fields

        public GameObject Parent;

        // player and enemy

        public int Health { get; set; }

        public Bullet Bullet { get; set; }

        public float Speed;

        // 
        protected float _layer;

        protected Vector2 _origin;

        protected Vector2 _position;

        protected float _rotation;        

        protected Texture2D _texture;

        protected GameWorld gameWorld;

        public List<GameObject> Children { get; set; }

        #endregion Fields

        #region Properties

        public Rectangle Rectangle
        {
            get
            {
                if(_texture != null)
                {
                    return new Rectangle((int)Position.X + _texture.Width / 5,
                                         (int)Position.Y + _texture.Height / 5,
                                         _texture.Width - _texture.Width / 5,
                                         _texture.Height - _texture.Height / 5);
                }
                throw new Exception("Unknown gameObject");
            }
        }

        public Rectangle CollisionBox => new Rectangle(Rectangle.X, Rectangle.Y, MathHelper.Max(Rectangle.Width, Rectangle.Height), MathHelper.Max(Rectangle.Width, Rectangle.Height));

        public Color Colour { get; set; }

        public bool IsRemoved { get; set; }

        public float Layer { get; set; }        

        public Vector2 Origin { get; set; }      

        public Vector2 Position { get; set; }      

        #endregion Properties

        public GameObject(Texture2D texture, GameWorld gameWorld)
        {
            _texture = texture;

            Children = new List<GameObject>();

            // TODO: Fix origin not working with sprite class            
            //Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            //Origin => _texture == null ? Vector2.Zero : new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;

            this.gameWorld = gameWorld;
        }

        #region Methods

        public override void Update(GameTime gameTime)
        {

        }
        /// <summary>
        /// Draw the gameobject
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(_texture != null)
            {
                // TODO: origin with sprite class
                //spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, 1f, SpriteEffects.None, Layer);
                //spriteBatch.Draw(_texture, Position, null, Colour, _rotation, 1f, SpriteEffects.None, Layer);
                //spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin new Vector2(_texture.Width / 2, _texture.Height / 2) , 1f, SpriteEffects.None, Layer);
                spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Vector2.Zero, 1f, SpriteEffects.None, Layer);
            }            
        }
        /// <summary>
        /// When invoked shoot will instantiate a new bullet and clone it
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="yOffSet"></param>
        protected void Shoot(float speed, Vector2 yOffSet, string bulletType)
        {
            var bullet = Bullet.Clone() as Bullet;
            // TODO: redo bullet as new instatiation instead of clone
            //Bullet bullet = new Bullet();
            bullet.Position = this.Position + yOffSet;
            bullet.Colour = this.Colour;
            bullet.Layer = 0.1f;
            bullet.LifeSpan = 5f;
            bullet.Velocity = new Vector2(speed, 0f);
            bullet.Parent = this;
            switch (bulletType)
            {
                case ("Bullet"):
                    bullet._texture = Sprites.Bullet;
                    break;
                case ("Fireball"):
                    bullet._texture = Sprites.LizardFireball;
                    break;
                case ("Goo"):
                    bullet._texture = Sprites.MushroomGoo;
                    break;
                case ("Acorn"):
                    bullet._texture = Sprites.AcornBullet;
                    break;
            }

            Children.Add(bullet);
        }
        /// <summary>
        /// Cloning is fun, and stupid
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var gameObject = this.MemberwiseClone() as GameObject;            

            return gameObject;
        }
        /// <summary>
        /// check if a collision is happening
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsColliding(GameObject other)
        {
            if(this == other)
                return false;

            return CollisionBox.Intersects(other.CollisionBox);
        }

        public virtual void OnCollision(GameObject other)
        {
        }
        #endregion Methods
    }
}
