using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGPJC
{
    /// <summary>
    /// Base Component that objects inherit from which lets us use them as components in a component/composit?? pattern
    /// all child components will be part of a whole that all share a base type
    /// </summary>
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
