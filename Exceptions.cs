using System;

namespace Battleship
{
    public class BattleShipEngineShipOverlapException: Exception
    {
        public Ship Ship1 {get;}
        public Ship Ship2 {get;}
        public BattleShipEngineShipOverlapException(string message, Ship ship1, Ship ship2): base(message)
        {
            this.Ship1 = ship1;
            this.Ship2 = ship2;
        }
    }


    
    public class BattleShipEngineException: Exception
    {
        public Ship Ship {get;}
        public BattleShipEngineException(string message, Ship ship): base(message)
        {
            this.Ship = ship;
        }
    }
}
