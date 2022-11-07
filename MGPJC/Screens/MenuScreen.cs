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
    internal class MenuScreen : Screen
    {
        private List<Component> _components;

        public MenuScreen(GameWorld gameWorld, ContentManager content)
          : base(gameWorld, content)
        {
        }

        public override void LoadContent()
        {
            //var buttonTexture1 = _content.Load<Texture2D>("Btn0");
            //var buttonTexture2 = _content.Load<Texture2D>("Btn2");

            _components = new List<Component>()
            {
                new GameObject(Sprites.MenuScreen)
                {
                    Layer = 0f,
                    Position = new Vector2(0, 0),
                },
                new Button(Sprites.ButtonPlay, Sprites.Font)
                {
                    //Text = "Play",
                    Position = new Vector2(GameWorld.ScreenWidth / 2, 400),
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
        }

        private void Play(object sender, EventArgs args)
        {
            _gameWorld.ChangeScreen(new GameScreen(_gameWorld, _content));
        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _gameWorld.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach(var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}

