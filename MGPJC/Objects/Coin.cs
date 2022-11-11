using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGPJC
{
    public class Coin
    {
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Texture2D _texture;


        public float Speed = 1f;

        public Vector2 Velocity;

        protected Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if(_animationManager != null)
                {
                    _animationManager.Position = _position;
                }
            }
        }

        // overlaod CTor

        //static
        public Coin(Texture2D texture)  //rename to object/entety??
        {
            _texture = texture;
        }
        //Dynamic
        public Coin(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value); // .first = set to first animation 
        }

        public void Update(GameTime gameTime, List<Coin> coins)
        {
            SetAnimations();

            _animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if(_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("SHITS FUCKED!!");
        }

        protected void SetAnimations()
        {
            _animationManager.Play(_animations["Coins"]);  //call for spritesheet name

          
        }
    }
}
