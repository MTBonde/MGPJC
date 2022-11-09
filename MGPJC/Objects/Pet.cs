using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPJC
{
    internal class Pet : GameObject
    {
        private float _timer;
        private string petType;
        public float ShootingTimer = 1.75f;

        public Pet(Texture2D texture, GameWorld gameWorld, string petType)
          : base(texture, gameWorld)
        {
            this.petType = petType;
        }
        
        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds * gameWorld.gameSpeed;

            if(_timer >= ShootingTimer)
            {
                Shoot(5f, new Vector2 (69, 40));
                _timer = 0;
            }


        }

    }
}
