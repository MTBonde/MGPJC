using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;

namespace MGPJC
{
    public class ShopManager
    {
        //Setup texture list to store textures for drawing
        private List<Texture2D> textures;

        //Rectangles for drawing the pet buttons to
        private Rectangle lizardRectangle;
        private Rectangle mushroomRectangle;
        private Rectangle penguinRectangle;

        //Price definitions for the pets
        private int lizardPrice = 20;
        private int mushroomPrice = 40;
        private int penguinPrice = 60;

        //Tracks whether or not the shop is open
        private bool shopIsOpen = false;

        //Tracks whether or not the shop window should be drawn
        private bool shouldDrawShop = false;

        private GameWorld gameWorld;
        private GameScreen gameScreen;



        /// <summary>
        /// Shopmanager constructer for creating the shop manager
        /// </summary>
        public ShopManager(GameWorld gameWorld,GameScreen gameScreen)
        {
            //Add textures for the shop to the texture list
            textures = new List<Texture2D>()
            {
                Sprites.ShopBackground,
                Sprites.ShopLizard,
                Sprites.ShopMushroom,
                Sprites.ShopPenguin
            };

            //Setup rectangles for pet purchase buttons
            lizardRectangle = new Rectangle(440, 540, textures[1].Width, textures[1].Height);
            mushroomRectangle = new Rectangle(710, 540, textures[1].Width, textures[1].Height);
            penguinRectangle = new Rectangle(980, 540, textures[1].Width, textures[1].Height);


            this.gameWorld = gameWorld;
            this.gameScreen = gameScreen;
        }



        /// <summary>
        /// Toggles the shop menu (Open og closes the shop)
        /// </summary>
        public void ToggleShop()
        {
            //Check the state of the shop, then performs the opposite
            if (shopIsOpen == true)
            {
                //Close shop
                shopIsOpen = false;

                //Close shop window
                shouldDrawShop = false;

                //Start gameplay again
                gameWorld.gameSpeed = 1;
            }
            else
            {
                //Open shop
                shopIsOpen = true;

                //Draw shop window
                shouldDrawShop = true;

                //Pause gameplay while shop is open
                gameWorld.gameSpeed = 0;
            }
        }



        /// <summary>
        /// Update method for shop manager
        /// </summary>
        public void Update()
        {
            //Get mouse state for position and input
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            //If left mouse button is pressed, check if one of the buy options were bought
            if (mouseState.LeftButton == ButtonState.Pressed && shouldDrawShop == true)
            {
                //Check lizard button rectangle/collision for mouse click
                if (lizardRectangle.Contains(mousePosition))
                {
                    //Remove shop window but keep game paused
                    shouldDrawShop = false;

                    //Place lizard
                }
                else if (mushroomRectangle.Contains(mousePosition))     //Check mushroom button rectangle/collision for mouse click
                {
                    //Remove shop window but keep game paused
                    shouldDrawShop = false;

                    //Place mushroom
                }
                else if (penguinRectangle.Contains(mousePosition))  //Check penguin button rectangle/collision for mouse click
                {
                    //Remove shop window but keep game paused
                    shouldDrawShop = false;

                    //Place penguin
                }
            }
            else if (mouseState.RightButton == ButtonState.Pressed && shopIsOpen == true)
            {
                //If a pet is being placed, cancel out
                if (shouldDrawShop == false)
                {
                    //Undo purchase of pet, open shop window again
                    shouldDrawShop = true;
                }
                else
                {
                    //If the shop window is open, close it instead
                    shopIsOpen = false;

                    //Close shop window
                    shouldDrawShop = false;

                    //Start gameplay again
                    gameWorld.gameSpeed = 1;
                }
            }
        }



        /// <summary>
        /// Draws out the shop and components to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            //Check whether or not the shop has been opened before drawing
            if (shouldDrawShop == true)
            {
                //Draw the shop background
                spritebatch.Draw(textures[0], Vector2.Zero, Color.White);

                //Draw the pet purchase buttons
                spritebatch.Draw(textures[1], lizardRectangle, Color.White);
                spritebatch.Draw(textures[2], mushroomRectangle, Color.White);
                spritebatch.Draw(textures[3], penguinRectangle, Color.White);

                //Draw pet purchase buttons text
                spritebatch.DrawString(gameScreen.font, $"{lizardPrice}g", new Vector2(lizardRectangle.X+80, lizardRectangle.Y + textures[1].Height), Color.DarkOrange);
                spritebatch.DrawString(gameScreen.font, $"{mushroomPrice}g", new Vector2(mushroomRectangle.X + 80, mushroomRectangle.Y + textures[1].Height), Color.DarkOrange);
                spritebatch.DrawString(gameScreen.font, $"{penguinPrice}g", new Vector2(penguinRectangle.X + 80, penguinRectangle.Y + textures[1].Height), Color.DarkOrange);
            }
        }
    }
}
