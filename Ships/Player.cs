using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    public class Player
    {
        private readonly List<Ship> oneFlagShips = new List<Ship>(GameConfiguration.OneFlagShipsCount);
        private readonly List<Ship> twoFlagShips = new List<Ship>(GameConfiguration.TwoFlagShipsCount);
        private readonly List<Ship> threeFlagShips = new List<Ship>(GameConfiguration.ThreeFlagShipsCount);
        private readonly List<Ship> fourFlagShips = new List<Ship>(GameConfiguration.FourFlagShipsCount);

        public Player()
        {

        }

        //public void DecreaseAvailableShips(ShipType shipType)
        //{
        //    switch (shipType)
        //    {
        //        case ShipType.OneFlag:
        //            this.availableOneFlagShips -= (this.availableOneFlagShips != 0 ? 1 : 0);
        //            break;
        //        case ShipType.TwoFlag:
        //            this.availableTwoFlagShips -= (this.availableTwoFlagShips != 0 ? 1 : 0);
        //            break;
        //        case ShipType.ThreeFlag:
        //            this.availableThreeFlagShips -= (this.availableThreeFlagShips != 0 ? 1 : 0);
        //            break;
        //        case ShipType.FourFlag:
        //            this.availableFourFlagShips -= (this.availableFourFlagShips != 0 ? 1 : 0);
        //            break;
        //    }
        //}

        public int GetAvailableShips(ShipType shipType)
        {
            switch (shipType)
            {
                case ShipType.OneFlag:
                    return (GameConfiguration.OneFlagShipsCount - this.oneFlagShips.Count); 
                case ShipType.TwoFlag:
                    return (GameConfiguration.TwoFlagShipsCount - this.twoFlagShips.Count);
                case ShipType.ThreeFlag:
                    return (GameConfiguration.ThreeFlagShipsCount - this.threeFlagShips.Count);
                case ShipType.FourFlag:
                    return (GameConfiguration.FourFlagShipsCount - this.fourFlagShips.Count);
            }
            return 0;
        }

        public bool HasAvailableShips()
        {
            if (GameConfiguration.OneFlagShipsCount > this.oneFlagShips.Count)
            {
                return true;
            }
            
            if(GameConfiguration.TwoFlagShipsCount > this.twoFlagShips.Count)
            {
                return true;
            }

            if(GameConfiguration.ThreeFlagShipsCount > this.threeFlagShips.Count)
            {
                return true;
            }

            if(GameConfiguration.FourFlagShipsCount > this.fourFlagShips.Count)
            {
                return true;
            }

            return false;
        }

        public Ship GetShip(List<string> tileIds)
        {
            foreach(Ship s in this.oneFlagShips)
            {
                foreach(string tileid in tileIds)
                {
                    if (s.GetTilesIds().Contains(tileid))
                        return s;
                }
            }
            foreach(Ship s in this.twoFlagShips)
            {
                foreach (string tileid in tileIds)
                {
                    if (s.GetTilesIds().Contains(tileid))
                        return s;
                }
            }
            foreach(Ship s in this.threeFlagShips)
            {
                foreach (string tileid in tileIds)
                {
                    if (s.GetTilesIds().Contains(tileid))
                        return s;
                }
            }
            foreach(Ship s in this.fourFlagShips)
            {
                foreach (string tileid in tileIds)
                {
                    if (s.GetTilesIds().Contains(tileid))
                        return s;
                }
            }
            return null;
        }

        public void AddShip(Ship ship)
        {
            switch (ship.GetShipType())
            {
                case ShipType.OneFlag:
                    this.oneFlagShips.Add(ship);
                    break;
                case ShipType.TwoFlag:
                    this.twoFlagShips.Add(ship);
                    break;
                case ShipType.ThreeFlag:
                    this.twoFlagShips.Add(ship);
                    break;
                case ShipType.FourFlag:
                    this.fourFlagShips.Add(ship);
                    break;
            }
        }

        /**
         * Returns a ship if it exists at given tile
         * otherwise returns null.
         */
        public Ship HasShipAt(String tileId)
        {
            //Check one-flag ships
            foreach(Ship ship in oneFlagShips)
            {
                if (ship.GetTilesIds().Contains(tileId))
                    return ship;
            }

            //Check two-flag ships
            foreach(Ship ship in twoFlagShips)
            {
                if (ship.GetTilesIds().Contains(tileId))
                    return ship;
            }

            //Check three-flag ships
            foreach(Ship ship in threeFlagShips)
            {
                if (ship.GetTilesIds().Contains(tileId))
                    return ship;
            }

            //Check four-flag ships
            foreach(Ship ship in fourFlagShips)
            {
                if (ship.GetTilesIds().Contains(tileId))
                    return ship;
            }

            return null;
        }

        public void RemoveShip(Ship ship)
        {
            if (this.oneFlagShips.Contains(ship))
                this.oneFlagShips.Remove(ship);
            else if (this.twoFlagShips.Contains(ship))
                this.twoFlagShips.Remove(ship);
            else if (this.threeFlagShips.Contains(ship))
                this.threeFlagShips.Remove(ship);
            else if (this.fourFlagShips.Contains(ship))
                this.fourFlagShips.Remove(ship);
        }
    }
}
