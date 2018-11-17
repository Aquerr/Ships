using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    public class Ship
    {
        private readonly List<string> flagTilesIds = new List<string>();
        private readonly ShipType shipType;

        public Ship(List<string> flagTilesIds)
        {
            switch (flagTilesIds.Count)
            {
                case 1:
                    this.shipType = ShipType.OneFlag;
                    break;
                case 2:
                    this.shipType = ShipType.TwoFlag;
                    break;
                case 3:
                    this.shipType = ShipType.ThreeFlag;
                    break;
                case 4:
                    this.shipType = ShipType.FourFlag;
                    break;
            }
            this.flagTilesIds = flagTilesIds;
        }

        public List<string> GetTilesIds()
        {
            return this.flagTilesIds;
        }

        public ShipType GetShipType()
        {
            return this.shipType;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if(this == obj)
            {
                return true;
            }
            if (!this.shipType.Equals(((Ship)obj).GetShipType()))
            {
                return false;
            }
            if (!this.flagTilesIds.Equals(((Ship)obj).GetTilesIds()))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + this.shipType.GetHashCode();
            result = prime * result + (this.flagTilesIds != null ? this.flagTilesIds.GetHashCode() : 0);
            return result;
        }
    }
}
