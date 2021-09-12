using System;

namespace Battleship
{
    // This class represents the Board State
    // Number of ships and their position on the board
    // Attack positions
    // Number of ships remaining on the board
    public class BattleshipBoardState
    {
        // This is an interface for a board setup. Implementer may chose to have a fixed or dynamic board setup along with special validations
        public IBoardSetup boardSetup {get; }

        public Ship[] ships 
        {
            get 
            {
                return boardSetup.Ships;
            }
        }

        // Two dimensional array represents the board  grid
        // Each element in the array either holds a null refreence or the reference of ship object
        public Ship[,] ShipPositions {get; set;}

        // Two dimensional array represents the board grid with attacked positions
        // If a position is attacked, true will be stored in the respective array position
        public bool[,] AttackPositions {get; set;}

        // Calculated property retruning number of ships remaining/alive on the board
        public int NumberOfShipRemaining 
        {
            get
            {
                return GetNumberOfShipsRemaining();
            }
        }


        // Calculated property retruning whether all ships are sunk or not
        public bool AllShipSunk
        {
            get
            {
                return AreAllShipsSunk();
            }
        }
        
        public BattleshipBoardState(IBoardSetup boardSetup)
        {
            this.boardSetup = boardSetup;

            this.AttackPositions = new bool[boardSetup.BoardSize.Item1,boardSetup.BoardSize.Item2];

        }

        private bool AreAllShipsSunk()
        {
            foreach(var ship in ships)
            {
                if (ship.Status == ShipStatus.Alive)
                {
                    return false;
                }
            }

            return true;
        }


        private int GetNumberOfShipsRemaining()
        {
            int count = 0;
            foreach(var ship in ships)
            {
                if (ship.Status == ShipStatus.Alive)
                {
                    count++;
                }
            }
            return count;
        }


    }
}
