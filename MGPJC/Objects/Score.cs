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
     static class Score
    {
       
        /// <summary>
        /// The Player levels up, when Xp gets to 100.
        /// </summary>
        public static int Level { get; private set; }
        /// <summary>
        /// Enemies drop Xp whn killed. 
        /// </summary>
        public static int Xp { get; set; }
        /// <summary>
        ///  Xp required to Level up
        /// </summary>
        private static int MaxXp;       

        public static int PlayerHealth { get; set; }


    // Static constructor
    static Score()
        {
            Reset();
        }

        public static void Reset()
        {
            Xp = 0;
            Level = 0;
            MaxXp = 100;
        }

        public static void Update()
        {
            AddPoints(Xp);

        }
        /// <summary>
        /// AddPoints is called in Update, right above.
        /// </summary>
        /// <param name="basePoints"></param>
        public static void AddPoints(int basePoints)
        {
         
           
            while (Xp >= MaxXp)
            {
                MaxXp += 100;   
                Level++;
            }
        }


       
        
    }
}
