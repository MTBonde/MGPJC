using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace MGPJC
{
    static class Audio
    {
        // Music stored in static variables
        public static Song MainMenuMusic { get; private set; }



        /// <summary>
        /// Load all audio files for easier access in rest of the project
        /// </summary>
        /// <param name="content"></param>
        public static void Load(ContentManager content)
        {
            // Music
            MainMenuMusic = content.Load<Song>("Audio/Playing Chicken");
        }
    }
}
