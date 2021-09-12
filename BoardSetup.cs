using System;

namespace Battleship
{
    // This interface defines the contract for BoardSetup
    // Implementers of this interface can have their own special validation rules
    public interface IBoardSetup
    {
        Tuple<int,int> BoardSize{ get; }

        Ship[] Ships {get;}

    }

    // This class holds the data to initialize a board with ships
    public class  BoardSetup: IBoardSetup
    {
        public Tuple<int,int> BoardSize{ get; private set; }

        public Ship[] Ships {get;  private set;}

        public BoardSetup(Tuple<int,int> boardSize, Ship[] ships)
        {
            if (boardSize == null)
            {
                throw new ArgumentNullException("boardSize");
            }
            if(ships == null)
            {
                throw new ArgumentNullException("ships");
            }

            this.BoardSize = boardSize;
            this.Ships = ships;

            ValidateBoardSetup();
        }

       // Kick off validatioin rules
        private void ValidateBoardSetup()
        {            
            ValidateShipandBoardSize();
            ValidateOverlappingShips();            

        }

        // Ship posiiton and size must conforms to the board size
        private void ValidateShipandBoardSize()
        {
            int boardMaxPositionX =  BoardSize.Item1;
            int boardMaxPositionY =  BoardSize.Item2;

            foreach(var ship in this.Ships)
            {
                
                if (ship.Layout == ShipLayout.Horizontal)
                {
                    int shipMaxPositionX = ship.ShipPosition.Item1 + ship.ShipSize;
                    if (shipMaxPositionX > boardMaxPositionX)
                    {
                        throw new BattleShipEngineException("Ship size exceeds board size", ship);
                    }
                    if (ship.ShipPosition.Item2 > boardMaxPositionY)
                    {
                        throw new BattleShipEngineException("Ship size exceeds board size", ship);
                    }

                }
                
                if (ship.Layout == ShipLayout.Vertical)
                {
                    int shipMaxPositionY = ship.ShipPosition.Item2 + ship.ShipSize;
                    if (shipMaxPositionY > boardMaxPositionY)
                    {
                        throw new BattleShipEngineException("Ship size exceeds board size", ship);
                    }
                    if (ship.ShipPosition.Item1 > boardMaxPositionX)
                    {
                        throw new BattleShipEngineException("Ship size exceeds board size", ship);                        
                    }

                }
            }
        }

        // This method will validate the board and ship data to ensure 
        // there is no overlap of ships as required for this task
        private void ValidateOverlappingShips()
        {
            Ship[,] shipPositions = new Ship[BoardSize.Item1, BoardSize.Item2];
            foreach(var ship in this.Ships)
            {
                int startPositionX = ship.ShipPosition.Item1;
                int startPositionY = ship.ShipPosition.Item2;

                for(int i=0; i< ship.ShipSize; i++)
                {                    
                    int positionX = startPositionX;
                    int positionY = startPositionY;

                    if (ship.Layout == ShipLayout.Horizontal)
                    {
                        positionX = startPositionX + i;

                    }
                    else
                    {
                        positionY = startPositionY + i;
                    }
                    Ship prvShip = shipPositions[positionX, positionY];
                    if (prvShip != null)
                    {    
                        throw new BattleShipEngineShipOverlapException("Ship position overlaps with another ship", ship, prvShip);                    
                    }

                    shipPositions[positionX, positionY] = ship;                    
                }
            }

        }

    }
}
