using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MGPJC
{
    public class GameWorld : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;


        public static Random Random;

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        private Screen _currentScreen;
        private Screen _nextScreen;

        public float gameSpeed = 1;
        public int gold = 0;



        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Random = new Random();

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprites.Load(Content);
            _currentScreen = new MenuScreen(this, Content);
            _currentScreen.LoadContent();
            _nextScreen = null;
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Score.Update();
            if(_nextScreen != null)
            {
                _currentScreen = _nextScreen;
                _currentScreen.LoadContent();

                _nextScreen = null;
            }
            

            _currentScreen.Update(gameTime);

            _currentScreen.PostUpdate(gameTime);
            
           

            base.Update(gameTime);
            
        }

        public void ChangeScreen(Screen screen)
        {
            _nextScreen = screen;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(55, 55, 55));

           


            _currentScreen.Draw(gameTime, _spriteBatch);
            

            base.Draw(gameTime);
        }
    }
}