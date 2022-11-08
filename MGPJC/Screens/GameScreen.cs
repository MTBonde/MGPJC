using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace MGPJC
{
    internal class GameScreen : Screen
    {
        private EnemyManager _enemyManager;

        private List<GameObject> _gameObjectList;

        public GameScreen(GameWorld game, ContentManager content) : base(game, content)
        {
        }

        public override void LoadContent()
        {
            var playerTexture = _content.Load<Texture2D>("Johnny pistol");
            var bulletTexture = _content.Load<Texture2D>("Chicken Johnny pistol bullet");            

            _gameObjectList = new List<GameObject>()
            {
                new GameObject(_content.Load<Texture2D>("Chicken Johnny background"))
                {
                    Layer = 0.0f,
                    Position = new Vector2(0, 0),
                }
            };

            var bulletPrefab = new Bullet(bulletTexture)
            {
                Layer = 0.5f
            };

            _gameObjectList.Add(new Player(playerTexture)
            {
                Colour = Color.White,
                //Position = new Vector2(100, 50),
                Position = new Vector2(800, LaneManager.LaneArray[1]),
                Layer = 0.3f,
                Bullet = bulletPrefab,
                Input = new Input()
                {
                    Up = Keys.W,
                    Down = Keys.S,
                    Left = Keys.A,
                    Right = Keys.D,
                    Shoot = Keys.Space,
                },
                Health = 10,                
            });

            _enemyManager = new EnemyManager(_content)
            {
                Bullet = bulletPrefab,
            };
        }

        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                _gameWorld.ChangeScreen(new MenuScreen(_gameWorld, _content));

            foreach(GameObject go in _gameObjectList)
            {
                go.Update(gameTime);

                foreach(GameObject other in _gameObjectList)
                {
                    if(go.IsColliding(other))
                    {
                        go.OnCollision(other);
                        other.OnCollision(go);
                    }
                }
            }
            _enemyManager.Update(gameTime);
            if(_enemyManager.CanAdd && _gameObjectList.Where(c => c is Enemy).Count() < _enemyManager.MaxEnemies)
            {
                _gameObjectList.Add(_enemyManager.GetEnemy());
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            var collidableGameObjects = _gameObjectList.Where(c => c is ICanCollide);

            foreach(var GameObject1 in collidableGameObjects)
            {
                foreach(var GameObject2 in collidableGameObjects)
                {
                    // Don't do anything if they're the same GameObject!
                    if(GameObject1 == GameObject2)
                        continue;

                    if(!GameObject1.CollisionBox.Intersects(GameObject2.CollisionBox))
                        continue;

                    // TODO : fix intersect
                    
                    //if(GameObject1.Intersects(GameObject2))
                    //    ((ICanCollide)GameObject1).OnCollision(GameObject2);
                }
            }

            // Add the children sprites to the list of sprites (ie bullets)
            int spriteCount = _gameObjectList.Count;
            for(int i = 0; i < spriteCount; i++)
            {
                var sprite = _gameObjectList[i];
                foreach(var child in sprite.Children)
                    _gameObjectList.Add(child);

                sprite.Children = new List<GameObject>();
            }

            for(int i = 0; i < _gameObjectList.Count; i++)
            {
                if(_gameObjectList[i].IsRemoved)
                {
                    _gameObjectList.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach(var sprite in _gameObjectList)
                sprite.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            
            
            //spriteBatch.DrawString(_font, "Health: " + Player.Health, new Vector2(x, 30f), Color.White);
            
            spriteBatch.End();
        }
    }
}
