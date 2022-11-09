using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MGPJC
{
    /// <summary>
    /// controls the main game screen and the gameplay
    /// </summary>
    public class GameScreen : Screen
    {
        private EnemyManager _enemyManager;

        private List<GameObject> _gameObjectList;

        public SpriteFont font;

        private ShopManager _shopManager;

        private bool keyTabLock = false;            //If true, another keypress of tab cannot be registered

        private GameWorld gameWorld;

        private Player player;

        private Pet pet;
        


        /// <summary>
        /// Constructer to instantiate GameScreen object
        /// </summary>
        /// <param name="game"></param>
        /// <param name="content"></param>
        public GameScreen(GameWorld game, ContentManager content) : base(game, content)
        {
            this.gameWorld = game;
        }



        /// <summary>
        /// Load content used in gameplay screen
        /// </summary>
        public override void LoadContent()
        {
            //Load pet <<<DELETE
            //var petTexture = _content.Load<Texture2D>("Johnny pistol");

            //Load font for UI
            font = _content.Load<SpriteFont>("Font");

            //Create instances to draw background, sun ray overlay and vignette
            _gameObjectList = new List<GameObject>()
            {
                new GameObject(Sprites.GameScreen,gameWorld)
                {
                    Layer = 0.0f,
                    Position = new Vector2(0, 0),
                },
                new GameObject(Sprites.SunRays,gameWorld)
                {
                    Layer = 0.0f,
                    Position = new Vector2(0, 0),
                },
                new GameObject(Sprites.Vignette,gameWorld)
                {
                    Layer = 0.0f,
                    Position = new Vector2(0, 0),
                }
            };

            //Create a bullet prefab and set it's layer/depth for draw
            var bulletPrefab = new Bullet(gameWorld)
            {
                Layer = 0.5f
            };

            //DELETE<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            pet = new Pet(Sprites.ShopLizard, gameWorld, "Lizard")
            {
                Position = new Vector2(69, LaneManager.LaneArray[1]),
                Layer = 0.2f,
                Bullet = bulletPrefab
            };
            _gameObjectList.Add(pet);


            //Instantiate player object
            player = new Player(Sprites.Player, gameWorld)
            {
                Colour = Color.White,
                
                // Starts the player in the middle lane, 800 pixels from the left happens to be perfectly at the end of the lane
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
                Health = 3,
            };

            //Add player to object list
            _gameObjectList.Add(player);

            //Instantiate enemy manager object
            _enemyManager = new EnemyManager(_content,gameWorld)
            {
                Bullet = bulletPrefab
            };

            //Create instance of shop manager
            _shopManager = new ShopManager(gameWorld,this);

            //Set starting health of player
            Score.PlayerHealth = 3;
        }



        /// <summary>
        /// Updates gamescreen every game frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //Returns to main menu screen when pressing escape
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                _gameWorld.ChangeScreen(new MenuScreen(_gameWorld, _content));
            
            //Go through and update all gameobjects in gameobject list and also check for collisions between them
            foreach (GameObject go in _gameObjectList)
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

            //Update enemy manager object
            _enemyManager.Update(gameTime);

            //Adds the spawned enemies from enemy manager to gameobject list
            if(_enemyManager.CanAdd && _gameObjectList.Where(c => c is Enemy).Count() < _enemyManager.MaxEnemies)
            {
                _gameObjectList.Add(_enemyManager.GetEnemy());
            }

            //Open shop with tab key
            if (Keyboard.GetState().IsKeyDown(Keys.Tab) && keyTabLock == false)
            {
                //Toggle shop window
                _shopManager.ToggleShop();

                //Lock so keypress isn't registered multiple times
                keyTabLock = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Tab))
            {
                //Unlock tab key so it can be pressed again
                keyTabLock = false;
            }

            //Update shop manager
            _shopManager.Update();
        }



        /// <summary>
        /// Post update runs at the end of update method
        /// </summary>
        /// <param name="gameTime"></param>
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

            spriteBatch.DrawString(font, $"    XP: {Score.Xp} / 100", Vector2.Zero, Color.Black);
            spriteBatch.DrawString(font, $"\n    Level: {Score.Level}", Vector2.Zero, Color.Black);

            //Draw gameplay ui(heart and coin)
            spriteBatch.Draw(Sprites.GameplayUI, Vector2.Zero, Color.White);

            //Draw player hp and gold to gameplay ui
            spriteBatch.DrawString(font, $"{Score.PlayerHealth}", new Vector2(430,945), Color.Black);
            spriteBatch.DrawString(font, $"{gameWorld.gold}", new Vector2(1750, 945), Color.Black);

            spriteBatch.End();


            //Call draw method on shop manager (Needs to be drawn on top of everything else, so give seperate spritebatch section)
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            _shopManager.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
