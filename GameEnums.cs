using System;

namespace Battleship
{
    // Enum holding the possible outcomes of an Attack
    public enum AttackResult
    {
        Miss,
        Hit,
        Sunk
    }

    // Enum holding the possible Game status
    public enum GameStatus
    {
        InProgress,
        Won,

        Lost,

        Tie        
    }
}
