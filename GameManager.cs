public class GameManager
{
    public int xOfBoard = 4;
    public int yOfBoard = 4;
    public int zOfBoard = 4;

    public int[,,] boardState;
    bool player1Turn = true;

    public int playerThatWon = 0;


    public GameManager(int _x, int _y, int _z)
    {
        xOfBoard = _x;
        yOfBoard = _y;
        zOfBoard = _z;
        boardState = new int[xOfBoard, yOfBoard, zOfBoard];
    }

    //update board state based on slot number passed in
    public int[,,] takeTurn(int slot)
    {
        if (updateBoardState(slot))
        {
            player1Turn = player1Turn ? false : true;
        }

        return boardState;
    }

    //get AI best move (xyz coordinates) using minimax
    public Move getAIBestMove()
    {
        int bestScore = int.MinValue;
        Move bestMove = new Move(0, 0, 0);
        int[,,] copyOfBoard = (int[,,])boardState.Clone();

        for (int x = 0; x < xOfBoard; x++)
        {
            for (int z = 0; z < zOfBoard; z++)
            {
                for (int y = 0; y < yOfBoard; y++)
                {
                    if (copyOfBoard[x, y, z] == 0)
                    {
                        copyOfBoard[x, y, z] = 2;
                        int score = minimax(copyOfBoard, 0, false);
                        Console.WriteLine($"score: {score}");
                        copyOfBoard[x, y, z] = 0;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove.setMove(x, y, z);
                        }
                        break;
                    }
                }
            }
        }

        return bestMove;
    }

    int minimax(int[,,] board, int depth, bool isMaximizing)
    {
        // var scoresDict = new Dictionary<int, int>(){
        //     {1, 10},
        //     {2, -10},
        //     {3, 0}
        // };

        // int result = winningConditionCheck();
        // if (result != 0)
        // {
        //     return scoresDict[result];
        // }

        // if (isMaximizing)
        // {
        //     int bestScore = int.MinValue;

        //     for (int x = 0; x < xOfBoard; x++)
        //     {
        //         for (int z = 0; z < zOfBoard; z++)
        //         {
        //             for (int y = 0; y < yOfBoard; y++)
        //             {
        //                 if (board[x, y, z] == 0)
        //                 {
        //                     board[x, y, z] = 2;
        //                     int score = minimax(board, depth + 1, false);
        //                     board[x, y, z] = 0;
        //                     bestScore = Math.Max(score, bestScore);
        //                     break;
        //                 }
        //             }
        //         }
        //     }
        //     return bestScore;
        // }
        // else
        // {
        //     int bestScore = int.MaxValue;

        //     for (int x = 0; x < xOfBoard; x++)
        //     {
        //         for (int z = 0; z < zOfBoard; z++)
        //         {
        //             for (int y = 0; y < yOfBoard; y++)
        //             {
        //                 if (board[x, y, z] == 0)
        //                 {
        //                     board[x, y, z] = 1;
        //                     int score = minimax(board, depth + 1, true);
        //                     board[x, y, z] = 0;
        //                     bestScore = Math.Min(score, bestScore);
        //                     break;
        //                 }
        //             }
        //         }
        //     }
        //     return bestScore;
        // }
        return 1;
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

    //pass in x,y,z coordinates to get slot number
    public int getSlotNumFromCoordinates(int x, int y, int z)
    {
        int slotNum = ((x + 1) * (z + 1)) - 1;

        return slotNum;
    }

    //check if there is winner, 0 for game continues without winner, 1 for player 1 won, 2 for player 2 won
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

    //check if there is any horizontal checking winning condition matched, return 0 for game continues without winner, 1 for player 1 won, 2 for player 2 won
    int horizontalChecking()
    {
        for (int y = 0; y < yOfBoard; y++)
        {
            for (int z = 0; z < zOfBoard; z++)
            {
                int a = boardState[0, y, z];
                int b = boardState[1, y, z];
                int c = boardState[2, y, z];
                int d = boardState[3, y, z];

                if (a != 0 && a == b && a == c && a == d)
                {
                    return a;
                }
            }
        }
        return 0;
    }
}
