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
        //Rectangles for drawing the pet buttons to
        private Rectangle lizardRectangle;
        private Rectangle mushroomRectangle;
        private Rectangle penguinRectangle;

        //Rectangles for drawing the pet placement sprites
        private Rectangle lizardPlacementRectangle;
        private Rectangle mushroomPlacementRectangle;
        private Rectangle penguinPlacementRectangle;

        //Price definitions for the pets
        private int lizardPrice = 20;
        private int mushroomPrice = 40;
        private int penguinPrice = 60;

        //Tracks whether or not the shop is open
        private bool shopIsOpen = false;

        //Tracks whether or not the shop window should be drawn
        private bool shouldDrawShop = false;

        //The current pet that is being placed from shop
        private string currentPlacePet = "";            //"" Means none are being placed

        private GameWorld gameWorld;
        private GameScreen gameScreen;

        private bool mouseClickPlaceLock = false;



        /// <summary>
        /// Shopmanager constructer for creating the shop manager
        /// </summary>
        public ShopManager(GameWorld gameWorld,GameScreen gameScreen)
        {
            //Setup rectangles for pet purchase buttons
            lizardRectangle = new Rectangle(440, 540, Sprites.ShopLizard.Width, Sprites.ShopLizard.Height);
            mushroomRectangle = new Rectangle(710, 540, Sprites.ShopMushroom.Width, Sprites.ShopMushroom.Height);
            penguinRectangle = new Rectangle(980, 540, Sprites.ShopMushroom.Width, Sprites.ShopPenguin.Height);

            //Setup rectangles for pet placements 
            lizardPlacementRectangle = new(0, 0, Sprites.LizardPet.Width, Sprites.LizardPet.Height);
            mushroomPlacementRectangle = new(0, 0, Sprites.MushroomPet.Width, Sprites.MushroomPet.Height);
            penguinPlacementRectangle = new(0, 0, Sprites.PenguinPet.Width, Sprites.PenguinPet.Height);

            this.gameWorld = gameWorld;
            this.gameScreen = gameScreen;
        }



        /// <summary>
        /// Toggles the shop menu (Open og closes the shop)
        /// </summary>
        public void ToggleShop()
        {
            //Check the state of the shop, then performs the opposite
            if (shopIsOpen == true && shouldDrawShop == true) 
            {
                //Close shop
                shopIsOpen = false;

                //Close shop window
                shouldDrawShop = false;

                //Start gameplay again
                gameWorld.gameSpeed = 1;

                //Resets current placed pet
                currentPlacePet = "";
            }
            else if (gameWorld.gameSpeed > 0)
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
            if (mouseState.LeftButton == ButtonState.Pressed && shouldDrawShop == true && mouseClickPlaceLock == false)
            {
                //Check lizard button rectangle/collision for mouse click
                if (lizardRectangle.Contains(mousePosition))
                {
                    //Remove shop window but keep game paused
                    shouldDrawShop = false;

                    //Place lizard
                    currentPlacePet = "lizard";

                    //Lock so mouse click doesn't carry over to draw
                    mouseClickPlaceLock = true;
                }
                else if (mushroomRectangle.Contains(mousePosition))     //Check mushroom button rectangle/collision for mouse click
                {
                    //Remove shop window but keep game paused
                    shouldDrawShop = false;

                    //Place mushroom
                    currentPlacePet = "mushroom";

                    //Lock so mouse click doesn't carry over to draw
                    mouseClickPlaceLock = true;
                }
                else if (penguinRectangle.Contains(mousePosition))  //Check penguin button rectangle/collision for mouse click
                {
                    //Remove shop window but keep game paused
                    shouldDrawShop = false;

                    //Place penguin
                    currentPlacePet = "penguin";

                    //Lock so mouse click doesn't carry over to draw
                    mouseClickPlaceLock = true;
                }
            }
            else if (mouseState.RightButton == ButtonState.Pressed && shopIsOpen == true && shouldDrawShop == false)
            {
                //No pet is being placed
                currentPlacePet = "";

                //Undo purchase of pet, open shop window again
                shouldDrawShop = true;
            }
            
            if (mouseState.LeftButton == ButtonState.Released)
            {
                mouseClickPlaceLock = false;
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
                spritebatch.Draw(Sprites.ShopBackground, Vector2.Zero, Color.White);

                //Draw the pet purchase buttons
                spritebatch.Draw(Sprites.ShopLizard, lizardRectangle, Color.White);
                spritebatch.Draw(Sprites.ShopMushroom, mushroomRectangle, Color.White);
                spritebatch.Draw(Sprites.ShopPenguin, penguinRectangle, Color.White);

                //Draw pet purchase buttons text
                spritebatch.DrawString(gameScreen.font, $"{lizardPrice}g", new Vector2(lizardRectangle.X+80, lizardRectangle.Y + Sprites.ShopLizard.Height), Color.DarkOrange);
                spritebatch.DrawString(gameScreen.font, $"{mushroomPrice}g", new Vector2(mushroomRectangle.X + 80, mushroomRectangle.Y + Sprites.ShopMushroom.Height), Color.DarkOrange);
                spritebatch.DrawString(gameScreen.font, $"{penguinPrice}g", new Vector2(penguinRectangle.X + 80, penguinRectangle.Y + Sprites.ShopPenguin.Height), Color.DarkOrange);
            }
            else if (shopIsOpen == true && shouldDrawShop == false)
            {
                //Get mouse state for position and input
                var mouseState = Mouse.GetState();
                var mousePosition = new Point(mouseState.X, mouseState.Y);


                //
                if (currentPlacePet != "" && mouseClickPlaceLock == false)
                {
                    switch (currentPlacePet)
                    {
                        case "lizard":
                            lizardPlacementRectangle.X = mousePosition.X - lizardPlacementRectangle.Width / 2;
                            lizardPlacementRectangle.Y = mousePosition.Y - lizardPlacementRectangle.Height / 2;

                            if (gameWorld.gold >= 20 && lizardPlacementRectangle.X < 600)
                            {
                                spritebatch.Draw(Sprites.LizardPet, lizardPlacementRectangle, Color.White);

                                if (mouseState.LeftButton == ButtonState.Pressed)
                                {
                                    gameScreen.SetPet(Sprites.LizardPet,gameWorld,"Lizard",new Vector2(lizardPlacementRectangle.X,lizardPlacementRectangle.Y));
                                    gameWorld.gold -= 20;
                                    shouldDrawShop = true;
                                    mouseClickPlaceLock = true;
                                }
                            }
                            else 
                            {
                                spritebatch.Draw(Sprites.LizardPet, lizardPlacementRectangle, Color.Red);
                            }
                        break;

                        case "mushroom":
                            mushroomPlacementRectangle.X = mousePosition.X - mushroomPlacementRectangle.Width / 2;
                            mushroomPlacementRectangle.Y = mousePosition.Y - mushroomPlacementRectangle.Height / 2;

                            if (gameWorld.gold >= 40)
                            {
                                spritebatch.Draw(Sprites.MushroomPet, mushroomPlacementRectangle, Color.White);

                                if (mouseState.LeftButton == ButtonState.Pressed)
                                {
                                    gameScreen.SetPet(Sprites.MushroomPet, gameWorld, "Mushroom", new Vector2(mushroomPlacementRectangle.X, mushroomPlacementRectangle.Y));
                                    gameWorld.gold -= 40;
                                    shouldDrawShop = true;
                                    mouseClickPlaceLock = true;
                                }
                            }
                            else
                            {
                                spritebatch.Draw(Sprites.MushroomPet, mushroomPlacementRectangle, Color.Red);
                            }
                           break;

                        case "penguin":
                            penguinPlacementRectangle.X = mousePosition.X - penguinPlacementRectangle.Width / 2;
                            penguinPlacementRectangle.Y = mousePosition.Y - penguinPlacementRectangle.Height / 2;

                            if (gameWorld.gold >= 60)
                            {
                                spritebatch.Draw(Sprites.PenguinPet, penguinPlacementRectangle, Color.White);


                                if (mouseState.LeftButton == ButtonState.Pressed)
                                {
                                    gameScreen.SetPet(Sprites.PenguinPet, gameWorld, "Penguin", new Vector2(penguinPlacementRectangle.X, penguinPlacementRectangle.Y));
                                    gameWorld.gold -= 60;
                                    shouldDrawShop = true;
                                    mouseClickPlaceLock = true;
                                }
                            }
                            else
                            {
                                spritebatch.Draw(Sprites.PenguinPet, penguinPlacementRectangle, Color.Red);
                            }
                            break;
                    }
                }

                //Unlock mouse click to place pet
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    mouseClickPlaceLock = false;
                }
            }
        }
    }
}
