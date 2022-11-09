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
        private byte _currentLane = 1;
        private bool _hasMoved = false;
        public bool IsDead;

        public Input Input { get; set; }


        public Player(Texture2D texture, GameWorld gameWorld)
          : base(texture, gameWorld)
        {
            Speed = 3f;
        }

        public override void Update(GameTime gameTime)
        {
            //Check if hp is below 0 and player is dead
            if (Score.PlayerHealth <= 0)
            {
                IsDead = true;
            }

            if (IsDead)
                return;

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            var velocity = Position;
            _rotation = 0;

            if (_currentKey.IsKeyDown(Input.Up) && _currentLane != 0 && gameWorld.gameSpeed > 0)
            {
                if (_hasMoved == false)
                {
                    _currentLane--;
                    velocity.Y = LaneManager.LaneArray[_currentLane];
                    _hasMoved = true;
                }
            }
            else if (_currentKey.IsKeyDown(Input.Down) && _currentLane != 2 && gameWorld.gameSpeed > 0)
            {
                if (_hasMoved == false)
                {
                    _currentLane++;
                    velocity.Y = LaneManager.LaneArray[_currentLane];
                    _hasMoved = true;
                }
            }
            else _hasMoved = false;

            _shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * gameWorld.gameSpeed;

            if (_currentKey.IsKeyDown(Input.Shoot) && _shootTimer > 0.25f && _ammoCount >= 0 && gameWorld.gameSpeed > 0)
            {
                Shoot(Speed * 3, new Vector2(24, 24));
                _shootTimer = 0f;
                _ammoCount--;
                _reloadSpeed = 0;
            }
            else if (_ammoCount <= 5 && _shootTimer > 0.25f)
            {
                Reload(gameTime);
            }


            velocity.X = Position.X;
            Position = velocity;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsDead)
                return;

            base.Draw(gameTime, spriteBatch);
        }

        public override void OnCollision(GameObject gameObject)
        {
            if (IsDead)
                return;

            if (gameObject is Bullet && ((Bullet)gameObject).Parent is Enemy)
                Score.PlayerHealth = 0;

            if (gameObject is Enemy)
            {
                Score.PlayerHealth = 0;
            }
        }
        private void Reload(GameTime gameTime)
        {
            _reloadSpeed += (float)gameTime.ElapsedGameTime.TotalSeconds * gameWorld.gameSpeed;
            if (_reloadSpeed >= 0.8f)
            {
                _ammoCount = 5;
                _reloadSpeed = 0;
            }
        }

    }
}
