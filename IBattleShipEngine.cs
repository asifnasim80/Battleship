using System;

namespace Battleship
{
    // This is the main interface which to initialize and control the game play
    // Implementers may chose to perform variations to the standard gameplay
    public interface IBattleShipEngine
    {
        void InitializeBoard(IBoardSetup boardSetup);

        AttackResult Attack(int positionX, int positionY);

        GameStatus GetGameStatus();

        BattleshipBoardState GetBoardState();
    }


    // Default implementation of the BattleShipengine interface
    public class BattleshipEngine: IBattleShipEngine
    {
        private BattleshipBoardState boardState;
        public void InitializeBoard(IBoardSetup boardSetup)
        {
            if (boardSetup == null)
            {
                throw new ArgumentNullException("boardSetup");
            }
            this.boardState = new BattleshipBoardState(boardSetup);

            this.boardState.ShipPositions = this.InitializeShipsPositions();
        }

        // More validations to be done. For the purpose of this task just a basic attack process is coded
        public AttackResult Attack(int positionX, int positionY)
        {
            // Place the attack marker
            // More validation to be done to make sure attack position is unique and not previously used
            this.boardState.AttackPositions[positionX,positionY] = true;

            // Check if the attack position also has a ship on the grid
            if (this.boardState.ShipPositions[positionX, positionY] != null)
            {
                var ship = this.boardState.ShipPositions[positionX, positionY];

                // Update the ship object that it is attacked
                var shipStatus = ship.ShipAttacked();

                // Check the ship status after attack
                if(shipStatus == ShipStatus.Alive)
                {
                    // Return that the ship is hit but still alive
                    return AttackResult.Hit;
                }
                else if(shipStatus == ShipStatus.Sunken)
                {
                    // Ship is sunken as it has been hit by adequate number of times already
                    return AttackResult.Sunk;
                }                
            }

            // Ship is missed
            return AttackResult.Miss;
        }

        public GameStatus GetGameStatus()
        {
            if (boardState.AllShipSunk)
            {
                return GameStatus.Lost;
            }

            return GameStatus.InProgress;

            //This code needs to have second player boardState to determine Won and Tie status
            // To do that code needs to be modified to add another object of BoardState and maintain the state of that object as well
            // The ask is to only maintain single player board state, therefore it is not possible to determine Won or Tie status
        }

        public BattleshipBoardState GetBoardState()
        {
            return this.boardState;
        }

        


        // This method will initialize the board state for a single player
        // Some basic validations such as Ship overlapping are preformed as requested for the task
        // There needs to be more comprehensive validations
        private Ship[,] InitializeShipsPositions()
        {
            //Initialize a local array to hold ship objects in a grid
            Ship[,] shipPositions = new Ship[boardState.boardSetup.BoardSize.Item1,boardState.boardSetup.BoardSize.Item2];

            foreach(var ship in boardState.ships)
            {
                int shipPositionStartX = ship.ShipPosition.Item1;
                int shipPositionStartY = ship.ShipPosition.Item2;

                for(int i=0; i<ship.ShipSize; i++)
                {
                    int shipPositionX;
                    int shipPositionY;
                    if (ship.Layout == ShipLayout.Horizontal)
                    {
                        shipPositionX = shipPositionStartX + i;
                        shipPositionY = shipPositionStartY;
                    }
                    else
                    {
                        shipPositionX = shipPositionStartX;
                        shipPositionY = shipPositionStartY + i;                   
                    }
                    
                    Ship prvShip = shipPositions[shipPositionX,shipPositionY];
                    if (prvShip == null) // This must always be null as all the validatoins for overlapping is performed within the BoardSetup class
                    {
                        shipPositions[shipPositionX,shipPositionY] = ship;
                    }
                }                
            }

            return shipPositions;

        }

    }
}
