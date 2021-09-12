using System;

namespace Battleship
{
    public class  Ship
    {
        public Tuple<int,int> ShipPosition {get; set;}

        public int ShipSize {get; set;}

        public ShipLayout Layout {get; set;}

        public ShipStatus Status {get; private set;}


        private int _numberOfAttacks = 0;

        public ShipStatus ShipAttacked()
        {
            _numberOfAttacks++;

            if (_numberOfAttacks >= ShipSize)
            {
                this.Status = ShipStatus.Sunken;
            }

            return this.Status;
        }
    }

    public enum ShipLayout
    {
        Horizontal,
        Vertical
    }

    public enum ShipStatus
    {
        Alive,
        Sunken,
        
    }
}
