using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGPJC
{
    internal class GameScreen : Screen
    {
        private EnemyManager _enemyManager;

        private List<GameObject> _gameObject;

        public GameScreen(GameWorld game, ContentManager content) : base(game, content)
        {
        }

        public override void LoadContent()
        {
            var playerTexture = _content.Load<Texture2D>("Johnny pistol");
            var bulletTexture = _content.Load<Texture2D>("Chicken Johnny pistol bullet");            

            _gameObject = new List<GameObject>()
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

            _gameObject.Add(new Player(playerTexture)
            {
                Colour = Color.White,
                Position = new Vector2(100, 50),
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

            //_players = _gameObject.Where(c => c is Player).Select(c => (Player)c).ToList();

            _enemyManager = new EnemyManager(_content)
            {
                Bullet = bulletPrefab,
            };
        }

        public override void Update(GameTime gameTime)
        {
            //if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    _game.ChangeState(new MenuState(_game, _content));

            foreach(var sprite in _gameObject)
                sprite.Update(gameTime);

            _enemyManager.Update(gameTime);
            if(_enemyManager.CanAdd && _gameObject.Where(c => c is Enemy).Count() < _enemyManager.MaxEnemies)
            {
                _gameObject.Add(_enemyManager.GetEnemy());
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            var collidableSprites = _gameObject.Where(c => c is ICanCollide);

            foreach(var spriteA in collidableSprites)
            {
                foreach(var spriteB in collidableSprites)
                {
                    // Don't do anything if they're the same sprite!
                    if(spriteA == spriteB)
                        continue;

                    if(!spriteA.CollisionArea.Intersects(spriteB.CollisionArea))
                        continue;

                    //if(spriteA.Intersects(spriteB))
                    //    ((ICanCollide)spriteA).OnCollision(spriteB);
                }
            }

            // Add the children sprites to the list of sprites (ie bullets)
            int spriteCount = _gameObject.Count;
            for(int i = 0; i < spriteCount; i++)
            {
                var sprite = _gameObject[i];
                foreach(var child in sprite.Children)
                    _gameObject.Add(child);

                sprite.Children = new List<GameObject>();
            }

            for(int i = 0; i < _gameObject.Count; i++)
            {
                if(_gameObject[i].IsRemoved)
                {
                    _gameObject.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach(var sprite in _gameObject)
                sprite.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            
            
            //spriteBatch.DrawString(_font, "Health: " + Player.Health, new Vector2(x, 30f), Color.White);
            
            spriteBatch.End();
        }
    }
}
