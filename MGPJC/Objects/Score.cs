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
       
        private const int maxMultiplier = 20;

        public static int Level { get; private set; }
        public static int Xp { get; set; }
        public static int Multiplier { get; private set; }

        private static int MaxXp;       // Xp required to Level up

        public static int PlayerHealth { get; set; }


    // Static constructor
    static Score()
        {
            Reset();
        }

        public static void Reset()
        {
            Xp = 0;
            Multiplier = 0;
            Level = 0;
            MaxXp = 100;
        }

        public static void Update()
        {
            AddPoints(Xp);
            ResetMultiplier();
        }

        public static void AddPoints(int basePoints)
        {
            //if (Player.IsDead)
            //    return;

           
            while (Xp >= MaxXp)
            {
                MaxXp += 100;   
                Level++;
            }
        }

        //public static void IncreaseMultiplier()
        //{
        //    if (Player.IsDead)
        //    {
        //        multiplierTimeLeft = multiplierExpiryTime;
        //        if (Multiplier < maxMultiplier)
        //            Multiplier++;
        //    }
        //}

        public static void ResetMultiplier()
        {
            Multiplier = 1;
        }

        public static void RemoveLife()
        {
            Level--;
        }
    }
}
