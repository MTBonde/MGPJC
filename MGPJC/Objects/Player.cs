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
    /// <summary>
    /// controls the player's character
    /// </summary>
    public class Player : GameObject
    {
        private KeyboardState _currentKey;

        private KeyboardState _previousKey;

        private float _shootTimer = 0;

        /// <summary>
        /// a variable float to determine how long since the player has been reloading
        /// </summary>
        private float _reloadSpeed = 0;
        /// <summary>
        /// how many times the player can shoot before needing to reload
        /// </summary>
        private int _ammoCount = 6;
        /// <summary>
        /// the player's current lane position
        /// </summary>
        private byte _currentLane = 1;
        /// <summary>
        /// used to prevent the player from moving every frame
        /// </summary>
        private bool _hasMoved = false;
        /// <summary>
        /// Used to stop other functions if the player is dead
        /// </summary>
        public bool IsDead;

        public Input Input { get; set; }


        public Player(Texture2D texture, GameWorld gameWorld)
          : base(texture, gameWorld)
        {
            Speed = 3f;
        }
        /// <summary>
        /// Update position, life and reload of player
        /// </summary>
        /// <param name="gameTime"></param>
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

            if (_currentKey.IsKeyDown(Input.Shoot) && _shootTimer > 0.25f && _ammoCount > 0 && gameWorld.gameSpeed > 0)
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
        /// <summary>
        /// if player is dead jump out and do nothing
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsDead)
                return;

            base.Draw(gameTime, spriteBatch);
        }
        /// <summary>
        /// Collision handling
        /// </summary>
        /// <param name="gameObject"></param>
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
        /// <summary>
        /// Reload after 6 shots        
        /// </summary>
        /// <param name="gameTime"></param>
        private void Reload(GameTime gameTime)
        {
            _reloadSpeed += (float)gameTime.ElapsedGameTime.TotalSeconds * gameWorld.gameSpeed;
            if (_reloadSpeed >= 0.8f)
            {
                _ammoCount = 6;
                _reloadSpeed = 0;
            }
        }
    }
}
