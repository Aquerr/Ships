using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    public class GameConfiguration
    {
        //TODO: Add move time, etc.

        public static readonly string[] GridCharacters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        public static readonly int OneFlagShipsCount = 4;
        public static readonly int TwoFlagShipsCount = 3;
        public static readonly int ThreeFlagShipsCount = 2;
        public static readonly int FourFlagShipsCount = 1;
    }
}
