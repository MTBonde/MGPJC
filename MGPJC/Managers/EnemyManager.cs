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
    /// <summary>
    /// Loads Enemies texture and takes care of instantiating enemies
    /// </summary>
    public class EnemyManager
    {
        
        private float _timer;
        
        private float _animationTime;

        private List<Texture2D> _textures;

        public bool CanAdd { get; set; }

        public Bullet Bullet { get; set; }

        public int MaxEnemies { get; set; }

        public float SpawnTimer { get; set; }

        

        private GameWorld gameWorld;


        private Texture2D CurrentSprite => _textures[(int)_animationTime];

        private Random rnd = new Random();


        /// <summary>
        /// putting the enemy sprites of enemies on list
        /// </summary>
        /// <param name="content"></param>
        /// <param name="gameWorld"></param>
        public EnemyManager(ContentManager content, GameWorld gameWorld)
        {
            _textures = new List<Texture2D>()
            {   
                Sprites.EnemyFox,
                Sprites.EnemyCoon,
                Sprites.EnemySquirrel,
            };

            MaxEnemies = 30;
            SpawnTimer = 250f;

            this.gameWorld = gameWorld;
        }

        /// <summary>
        /// timer between spawned enemy 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds * (100+30*Score.Level) * gameWorld.gameSpeed;

            CanAdd = false;

            if(_timer > SpawnTimer)
            {
                CanAdd = true;

                _timer = 0;
            }
        }

        /// <summary>
        /// Instantiate new enemy when invoked in GameScreen
        /// gets the spawn position of enemy from lane manager
        /// </summary>
        /// <returns> new enemy </returns>
        public Enemy GetEnemy()
        {
            var texture = _textures[rnd.Next(0, _textures.Count)];

            int height = LaneManager.LaneArray [rnd.Next(0, LaneManager.LaneArray.Length)];
            int offset = rnd.Next(-LaneManager.LaneHeight / 8, LaneManager.LaneHeight / 8);
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
