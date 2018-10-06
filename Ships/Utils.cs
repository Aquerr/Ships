using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    public class Utils
    {
        public static int GetTile(String tileId, Direction direction)
        {
            String character = tileId.Substring(0, 1);
            int characterIndex = Array.IndexOf(GameConfiguration.GridCharacters, character);
            int id = int.Parse(tileId.Substring(1));

            switch (direction)
            {
                case Direction.Up:
                    return GetAboveTile(characterIndex, id);
                case Direction.UpLeft:
                    return GetAboveLeftTile(characterIndex, id);
                case Direction.UpRight: 
                    return GetAboveRightTile(characterIndex, id);
                case Direction.Left:
                    return GetLeftTile(characterIndex, id);
                case Direction.Right:
                    return GetLeftTile(characterIndex, id);
                case Direction.Down:
                    return GetBelowTile(characterIndex, id);
                case Direction.DownLeft:
                    return GetBelowLeftTile(characterIndex, id);
                case Direction.DownRight:
                    return GetBelowRightTile(characterIndex, id);
            }

            return 0;
        }

        //Util methods which returns 1 if tile is marked as ship and 0 if it is not.
        public static int GetAboveTile(int characterIndex, int tileNumber)
        {
            return 0;
        }

        public static int GetAboveLeftTile(int characterIndex, int tileNumber)
        {
            //Check upperLeft
            String upperLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + (tileNumber - 1);
            if (ExistShipAt(upperLeftTileId))
            {
                diagonalShip = true;
                shipFlagNumber++;
                MessageBox.Show("Ship exist at: " + upperLeftTileId);
                shipFlagNumber += CheckNextTiles(upperLeftTileId, Direction.UpLeft);
            }

            return 0;
        }

        public static int GetAboveRightTile(int characterIndex, int tileNumber)
        {

            return 0;
        }

        public static int GetLeftTile(int characterIndex, int tileNumber)
        {


            return 0;
        }

        public static int GetRightTile(int characterIndex, int tileNumber)
        {


            return 0;
        }

        public static int GetBelowTile(int characterIndex, int tileNumber)
        {


            return 0;
        }

        public static int GetBelowLeftTile(int characterIndex, int tileNumber)
        {

            return 0;
        }

        public static int GetBelowRightTile(int characterIndex, int tileNumber)
        {

            return 0;
        }
    }
}
