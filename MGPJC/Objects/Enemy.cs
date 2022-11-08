using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MGPJC
{
    public class Enemy : GameObject
    {
        private float _timer;

        public float ShootingTimer = 1.75f;

        public Enemy(Texture2D texture)
          : base(texture)
        {
            Speed = 2f;
        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if(_timer >= ShootingTimer)
            //{
            //    Shoot(-5f);
            //    _timer = 0;
            //}

            Position += new Vector2(-Speed, 0);

            // if the enemy is off the left side of the screen
            if(Position.X < -_texture.Width)
                IsRemoved = true;
        }

        public override void OnCollision(GameObject gameObject)
        {
            // If we hit a bullet that belongs to a player      
            if(gameObject is Bullet && ((Bullet)gameObject).Parent is Player)
            {
                Health--;

                if(Health <= 0 && IsRemoved == false)
                {
                    Score.Xp += 10;
                    IsRemoved = true;                    
                }
            }
        }
    }
}
