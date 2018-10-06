using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    public class Player
    {
        private int availableOneFlagShips;
        private int availableTwoFlagShips;
        private int availableThreeFlagShips;
        private int availableFourFlagShips;


        public Player()
        {
            this.availableOneFlagShips = GameConfiguration.OneFlagShipsCount;
            this.availableTwoFlagShips = GameConfiguration.TwoFlagShipsCount;
            this.availableThreeFlagShips = GameConfiguration.ThreeFlagShipsCount;
            this.availableFourFlagShips = GameConfiguration.FourFlagShipsCount;
        }

        public void DecreaseAvailableShips(ShipType shipType)
        {
            switch (shipType)
            {
                case ShipType.OneFlag:
                    this.availableOneFlagShips -= (this.availableOneFlagShips != 0 ? 1 : 0);
                    break;
                case ShipType.TwoFlag:
                    this.availableTwoFlagShips -= (this.availableTwoFlagShips != 0 ? 1 : 0);
                    break;
                case ShipType.ThreeFlag:
                    this.availableThreeFlagShips -= (this.availableThreeFlagShips != 0 ? 1 : 0);
                    break;
                case ShipType.FourFlag:
                    this.availableFourFlagShips -= (this.availableFourFlagShips != 0 ? 1 : 0);
                    break;
            }
        }

        public int GetAvailableShips(ShipType shipType)
        {
            switch (shipType)
            {
                case ShipType.OneFlag:
                    return this.availableOneFlagShips; 
                case ShipType.TwoFlag:
                    return this.availableTwoFlagShips;
                case ShipType.ThreeFlag:
                    return this.availableThreeFlagShips;
                case ShipType.FourFlag:
                    return this.availableFourFlagShips;
            }
            return 0;
        }

        public bool HasAvailableShips()
        {
            if (this.availableFourFlagShips != 0 || this.availableThreeFlagShips != 0
                || this.availableTwoFlagShips != 0 || this.availableOneFlagShips != 0)
                return true;
            return false;
        }

        public void AddShip(int shipFlagNumber)
        {
            if(shipFlagNumber == 1)
            {
                this.DecreaseAvailableShips(ShipType.OneFlag);
            }
            else if(shipFlagNumber == 2)
            {
                this.availableOneFlagShips++;
                this.DecreaseAvailableShips(ShipType.TwoFlag);
            }
            else if (shipFlagNumber == 3)
            {
                this.availableTwoFlagShips++;
                this.DecreaseAvailableShips(ShipType.ThreeFlag);
            }
            else if (shipFlagNumber == 4)
            {
                this.availableThreeFlagShips++;
                this.DecreaseAvailableShips(ShipType.FourFlag);
            }
        }
    }
}
