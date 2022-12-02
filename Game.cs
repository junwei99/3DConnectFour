public class Game
{
    public int xOfBoard = 4;
    public int yOfBoard = 4;
    public int zOfBoard = 4;
    public int[,,] boardState;
    bool player1Turn = false;
    public int playerThatWon = 0;

    //all minimax related logic is stored in this class
    public AIEngine ai;

    public Game(int _x, int _y, int _z)
    {
        xOfBoard = _x;
        yOfBoard = _y;
        zOfBoard = _z;
        boardState = new int[xOfBoard, yOfBoard, zOfBoard];
        ai = new AIEngine(4);
    }

    //update board state based on slot number passed in
    public void takeTurn(int slot)
    {
        if (updateBoardState(slot))
        {
            player1Turn = player1Turn ? false : true;
        }
    }

    //pass in slot to update board state 
    bool updateBoardState(int slot)
    {
        int zCoordinate = (int)(slot / zOfBoard);
        int xCoordinate = slot % xOfBoard;

        for (int yCoordinate = 0; yCoordinate < yOfBoard; yCoordinate++)
        {
            if (boardState[xCoordinate, yCoordinate, zCoordinate] == 0)
            {
                boardState[xCoordinate, yCoordinate, zCoordinate] = player1Turn ? 1 : 2;
                Console.WriteLine("Place being spawned at (" + xCoordinate + "," + yCoordinate + "," + zCoordinate + ")");
                return true;
            }
        }
        return false;
    }

    //pass in x,y,z coordinates to get slot number
    public int getSlotNumFromCoordinates(int x, int y, int z)
    {
        Console.WriteLine($"xyz coordinates {x},{y},{z}");
        // int slotNum = ((x + 1) * (z + 1)) - 1;
        int slotNum = ((z + 1) * 4) - (4 - x);
        Console.WriteLine($"slotNum : {slotNum}");
        return slotNum;
    }

    public bool didSomeoneWonTheGame()
    {
        int winnerValue = ai.getWinnerValue(boardState);

        if (winnerValue != 0 && DebugConfig.shouldGameEndAfterWinningMove)
        {
            playerThatWon = winnerValue;
            return true;
        }

        if (winnerValue != 0 && (DebugConfig.shouldGameEndAfterWinningMove == false)) Console.WriteLine($"DEBUG MODE: {(winnerValue == 1 ? "PLAYER" : "AI")} WON");

        return false;
    }
}
