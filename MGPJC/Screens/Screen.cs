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
    /// Parent class to the various screens, giving them their shared methods
    /// </summary>
    public abstract class Screen
    {
        protected GameWorld _gameWorld;

        protected ContentManager _content;

        public Screen(GameWorld gameWorld, ContentManager content)
        {
            _gameWorld = gameWorld;

            _content = content;
        }

        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch);
    }
}
