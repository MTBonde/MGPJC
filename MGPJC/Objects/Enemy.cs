using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MGPJC
{
    /// <summary>
    /// Enemy Class Basis for futher enemies
    /// </summary>
    public class Enemy : GameObject
    {
        private float _timer;

        public float ShootingTimer = 1.75f;

        public Enemy(Texture2D texture, GameWorld gameWorld)
          : base(texture,gameWorld)
        {
            Speed = 2f;
        }

        /// <summary>
        /// update position of enemy, remove if enemy is offscreen and change player health
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds * gameWorld.gameSpeed;

            Position += new Vector2(-Speed* gameWorld.gameSpeed, 0);

            // if the enemy is off the left side of the screen
            if(Position.X < -_texture.Width)
                IsRemoved = true;

            //Check if enemy has reached behind x 0 (reached players eggs)
            if (Position.X + _texture.Width <= 0)
            {
                //Remove 1 from player health
                if (Score.PlayerHealth > 0)
                {
                    Score.PlayerHealth--;
                }

                //Remove this enemy object
                IsRemoved = true;
            }
        }

        /// <summary>
        /// Enemy Collision handling 
        /// </summary>
        /// <param name="gameObject"></param>
        public override void OnCollision(GameObject gameObject)
        {
            // If we hit a bullet that belongs to a player      
            if(gameObject is Bullet && ((Bullet)gameObject).Parent is Player || gameObject is Bullet && ((Bullet)gameObject).Parent is Pet)
            {
                Health--;

                //removes enemy when 0 health 
                if(Health <= 0 && IsRemoved == false)
                {
                    //Provide gold to the player
                    gameWorld.gold += GameWorld.Random.Next(1,3);
                    //gives the player experience
                    Score.Xp += 10;
                    //makes sure the enemy does not trigger this function again twice before being removed
                    IsRemoved = true;                    
                }
            }
        }
    }
}
