using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGPJC
{
    static class Sprites
    {
        // Fonts
        public static SpriteFont Font { get; private set; }

        // Screen BackGrounds
        public static Texture2D SplashScreen { get; private set; }
        public static Texture2D MenuScreen { get; private set; }
        public static Texture2D GameScreen { get; private set; }

        // menu Buttom
        public static Texture2D ButtonPlay { get; private set; }
        public static Texture2D ButtonQuit { get; private set; }

        // Object Sprites
        public static Texture2D Player { get; private set; }
        public static Texture2D Bullet { get; private set; }        

        public static Texture2D EnemyFox { get; private set; }
        public static Texture2D EnemyCoon { get; private set; }
        public static Texture2D EnemySquirrel { get; private set; }




        public static void Load(ContentManager content)
        {
            // Fonts
            Font = content.Load<SpriteFont>("Font");

            // Screen BackGrounds
            SplashScreen = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\Splash\\Splash.png");
            MenuScreen = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\Shop\\Chicken Johnny shop.png");
            GameScreen = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\Background\\Chicken Johnny background.png");

            // menu Buttom
            ButtonPlay = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\MenuScreen\\Btn0.png");
            ButtonQuit = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\MenuScreen\\Btn2.png");

            // Object Sprites
            Player = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\Johnny\\Johnny pistol");
            Bullet = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\Johnny\\Chicken Johnny pistol bullet.png");

            EnemyFox = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\Enemies\\Chicken Johnny fox.png");
            EnemyCoon = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\Enemies\\Chicken Johnny racoon.png");            
            EnemySquirrel = content.Load<Texture2D>("C:\\Users\\Moxy\\source\\repos\\MGPJC\\MGPJC\\Content\\Graphics\\Enemies\\Chicken Johnny squirrel.png");

        }
    }
}
