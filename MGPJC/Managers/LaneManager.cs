using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGPJC
{
    public class LaneManager
    {
        // Middle of screen is middle of middle lane, and all lanes take up only half of screen height.
        // therefor Lanesize is y / 2 / 3 lanes = y /6 pr lane.
        private static int _laneHeight = (int)GameWorld.ScreenHeight / 6;
        //private static int _laneHeight = Drawing._graphics.PreferredBackBufferHeight / 6;
        // Center of lane 1,2 & 3:
        private static int _lane1 = _laneHeight * 2-69;
        private static int _lane2 = _laneHeight * 3-69;
        private static int _lane3 = _laneHeight * 4-69;

        public static int[] LaneArray =
        {
            _lane1,
            _lane2,
            _lane3,
        };

        public static int LaneHeight { get; }

        /* 3x3 grid is y/2 on bothsides
         * so 1 grid square is lanesize * lanesize
         * space left and right of 3x3grid is lane/2
         * 
         * from left to JC's grid there is lane/2+Lane*3+lane/2= 4 laneheights
         * */
    }
}
