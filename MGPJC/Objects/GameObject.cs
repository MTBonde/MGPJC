using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGPJC
{
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

        private float _scale;

        protected Texture2D _texture;

        public List<GameObject> Children { get; set; }

        #endregion Fields

        #region Properties

        public Rectangle Rectangle
        {
            get
            {
                if(_texture != null)
                {
                    return new Rectangle((int)Position.X - (int)Origin.X,
                                         (int)Position.Y - (int)Origin.Y,
                                         _texture.Width,
                                         _texture.Height);
                }
                throw new Exception("Unknown gameObject");
            }
        }

        public Rectangle CollisionBox => new Rectangle(Rectangle.X, Rectangle.Y, MathHelper.Max(Rectangle.Width, Rectangle.Height), MathHelper.Max(Rectangle.Width, Rectangle.Height));

        public Color Colour { get; set; }

        public bool IsRemoved { get; set; }

        public float Layer
        {
            get => _layer;
            set => _layer = value;
        }

        public Vector2 Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public float Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }

        public float Scale
        {
            get => _scale;
            set => _scale = value;
        }
        #endregion Properties

        public GameObject(Texture2D texture)
        {
            _texture = texture;

            Children = new List<GameObject>();

            // TODO: Fix origin not working with sprite class
            
            //Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            //Origin => _texture == null ? Vector2.Zero : new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;
        }

        #region Methods

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(_texture != null)
            {
                //spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, 1f, SpriteEffects.None, Layer);
                //spriteBatch.Draw(_texture, Position, null, Colour, _rotation, 1f, SpriteEffects.None, Layer);
                //spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin new Vector2(_texture.Width / 2, _texture.Height / 2) , 1f, SpriteEffects.None, Layer);
                spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Vector2.Zero, 1f, SpriteEffects.None, Layer);
            }            
        }

        // TODO: Fix intersect
        
        //public bool Intersects(GameObject gameObject)
        //{
        //    if(value.Left < Right && Left < value.Right && value.Top < Bottom)
        //    {
        //        return Top < value.Bottom;
        //    }

        //    return false;
        //}
         

        protected void Shoot(float speed, Vector2 offSet)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Position = this.Position + offSet;
            bullet.Colour = this.Colour;
            bullet.Layer = 0.1f;
            bullet.LifeSpan = 5f;
            bullet.Velocity = new Vector2(speed, 0f);
            bullet.Parent = this;

            Children.Add(bullet);
        }

        public object Clone()
        {
            var gameObject = this.MemberwiseClone() as GameObject;            

            return gameObject;
        }

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
