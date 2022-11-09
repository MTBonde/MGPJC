using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;


namespace MGPJC
{
    /// <summary>
    /// manages a menu that appears when the game is started or upon pressing the enter key
    /// </summary>
    internal class MenuScreen : Screen
    {
        
        private List<Component> _components;

        private GameWorld gameWorld;


        /// <summary>
        /// Menuscreen constructer
        /// </summary>
        /// <param name="gameWorld"></param>
        /// <param name="content"></param>
        public MenuScreen(GameWorld gameWorld, ContentManager content)
          : base(gameWorld, content)
        {
            this.gameWorld = gameWorld;
        }



        /// <summary>
        /// Load content for the menu
        /// </summary>
        public override void LoadContent()
        {
            _components = new List<Component>()
            {
                new GameObject(Sprites.MenuScreen,gameWorld)
                {
                    Layer = 0f,
                    Position = new Vector2(0, 0),
                },
                new Button(Sprites.ButtonPlay, Sprites.Font)
                {
                    //Text = "Play", //place holder for futher use
                    Position = new Vector2(GameWorld.ScreenWidth / 2, 400),
                    // listen for the event 'click' and does the difined method
                    Click = new EventHandler(Play),
                    Layer = 0.1f
                },
                new Button(Sprites.ButtonQuit, Sprites.Font)
                {
                    //Text = "Quit",
                    Position = new Vector2(GameWorld.ScreenWidth / 2, 520),
                    Click = new EventHandler(Button_Quit_Clicked),
                    Layer = 0.1f
                },
            };

            //Play menu music
            MediaPlayer.Play(Audio.MainMenuMusic);
            MediaPlayer.IsRepeating = true;
        }



        /// <summary>
        /// method invoked by click play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Play(object sender, EventArgs args)
        {
            _gameWorld.ChangeScreen(new GameScreen(_gameWorld, _content));
        }



        /// <summary>
        /// methos invoked by click quit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _gameWorld.Exit();
        }



        /// <summary>
        /// Updates all components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach(var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }



        /// <summary>
        /// Draw all components, components is a base class all objects inherit from
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach(var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}