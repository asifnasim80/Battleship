using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize a few ships to simulate a gameplay
            List<Ship> ships = new List<Ship>();
            ships.Add(new Ship {ShipPosition = new Tuple<int, int>(0,0), Layout = ShipLayout.Horizontal, ShipSize = 5});
            ships.Add(new Ship {ShipPosition = new Tuple<int, int>(1,1), Layout = ShipLayout.Vertical, ShipSize = 5});
            
            //Uncomment following line to receive overlapping validation exceptioin
            //ships.Add(new Ship {ShipPosition = new Tuple<int, int>(0,0), Layout = ShipLayout.Horizontal, ShipSize = 2});

            //Uncomment following line to receive ship sizing validation exceptioin
            //ships.Add(new Ship {ShipPosition = new Tuple<int, int>(0,7), Layout = ShipLayout.Horizontal, ShipSize = 11});


            // Initialize board setup of 10x10 grid and ships
            IBoardSetup boardSetup = new BoardSetup(new Tuple<int, int>(10,10), ships.ToArray());
            
            // Initialize the gaming  engine
            // This implements an interface and in the actual host (.Net Web App) this will be injected via Dependency injection
            IBattleShipEngine engine = new BattleshipEngine();
            engine.InitializeBoard(boardSetup);

            // Simulate a couple of attacks and writing the status
            var attackResult = engine.Attack(0,0);
            Console.WriteLine(attackResult);
            attackResult = engine.Attack(1,0);
            Console.WriteLine(attackResult);
            attackResult = engine.Attack(2,0);
            Console.WriteLine(attackResult);
            attackResult = engine.Attack(3,0);
            Console.WriteLine(attackResult);
            attackResult = engine.Attack(4,0);
            Console.WriteLine(attackResult);


            attackResult = engine.Attack(9,0);
            Console.WriteLine(attackResult);

            // Write  game status
            var gameStatus = engine.GetGameStatus();
            Console.WriteLine(gameStatus);
        }
    }
}
