using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGPJC
{
    public class Player : GameObject
    {
        private KeyboardState _currentKey;

        private KeyboardState _previousKey;

        private float _shootTimer = 0;

        private float _reloadSpeed = 0;
        private int _ammoCount = 5;
        private float _hasMoved = 0;
        private int _currentlane;
        public bool IsDead => Health <= 0;

        public Input Input { get; set; }

        //public Score Score { get; set; }

        private static Player instance;
       
        public Player(Texture2D texture)
          : base(texture)
        {
            Speed = 5f;
        }

        

        public override void Update(GameTime gameTime)
        {
            if(IsDead)
                return;

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            var velocity = Vector2.Zero;
            _rotation = 0;

            _hasMoved += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_currentKey.IsKeyDown(Input.Up) && _hasMoved >= 0.2f)
            {
                if(Position.Y >= LaneManager.LaneArray[1])
                {
                velocity.Y -= LaneManager.LaneArray[0] / 2;
                _hasMoved = 0;
                }
            }
            else if(_currentKey.IsKeyDown(Input.Down) && _hasMoved >= 0.2f)
            {
                if (Position.Y <= LaneManager.LaneArray[1])
                {
                    velocity.Y += LaneManager.LaneArray[0] / 2;
                    _hasMoved = 0;
                }
            
            }

            if (_currentKey.IsKeyDown(Input.Left) )
            {
                velocity.X -= Speed;
            }
            else if(_currentKey.IsKeyDown(Input.Right))
            {
                velocity.X += Speed;
            }

            _shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(_currentKey.IsKeyDown(Input.Shoot) && _shootTimer > 0.25f && _ammoCount >= 0)
            {
                Shoot(Speed * 3, new Vector2(40, 24));
                _shootTimer = 0f;
                _ammoCount--;
                _reloadSpeed = 0;
            }
            else if (_ammoCount <= 5 && _shootTimer > 0.25f)
            {
                Reload(gameTime);
            }


            Position += velocity;

            Position = Vector2.Clamp(Position, new Vector2(80, 0), new Vector2(GameWorld.ScreenWidth / 4, GameWorld.ScreenHeight));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(IsDead)
                return;

            base.Draw(gameTime, spriteBatch);
        }

        public override void OnCollision(GameObject gameObject)
        {
            if(IsDead)
                return;

            if(gameObject is Bullet && ((Bullet)gameObject).Parent is Enemy)
                Health--;

            if(gameObject is Enemy)
                Health -= 3;
        }
        private void Reload(GameTime gameTime)
        {
            _reloadSpeed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_reloadSpeed >= 1f)
            {
                _ammoCount = 5;
                _reloadSpeed = 0;
            }
        }

    }
}
