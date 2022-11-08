using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPJC
{
    static class Score
    {
        private const int maxMultiplier = 20;

        public static int Level { get; private set; }
        public static int Xp { get; set; }
        public static int Multiplier { get; private set; }

        private static int MaxXp;       // Xp required to Level up

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
