using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ships
{
    public class Utils
    {
        public static List<string> GetTiles(String currentTileId, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            String character = currentTileId.Substring(0, 1);
            int characterIndex = Array.IndexOf(GameConfiguration.GridCharacters, character);
            int id = int.Parse(currentTileId.Substring(1));

            //Check if tiles exist in other directions than wanted one
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (dir == checkingFrom)
                    continue;

                List<string> tilesInDirection = GetTilesInDirection(characterIndex, id, dir);
                if (GetOppositeDirection(checkingFrom) != dir && tilesInDirection != null && tilesInDirection.Count > 0)
                    return null;
            }

            return GetTilesInDirection(characterIndex, id, GetOppositeDirection(checkingFrom));
        }

        public static List<string> GetAboveTile(int characterIndex, int tileNumber, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            int aboveTileCharacterIndex = characterIndex;
            int aboveTileNumber = tileNumber - 1;
            if (!IsValidIndex(aboveTileCharacterIndex, aboveTileNumber))
                return tiles;

            String aboveTileId = GameConfiguration.GridCharacters[aboveTileCharacterIndex] + (aboveTileNumber);
            if (ExistShipAt(aboveTileId))
            {
                tiles.Add(aboveTileId);
                List<string> aboveTiles = GetTiles(aboveTileId, checkingFrom);
                if (aboveTiles == null)
                    return null;
                tiles.AddRange(aboveTiles);
                return tiles;
             }
            else
            {
                return new List<string>();
            }
        }

        public static List<string> GetAboveLeftTile(int characterIndex, int tileNumber, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            int aboveLeftTileCharacterIndex = characterIndex - 1;
            int aboveLeftTileNumber = tileNumber - 1;
            if (!IsValidIndex(aboveLeftTileCharacterIndex, aboveLeftTileNumber))
                return tiles;

            String aboveLeftTileId = GameConfiguration.GridCharacters[aboveLeftTileCharacterIndex] + (aboveLeftTileNumber);
            if (ExistShipAt(aboveLeftTileId))
            {
                tiles.Add(aboveLeftTileId);
                List<string> aboveLeftTiles = GetTiles(aboveLeftTileId, checkingFrom);
                if (aboveLeftTiles == null)
                    return null;
                tiles.AddRange(aboveLeftTiles);
                return tiles;
            }
            else
            {
                return new List<string>();
            }
        }

        public static List<string> GetAboveRightTile(int characterIndex, int tileNumber, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            int aboveRightTileCharacterIndex = characterIndex + 1;
            int aboveRightTileNumber = tileNumber - 1;
            if (!IsValidIndex(aboveRightTileCharacterIndex, aboveRightTileNumber))
                return tiles;

            String aboveRightTileId = GameConfiguration.GridCharacters[aboveRightTileCharacterIndex] + (aboveRightTileNumber);
            if (ExistShipAt(aboveRightTileId))
            {
                tiles.Add(aboveRightTileId);
                List<string> aboveRightTiles = GetTiles(aboveRightTileId, checkingFrom);
                if (aboveRightTiles == null)
                    return null;
                tiles.AddRange(aboveRightTiles);
                return tiles;
            }
            else
            {
                return new List<string>();
            }
        }

        public static List<string> GetLeftTile(int characterIndex, int tileNumber, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            int leftTileCharacterIndex = characterIndex - 1;
            int leftTileNumber = tileNumber;
            if (!IsValidIndex(leftTileCharacterIndex, leftTileNumber))
                return tiles;

            String leftTileId = GameConfiguration.GridCharacters[leftTileCharacterIndex] + (leftTileNumber);
            if (ExistShipAt(leftTileId))
            {
                tiles.Add(leftTileId);
                List<string> leftTiles = GetTiles(leftTileId, checkingFrom);
                if (leftTiles == null)
                    return null;
                tiles.AddRange(leftTiles);
                return tiles;
            }
            else
            {
                return new List<string>();
            }
        }

        public static List<string> GetRightTile(int characterIndex, int tileNumber, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            int rightTileCharacterIndex = characterIndex + 1;
            int rightTileNumber = tileNumber;
            if (!IsValidIndex(rightTileCharacterIndex, rightTileNumber))
                return tiles;

            String rightTileId = GameConfiguration.GridCharacters[rightTileCharacterIndex] + (rightTileNumber);
            if (ExistShipAt(rightTileId))
            {
                tiles.Add(rightTileId);
                List<string> rightTiles = GetTiles(rightTileId, checkingFrom);
                if (rightTiles == null)
                    return null;
                tiles.AddRange(rightTiles);
                return tiles;
            }
            else
            {
                return new List<string>();
            }
        }

        public static List<string> GetBelowTile(int characterIndex, int tileNumber, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            int belowTileCharacterIndex = characterIndex;
            int belowTileNumber = tileNumber + 1;
            if (!IsValidIndex(belowTileCharacterIndex, belowTileNumber))
                return tiles;

            String belowTileId = GameConfiguration.GridCharacters[belowTileCharacterIndex] + (belowTileNumber);
            if (ExistShipAt(belowTileId))
            {
                tiles.Add(belowTileId);
                List<string> belowTiles = GetTiles(belowTileId, checkingFrom);
                if (belowTiles == null)
                    return null;
                tiles.AddRange(belowTiles);
                return tiles;
            }
            else
            {
                return new List<string>();
            }
        }

        public static List<string> GetBelowLeftTile(int characterIndex, int tileNumber, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            int belowLeftTileCharacterIndex = characterIndex - 1;
            int belowLeftTileNumber = tileNumber + 1;
            if (!IsValidIndex(belowLeftTileCharacterIndex, belowLeftTileNumber))
                return tiles;

            String belowLeftTileId = GameConfiguration.GridCharacters[belowLeftTileCharacterIndex] + (belowLeftTileNumber);
            if (ExistShipAt(belowLeftTileId))
            {
                tiles.Add(belowLeftTileId);
                List<string> belowLeftTiles = GetTiles(belowLeftTileId, checkingFrom);
                if (belowLeftTiles == null)
                    return null;
                tiles.AddRange(belowLeftTiles);
                return tiles;
            }
            else
            {
                return new List<string>();
            }
        }

        public static List<string> GetBelowRightTile(int characterIndex, int tileNumber, Direction checkingFrom)
        {
            List<string> tiles = new List<string>();
            int belowRightTileCharacterIndex = characterIndex + 1;
            int belowRightTileNumber = tileNumber + 1;
            if (!IsValidIndex(belowRightTileCharacterIndex, belowRightTileNumber))
                return tiles;

            String belowRightTileId = GameConfiguration.GridCharacters[belowRightTileCharacterIndex] + (belowRightTileNumber);
            if (ExistShipAt(belowRightTileId))
            {
                tiles.Add(belowRightTileId);
                List<string> belowRightTiles = GetTiles(belowRightTileId, checkingFrom);
                if (belowRightTiles == null)
                    return null;
                tiles.AddRange(belowRightTiles);
                return tiles;
            }
            else
            {
                return new List<string>();
            }
        }

        public static bool ExistShipAt(String tileId)
        {
            if (Game.PLAYER_GRID_FIELDS.TryGetValue(tileId, out Button tile))
            {
                return tile.Background == Brushes.Gray;
            }
            return false;
        }

        private Utils()
        {

        }

        private static List<string> GetTilesInDirection(int characterIndex, int id, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return GetAboveTile(characterIndex, id, Direction.Down);
                case Direction.UpLeft:
                    return GetAboveLeftTile(characterIndex, id, Direction.DownRight);
                case Direction.UpRight:
                    return GetAboveRightTile(characterIndex, id, Direction.DownLeft);
                case Direction.Left:
                    return GetLeftTile(characterIndex, id, Direction.Right);
                case Direction.Right:
                    return GetRightTile(characterIndex, id, Direction.Left);
                case Direction.Down:
                    return GetBelowTile(characterIndex, id, Direction.Up);
                case Direction.DownLeft:
                    return GetBelowLeftTile(characterIndex, id, Direction.UpRight);
                case Direction.DownRight:
                    return GetBelowRightTile(characterIndex, id, Direction.UpLeft);
            }
            return null;
        }

        private static bool IsValidIndex(int characterIndex, int tileNumber)
        {
            if (characterIndex < 0 || characterIndex > GameConfiguration.GridCharacters.Length - 1)
                return false;
            if (tileNumber < 1 || tileNumber > 10)
                return false;
            return true;
        }

        public static Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.UpLeft:
                    return Direction.DownRight;
                case Direction.UpRight:
                    return Direction.DownLeft;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Up;
                case Direction.DownLeft:
                    return Direction.UpRight;
                case Direction.DownRight:
                    return Direction.UpLeft;
                default:
                    return Direction.Left;
            }
        }
    }
}
