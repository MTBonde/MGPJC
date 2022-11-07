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
    internal class SplashScreen : Screen
    {
        private Texture2D _splash;
        private SpriteFont font;

        private Random _rnd;

        private Color _fontColor = Color.CornflowerBlue;

        public SplashScreen(GameWorld gameWorld, ContentManager content) : base(gameWorld, content)
        {
            _rnd = new Random();
        }

        

        public override void LoadContent()
        {
            _splash = _content.Load<Texture2D>("Splash");
            font = _content.Load<SpriteFont>("Font");
            //new GameObject(_content.Load<Texture2D>("Splash"))
            //{
            //    Layer = 0f,
            //    Position = new Vector2(GameWorld.ScreenWidth / 2, GameWorld.ScreenHeight / 2),
            //};
        }

        

        public override void Update(GameTime gameTime)
        {
            _fontColor = new Color(_rnd.Next(0, 255), _rnd.Next(0, 255), _rnd.Next(0, 255));
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_splash, new Vector2(GameWorld.ScreenWidth / 2, GameWorld.ScreenHeight / 2), Color.White);

            _spriteBatch.DrawString(font, "Press 'SPACE' to start", new Vector2(100, 100), Color.White);

            for(int i = 0; i < 20; i++)
            {
                //rgb
                _spriteBatch.DrawString(font, "Running on The Scoffy Code Engine V3 (tm)", new Vector2(GameWorld.ScreenHeight / 4 * 3, GameWorld.ScreenWidth / 2), _fontColor);
            }
        }
    }
}
