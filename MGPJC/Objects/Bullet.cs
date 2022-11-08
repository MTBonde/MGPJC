﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MGPJC
{
    public class Bullet : GameObject, ICanCollide
    {
        private float _timer;

        //public Explosion Explosion;

        public float LifeSpan { get; set; }

        public Vector2 Velocity { get; set; }

        public Bullet(GameWorld gameWorld)
          : base(Sprites.Bullet,gameWorld)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds * gameWorld.gameSpeed;

            if(_timer >= LifeSpan)
                IsRemoved = true;

            Position += Velocity * gameWorld.gameSpeed;
        }

        public override void OnCollision(GameObject gameObject)
        {
            // Bullets don't collide with other bullets
            if(gameObject is Bullet)
                return;

            // EnemySquirrel can't shoot other Enemies
            if(gameObject is Enemy && this.Parent is Enemy)
                return;

            if(gameObject is Enemy && this.Parent is Player)
            {
                IsRemoved = true;                
            }

            if(gameObject is Player && this.Parent is Enemy)
            {
                IsRemoved = true;
            }

            if(Position.X > GameWorld.ScreenWidth)
            {
                IsRemoved = true;
            }
        }
    }
}
