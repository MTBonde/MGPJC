using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MGPJC
{
    public class EnemyManager
    {
        private float _timer;

        private List<Texture2D> _textures;

        public bool CanAdd { get; set; }

        public Bullet Bullet { get; set; }

        public int MaxEnemies { get; set; }

        public float SpawnTimer { get; set; }


        private Random rnd = new Random();

        public EnemyManager(ContentManager content)
        {
            _textures = new List<Texture2D>()
            {
                //content.Load<Texture2D>("Chicken Johnny fox"),
                //content.Load<Texture2D>("Chicken Johnny squirrel"),
                //content.Load<Texture2D>("Chicken Johnny racoon"),        
                
                Sprites.EnemyFox,
                Sprites.EnemyCoon,
                Sprites.EnemySquirrel,
            };

            MaxEnemies = 30;
            SpawnTimer = 1.5f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CanAdd = false;

            if(_timer > SpawnTimer)
            {
                CanAdd = true;

                _timer = 0;
            }
        }

        public Enemy GetEnemy()
        {
            var texture = _textures[rnd.Next(0, _textures.Count)];

            // TODO: fix height and offset
            int height = LaneManager.LaneArray[rnd.Next(0, LaneManager.LaneArray.Length)];
            int offset = rnd.Next(-LaneManager.LaneHeight / 12, LaneManager.LaneHeight / 12);
            //int offset = 0;
            int _placetoSpawn = height + offset;

            

            if(_textures.Count == 0)
            {
                return new Enemy(texture)
                {

                    //Scale = 0.1f,
                    Colour = Color.White,
                    Bullet = Bullet,
                    Health = 50,
                    Layer = 0.2f,
                    Position = new Vector2(GameWorld.ScreenWidth + texture.Width, _placetoSpawn),
                    Speed = rnd.Next(1, 5) + (float)rnd.NextDouble(),
                    ShootingTimer = 1.5f + (float)GameWorld.Random.NextDouble(),
                };
            }
            else
            {
                return new Enemy(texture)
                {

                    //Scale = 0.1f,
                    Colour = Color.White,
                    Bullet = Bullet,
                    Health = 1,
                    Layer = 0.2f,
                    Position = new Vector2(GameWorld.ScreenWidth + texture.Width, _placetoSpawn),
                    Speed = rnd.Next(1, 5) + (float)rnd.NextDouble(),
                    ShootingTimer = 1.5f + (float)GameWorld.Random.NextDouble(),
                };
            }

            //return new Enemy(texture)
            //{
                
            //    //Scale = 0.1f,
            //    Colour = Color.White,
            //    Bullet = Bullet,
            //    Health = 5,
            //    Layer = 0.2f,
            //    Position = new Vector2(GameWorld.ScreenWidth + texture.Width, _placetoSpawn),
            //    Speed = rnd.Next(1,5) + (float)rnd.NextDouble(),
            //    ShootingTimer = 1.5f + (float)GameWorld.Random.NextDouble(),
            //};
        }
    }
}
