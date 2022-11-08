using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Bullet bullet { get; set; }

        public int MaxEnemies { get; set; }

        public float SpawnTimer { get; set; }

        private float animationTime;

        private GameWorld gameWorld;


        private Texture2D currentSprite
        {
            get
            {
                return _textures[(int)animationTime];

            }
        }

        private Random rnd = new Random();

        public EnemyManager(ContentManager content, GameWorld gameWorld)
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
            SpawnTimer = 250f;

            this.gameWorld = gameWorld;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds * (100+10*Score.Level) * gameWorld.gameSpeed;

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
            int height = LaneManager.LaneArray [rnd.Next(0, LaneManager.LaneArray.Length)];
            int offset = rnd.Next(-LaneManager.LaneHeight / 8, LaneManager.LaneHeight / 8);
            //int offset = 0;
            int _placetoSpawn = height + offset - 69;
            

            return new Enemy(texture,gameWorld)
            {
                //Scale = 0.1f,
                Colour = Color.White,
                Health = 3,
                Layer = 0.2f,
                Position = new Vector2(GameWorld.ScreenWidth + texture.Width, _placetoSpawn),
                Speed = rnd.Next(3,5) + (float)rnd.NextDouble(),
                ShootingTimer = 1.5f + (float)GameWorld.Random.NextDouble(),
            };
        }
    }
}
