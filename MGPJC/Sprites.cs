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

        // Shop Sprite
        public static Texture2D Shop { get; private set; }



        public static void Load(ContentManager content)
        {
            // Fonts
            Font = content.Load<SpriteFont>("Font");

            // Screen BackGrounds
            //SplashScreen = content.Load<Texture2D>("Splash");
            MenuScreen = content.Load<Texture2D>("Chicken Johnny shop");
            GameScreen = content.Load<Texture2D>("Chicken Johnny background");

            // menu Buttom
            ButtonPlay = content.Load<Texture2D>("Btn0");
            ButtonQuit = content.Load<Texture2D>("Btn2");

            // Object Sprites
            Player = content.Load<Texture2D>("Johnny pistol");
            Bullet = content.Load<Texture2D>("Chicken Johnny pistol bullet");

            EnemyFox = content.Load<Texture2D>("Chicken Johnny fox");
            EnemyCoon = content.Load<Texture2D>("Chicken Johnny racoon");            
            EnemySquirrel = content.Load<Texture2D>("Chicken Johnny squirrel");

            //Shop Sprite
            Shop = content.Load<Texture2D>("Chicken Johnny shop");

        }
    }
}
