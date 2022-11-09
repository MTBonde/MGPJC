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
    public class Button : Component
    {
        #region Fields

        //mouse states
        private MouseState _previousMouse;
        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;        

        private Texture2D _texture;

        #endregion

        #region Properties
        //Event handler takes care of the event call that other methods can subscribe to and listen for
        public EventHandler Click;

        public bool Clicked { get; private set; }

        public float Layer { get; set; }

        public Vector2 Origin => new Vector2(_texture.Width / 2, _texture.Height / 2);
        // TODO: Conditional statement work with sprite class
        //public Vector2 Origin => _texture == null ? Vector2.Zero : new Vector2(_texture.Width, _texture.Height);

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle => new Rectangle((int)Position.X - ((int)Origin.X), (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);

        public string Text;

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.Black;
        }
        /// <summary>
        /// Draw button and possible text
        /// change color when hover
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if(_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, Position, null, colour, 0f, Origin, 1f, SpriteEffects.None, Layer);

            if(!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, Layer + 0.01f);
            }
        }
        /// <summary>
        /// update mouse and look for mouse rect intersect with button rect
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if(mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                // when event: click is called we invoke the actions defined for click
                if(_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
