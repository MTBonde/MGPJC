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
        //protected Dictionary<string, Animation> _animations;

        //protected AnimationManager _animationManager;

        // player and enemy
        public int Health { get; set; }

        public Bullet Bullet { get; set; }

        public float Speed;

        // 
        protected float _layer;

        protected Vector2 _origin;

        protected Vector2 _position;

        protected float _rotation;

        protected float _scale;
        //protected float _layer { get; set; }

        //protected Vector2 _origin { get; set; }

        //protected Vector2 _position { get; set; }

        //protected float _rotation { get; set; }

        //protected float _scale { get; set; }

        protected Texture2D _texture;

        public List<GameObject> Children { get; set; }

        public Color Colour { get; set; }

        public bool IsRemoved { get; set; }

        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;

                //if(_animationManager != null)
                //    _animationManager.Layer = _layer;
            }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;

                //if(_animationManager != null)
                //    _animationManager.Origin = _origin;
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;

                //if(_animationManager != null)
                //    _animationManager.Position = _position;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                if(_texture != null)
                {
                    return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
                }

                //if(_animationManager != null)
                //{
                //    var animation = _animations.FirstOrDefault().Value;

                //    return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, animation.FrameWidth, animation.FrameHeight);
                //}

                throw new Exception("Unknown gameObject");
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;

                //if(_animationManager != null)
                //    _animationManager.Rotation = value;
            }
        }

        //public readonly Color[] TextureData;

        //public Matrix Transform
        //{
        //    get
        //    {
        //        return Matrix.CreateTranslation(new Vector3(-Origin, 0)) *
        //          Matrix.CreateRotationZ(_rotation) *
        //          Matrix.CreateTranslation(new Vector3(Position, 0));
        //    }
        //}

        public GameObject Parent;

        /// <summary>
        /// The area of the gameObject that could "potentially" be collided with
        /// </summary>
        public Rectangle CollisionArea
        {
            get
            {
                return new Rectangle(Rectangle.X, Rectangle.Y, MathHelper.Max(Rectangle.Width, Rectangle.Height), MathHelper.Max(Rectangle.Width, Rectangle.Height));
            }
        }

        public GameObject(Texture2D texture)
        {
            _texture = texture;

            Children = new List<GameObject>();

            //Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            //Origin => _texture == null ? Vector2.Zero : new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;

            //TextureData = new Color[_texture.Width * _texture.Height];
            //_texture.GetData(TextureData);
        }

        //public GameObject(Dictionary<string, Animation> animations)
        //{
        //    _texture = null;

        //    Children = new List<GameObject>();

        //    Colour = Color.White;

        //    TextureData = null;

        //    _animations = animations;

        //    var animation = _animations.FirstOrDefault().Value;

        //    _animationManager = new AnimationManager(animation);

        //    Origin = new Vector2(animation.FrameWidth / 2, animation.FrameHeight / 2);
        //}

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
            //else if(_animationManager != null)
            //    _animationManager.Draw(_spriteBatch);
        }

        //public bool Intersects(GameObject sprite)
        //{
        //    if(this.TextureData == null)
        //        return false;

        //    if(sprite.TextureData == null)
        //        return false;

        //    // Calculate a matrix which transforms from A's local space into
        //    // world space and then into B's local space
        //    var transformAToB = this.Transform * Matrix.Invert(sprite.Transform);

        //    // When a point moves in A's local space, it moves in B's local space with a
        //    // fixed direction and distance proportional to the movement in A.
        //    // This algorithm steps through A one pixel at a time along A's X and Y axes
        //    // Calculate the analogous steps in B:
        //    var stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
        //    var stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

        //    // Calculate the top left corner of A in B's local space
        //    // This variable will be reused to keep track of the start of each row
        //    var yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

        //    for(int yA = 0; yA < this.Rectangle.Height; yA++)
        //    {
        //        // Start at the beginning of the row
        //        var posInB = yPosInB;

        //        for(int xA = 0; xA < this.Rectangle.Width; xA++)
        //        {
        //            // Round to the nearest pixel
        //            var xB = (int)Math.Round(posInB.X);
        //            var yB = (int)Math.Round(posInB.Y);

        //            if(0 <= xB && xB < sprite.Rectangle.Width &&
        //                0 <= yB && yB < sprite.Rectangle.Height)
        //            {
        //                // Get the colors of the overlapping pixels
        //                var colourA = this.TextureData[xA + yA * this.Rectangle.Width];
        //                var colourB = sprite.TextureData[xB + yB * sprite.Rectangle.Width];

        //                // If both pixel are not completely transparent
        //                if(colourA.A != 0 && colourB.A != 0)
        //                {
        //                    return true;
        //                }
        //            }

        //            // Move to the next pixel in the row
        //            posInB += stepX;
        //        }

        //        // Move to the next row
        //        yPosInB += stepY;
        //    }

        //    // No intersection found
        //    return false;
        //}

        //public virtual void OnCollide(GameObject gameObject)
        //{

        //}

        protected void Shoot(float speed)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Position = this.Position;
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

            //if(_animations != null)
            //{
            //    gameObject._animations = this._animations.ToDictionary(c => c.Key, v => v.Value.Clone() as Animation);
            //    gameObject._animationManager = gameObject._animationManager.Clone() as AnimationManager;
            //}

            return gameObject;
        }
    }
}
