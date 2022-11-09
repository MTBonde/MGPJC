using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPJC
{
    /// <summary>
    /// A pet class to create pets that assist the player when bought
    /// </summary>
    internal class Pet : GameObject
    {
        /// <summary>
        /// counts how long since the pet last shot or used a similar method
        /// </summary>
        private float _timer;
        /// <summary>
        /// meant to be used for switch cases in the update method to decide what the pet does based on it's type
        /// </summary>
        private string petType;
        /// <summary>
        /// sets the time between each instance of the shoot function being called
        /// </summary>
        public float ShootingTimer = 1.75f;

        public Pet(Texture2D texture, GameWorld gameWorld, string petType)
          : base(texture, gameWorld)
        {
            this.petType = petType;
        }
        /// <summary>
        /// invoke shoot method for pets
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds * gameWorld.gameSpeed;

            if(_timer >= ShootingTimer)
            {
                switch (petType)
                {
                    case "Lizard":
                        if (_timer > ShootingTimer)
                        {
                            Shoot(7, new Vector2(40, 40),"Fireball");
                            _timer = 0.1f;
                        }
                        break;
                    case "Mushroom":
                        if (_timer > ShootingTimer)
                        {
                            Shoot(5, new Vector2(40, 40),"Goo");
                            _timer = 1f;

                        }
                        break;
                    case "Penguin":
                        if (_timer > ShootingTimer)
                        {
                            gameWorld.gold += 1;
                            _timer = 0.75f;
                        }
                        break;
                }
            }


        }

    }
}
