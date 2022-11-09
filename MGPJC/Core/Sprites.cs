using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SharpDX.Direct2D1;

namespace MGPJC
{
    /// <summary>
    /// Static sprite class for loading all sprites 
    /// </summary>
    static class Sprites
    {
        /// <summary>
        /// public static properties for getting the sprites
        /// </summary>
        // Fonts
        public static SpriteFont Font { get; private set; }

        // Screen BackGrounds
        public static Texture2D SplashScreen { get; private set; }
        public static Texture2D MenuScreen { get; private set; }

        // GameScreen Background
        public static Texture2D GameScreen { get; private set; }
        public static Texture2D SunRays { get; private set; }
        public static Texture2D Vignette { get; private set; }

        // menu Buttom
        public static Texture2D ButtonPlay { get; private set; }
        public static Texture2D ButtonQuit { get; private set; }

        // Object Sprites
        public static Texture2D Player { get; private set; }
        public static Texture2D Bullet { get; private set; }        

        // Enemy Sprites
        public static Texture2D EnemyFox { get; private set; }
        public static Texture2D EnemyCoon { get; private set; }
        public static Texture2D EnemySquirrel { get; private set; }
        public static Texture2D AcornBullet { get; private set; }

        //Shop Sprites
        public static Texture2D ShopBackground { get; private set; }
        public static Texture2D ShopLizard { get; private set; }
        public static Texture2D ShopMushroom { get; private set; }
        public static Texture2D ShopPenguin { get; private set; }

        //Gameplay UI
        public static Texture2D GameplayUI { get; private set; }



        /// <summary>
        /// Load all sprites and and assign them to defined properties
        /// </summary>
        /// <param name="content"></param>
        public static void Load(ContentManager content)
        {
            // Fonts
            Font = content.Load<SpriteFont>("Font");

            // Screen BackGrounds
            SplashScreen = content.Load<Texture2D>("Graphics/Splash/Splash");
            MenuScreen = content.Load<Texture2D>("Graphics/Shop/Chicken Johnny shop");

            // GameScreen Background
            GameScreen = content.Load<Texture2D>("Graphics/Background/Chicken Johnny background");
            SunRays = content.Load<Texture2D>("Graphics/Background/Chicken Johnny sun rays");
            Vignette = content.Load<Texture2D>("Graphics/Background/Chicken Johnny vignette");

            // menu Buttom
        
            ButtonPlay = content.Load<Texture2D>("Graphics/Menu/Btn0");
            ButtonQuit = content.Load<Texture2D>("Graphics/Menu/Btn2");

            // Object Sprites
            Player = content.Load<Texture2D>("Graphics/Johnny/Johnny pistol");
            Bullet = content.Load<Texture2D>("Graphics/Johnny/Chicken Johnny pistol bullet");

            EnemyFox = content.Load<Texture2D>("Graphics/Enemies/Chicken Johnny fox");
            EnemyCoon = content.Load<Texture2D>("Graphics/Enemies/Chicken Johnny racoon");            
            EnemySquirrel = content.Load<Texture2D>("Graphics/Enemies/Chicken Johnny squirrel");
            AcornBullet = content.Load<Texture2D>("Graphics/Enemies/Chicken Johnny acorn bullet");

            //Shop sprites
            ShopBackground = content.Load<Texture2D>("Graphics/Shop/Chicken Johnny shop empty");
            ShopLizard = content.Load<Texture2D>("Graphics/Shop/Chicken Johnny shop pet lizard");
            ShopMushroom = content.Load<Texture2D>("Graphics/Shop/Chicken Johnny shop pet mushroom");
            ShopPenguin = content.Load<Texture2D>("Graphics/Shop/Chicken Johnny shop penguin");

            //Gameplay UI
            GameplayUI = content.Load<Texture2D>("Graphics/UI/Chicken Johnny ui");
        }
    }
}
