public class GameManager
{
    public int xOfBoard = 4;
    public int yOfBoard = 4;
    public int zOfBoard = 4;

    public int[,,] boardState;

    bool player1Turn = true;

    //0 for no player win yet, 1 for player 1, 2 for player 2
    public int playerThatWon = 0;

    public GameManager(int _x, int _y, int _z)
    {
        xOfBoard = _x;
        yOfBoard = _y;
        zOfBoard = _z;
        boardState = new int[xOfBoard, yOfBoard, zOfBoard];
    }

    public int[,,] takeTurn(int slot)
    {
        if (updateBoardState(slot))
        {
            player1Turn = player1Turn ? false : true;
        }

        return boardState;
    }

    public Move getAIBestMove()
    {
        double bestScore = double.NegativeInfinity;
        Move bestMove = new Move(0, 0, 0);
        int numOfLoops = 0;

        int[,,] copyOfBoard = (int[,,])boardState.Clone();

        for (int x = 0; x < boardState.GetLength(0); x++)
        {
            for (int z = 0; z < boardState.GetLength(2); z++)
            {
                for (int y = 0; y < boardState.GetLength(1); y++)
                {
                    if (boardState[x, y, z] == 0)
                    {
                        copyOfBoard[x, y, z] = 2;
                        int score = minimax(copyOfBoard, 0, false);
                        copyOfBoard[x, y, z] = 0;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove.setMove(x, y, z);
                        }
                        numOfLoops = numOfLoops + 1;
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"num of loops = {numOfLoops}");

        return bestMove;
    }

    int minimax(int[,,] boardState, int depth, bool isMaximizing)
    {
        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int x = 0; x < boardState.GetLength(0); x++)
            {
                for (int z = 0; z < boardState.GetLength(2); z++)
                {
                    for (int y = 0; y < boardState.GetLength(1); y++)
                    {
                        if (boardState[x, y, z] == 0)
                        {
                            int[,,] copyOfBoard = (int[,,])boardState.Clone();
                            copyOfBoard[x, y, z] = 2;
                            int score = minimax(copyOfBoard, depth + 1, false);
                            copyOfBoard[x, y, z] = 0;
                            bestScore = Math.Max(score, bestScore);
                            break;
                        }
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int x = 0; x < boardState.GetLength(0); x++)
            {
                for (int z = 0; z < boardState.GetLength(2); z++)
                {
                    for (int y = 0; y < boardState.GetLength(1); y++)
                    {
                        if (boardState[x, y, z] == 0)
                        {
                            int[,,] copyOfBoard = (int[,,])boardState.Clone();
                            copyOfBoard[x, y, z] = 1;
                            int score = minimax(copyOfBoard, depth + 1, true);
                            copyOfBoard[x, y, z] = 0;
                            bestScore = Math.Min(score, bestScore);
                            break;
                        }
                    }
                }
            }
            return bestScore;
        }
    }

    bool updateBoardState(int slot)
    {
        int zCoordinate = (int)(slot / zOfBoard);
        int xCoordinate = slot % xOfBoard;

        for (int yCoordinate = 0; yCoordinate < yOfBoard; yCoordinate++)
        {
            if (boardState[xCoordinate, yCoordinate, zCoordinate] == 0)
            {
                if (player1Turn)
                {
                    boardState[xCoordinate, yCoordinate, zCoordinate] = 1;
                }
                else
                {
                    boardState[xCoordinate, yCoordinate, zCoordinate] = 2;
                }
                Console.WriteLine("Place being spawned at (" + xCoordinate + "," + yCoordinate + "," + zCoordinate + ")");
                return true;
            }
        }
        return false;
    }

    public int getSlotNumFromCoordinates(int x, int y, int z)
    {
        int slotNum = ((x + 1) * (z + 1)) - 1;

        return slotNum;
    }

    public int winningConditionCheck()
    {
        int horizontalWin = horizontalChecking();
        // int verticalWin = false;
        // int diagonalWin = false;

        if (horizontalWin != 0)
        {
            return horizontalWin;
        }

        return 0;
    }

    int horizontalChecking()
    {
        for (int y = 0; y < yOfBoard; y++)
        {
            for (int z = 0; z < zOfBoard; z++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (boardState[x, y, z] != 0)
                    {
                        int a = boardState[x, y, z];
                        int b = boardState[x, y + 1, z];
                        int c = boardState[x, y + 2, z];
                        int d = boardState[x, y + 3, z];

                        if (a == b && a == c && a == d)
                        {
                            return a;
                        }
                    }
                }
            }
        }
        return 0;
    }
}
