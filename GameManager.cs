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
        // int bestScore = int.MinValue;
        float bestScore = float.NegativeInfinity;
        Move bestMove = new Move(0, 0, 0);
        int maxSearchDepth = 4;
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
                        // int score = minimax(copyOfBoard, 0, false);
                        float score = newMinimax(copyOfBoard, maxSearchDepth, false);
                        Console.WriteLine($"score {score}, move {x},{y},{z}");
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

    float newMinimax(int[,,] board, int searchDepth, bool isMaximizer)
    {
        float bestScore = 0;
        int winner = isThereAWinner(board);

        if (searchDepth == 0 || winner != 0)
        {
            if (winner != 0)
            {
                return winner == 1 ? (-10000000 * searchDepth) : (10000000 * searchDepth);
            }
            else
            {
                bestScore = evaluateBoard(board, isMaximizer);
                return bestScore;
            }
        }

        if (isMaximizer)
        {
            bestScore = float.NegativeInfinity;

            for (int x = 0; x < xOfBoard; x++)
            {
                for (int z = 0; z < zOfBoard; z++)
                {
                    for (int y = 0; y < yOfBoard; y++)
                    {
                        if (board[x, y, z] == 0)
                        {
                            board[x, y, z] = 2;
                            float score = newMinimax(board, searchDepth - 1, false);
                            board[x, y, z] = 0;
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
            bestScore = float.PositiveInfinity;

            for (int x = 0; x < xOfBoard; x++)
            {
                for (int z = 0; z < zOfBoard; z++)
                {
                    for (int y = 0; y < yOfBoard; y++)
                    {
                        if (board[x, y, z] == 0)
                        {
                            board[x, y, z] = 1;
                            float score = newMinimax(board, searchDepth - 1, true);
                            board[x, y, z] = 0;
                            bestScore = Math.Min(score, bestScore);
                            break;
                        }
                    }
                }
            }

            return bestScore;
        }

    }

    float evaluateBoard(int[,,] currentBoard, bool isMaximizer)
    {
        float boardScore = 0;
        boardScore = boardScore + horizontalChecking(currentBoard, isMaximizer);
        boardScore = boardScore + verticalChecking(currentBoard, isMaximizer);
        boardScore = boardScore + diagonalCheck(currentBoard, isMaximizer);
        return boardScore;
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
        Console.WriteLine($"xyz coordinates {x},{y},{z}");
        // int slotNum = ((x + 1) * (z + 1)) - 1;

        int slotNum = ((z + 1) * 4) - (4 - x);

        Console.WriteLine($"slotNum : {slotNum}");

        return slotNum;
    }


    //check if there is any horizontal checking winning condition matched, return 0 for game continues without winner, 1 for player 1 won, 2 for player 2 won
    float horizontalChecking(int[,,] currentBoard, bool isMaximizer)
    {
        float score = 0;

        for (int i = 0; i < 2; i++)
        {
            for (int y = 0; y < yOfBoard; y++)
            {
                for (int z = 0; z < zOfBoard; z++)
                {
                    int a;
                    int b;
                    int c;
                    int d;

                    if (i == 0)
                    {
                        a = currentBoard[0, y, z];
                        b = currentBoard[1, y, z];
                        c = currentBoard[2, y, z];
                        d = currentBoard[3, y, z];
                    }
                    else
                    {
                        a = currentBoard[z, y, 0];
                        b = currentBoard[z, y, 1];
                        c = currentBoard[z, y, 2];
                        d = currentBoard[z, y, 3];
                    }

                    //all 4 is checked
                    if (a != 0 && a == b && a == c && a == d)
                    {
                        if (isMaximizer)
                        {
                            score = score + ((a == 1) ? -1000 : 1000);
                        }
                        else
                        {
                            score = score + ((a == 2) ? 1000 : -1000);
                        }
                    }

                    //3 out of 4 checked
                    if ((b != 0) && (a == b && a == c && d == 0) || (a == 0 && b == c && b == d))
                    {

                        int compareValue = (a != 0) ? a : b;

                        if (isMaximizer)
                        {
                            score = score + ((compareValue == 1) ? -5 : 5);
                        }
                        else
                        {
                            score = score + ((compareValue == 2) ? 5 : -5);
                        }
                    }

                    //2 out of 4 checked
                    if ((a != 0 && a == b && c == 0 && d == 0) || ((c != 0) && (c == d && a == 0 && b == 0) || (b == c && a == 0 && d == 0)))
                    {
                        int compareValue = (a != 0) ? a : c;
                        if (isMaximizer)
                        {
                            score = score + ((compareValue == 1) ? -1 : 1);
                        }
                        else
                        {
                            score = score + ((compareValue == 2) ? 1 : -1);
                        }
                    }

                    //1 checked in middle (advantageous spot)
                    if ((z == 1 || z == 2) && ((b != 0 && a == 0 && c == 0 && d == 0) || (c != 0 && a == 0 && b == 0 && d == 0)))
                    {
                        int compareValue = (b != 1) ? b : c;
                        if (isMaximizer)
                        {
                            score = score + ((compareValue == 1) ? -3 : 3);
                        }
                        else
                        {
                            score = score + ((compareValue == 2) ? 3 : -3);
                        }
                    }

                }
            }
        }
        return score;
    }

    float verticalChecking(int[,,] currentBoard, bool isMaximizer)
    {
        float score = 0;

        for (int x = 0; x < xOfBoard; x++)
        {
            for (int z = 0; z < zOfBoard; z++)
            {
                int a = currentBoard[x, 0, z];
                int b = currentBoard[x, 1, z];
                int c = currentBoard[x, 2, z];
                int d = currentBoard[x, 3, z];

                if (a != 0)
                {
                    //all 4 is checked
                    if (a == b && a == c && a == d)
                    {
                        if (isMaximizer)
                        {
                            score = score + ((a == 1) ? -1000 : 1000);
                        }
                        else
                        {
                            score = score + ((a == 2) ? 1000 : -1000);
                        }
                    }

                    //3 is checked
                    if (a == b && a == c && d == 0)
                    {
                        if (isMaximizer)
                        {
                            score = score + ((a == 1) ? -5 : 5);
                        }
                        else
                        {
                            score = score + ((a == 2) ? 5 : -5);
                        }
                    }

                    //2 out of 4 checked
                    if (a == b && c == 0 && d == 0)
                    {
                        if (isMaximizer)
                        {
                            score = score + ((a == 1) ? -1 : 1);
                        }
                        else
                        {
                            score = score + ((a == 2) ? 1 : -1);
                        }
                    }
                }

            }
        }
        return score;
    }

    float diagonalCheck(int[,,] currentBoard, bool isMaximizer)
    {
        float score = 0;

        for (int y = 0; y < yOfBoard; y++)
        {

            int a = currentBoard[3, y, 0];
            int b = currentBoard[2, y, 1];
            int c = currentBoard[1, y, 2];
            int d = currentBoard[0, y, 3];

            int e = currentBoard[0, y, 3];
            int f = currentBoard[1, y, 2];
            int g = currentBoard[2, y, 1];
            int h = currentBoard[3, y, 0];

            if ((a != 0 && (a == b && a == c && a == d)) || (e != 0 && (e == f && e == g && e == h)))
            {
                int compareValue = (a != 0) ? a : e;
                if (isMaximizer)
                {
                    score = score + ((compareValue == 1) ? -1000 : 1000);
                }
                else
                {
                    score = score + ((compareValue == 2) ? 1000 : -1000);
                }
            }
        }

        return score;
    }

    int isThereAWinner(int[,,] currentBoard)
    {
        //horizontal check
        for (int i = 0; i < 2; i++)
        {
            for (int y = 0; y < yOfBoard; y++)
            {
                for (int xOrZ = 0; xOrZ < xOfBoard; xOrZ++)
                {
                    int a;
                    int b;
                    int c;
                    int d;
                    //if i is 0, xOrZ will represent x
                    if (i == 0)
                    {
                        a = currentBoard[xOrZ, y, 0];
                        b = currentBoard[xOrZ, y, 1];
                        c = currentBoard[xOrZ, y, 2];
                        d = currentBoard[xOrZ, y, 3];
                    }
                    //if i is 1, xOrZ will represent z
                    else
                    {
                        a = currentBoard[0, y, xOrZ];
                        b = currentBoard[1, y, xOrZ];
                        c = currentBoard[2, y, xOrZ];
                        d = currentBoard[3, y, xOrZ];
                    }

                    if ((a != 0) && (a == b && a == c && a == d))
                    {
                        return a;
                    }
                }
            }
        }

        //vertical check
        for (int x = 0; x < xOfBoard; x++)
        {
            for (int z = 0; z < zOfBoard; z++)
            {
                int a = currentBoard[x, 0, z];
                int b = currentBoard[x, 1, z];
                int c = currentBoard[x, 2, z];
                int d = currentBoard[x, 3, z];

                if ((a != 0) && (a == b && a == c && a == d))
                {
                    return a;
                }
            }
        }

        //diagonal check
        for (int y = 0; y < yOfBoard; y++)
        {
            int a = currentBoard[3, y, 0];
            int b = currentBoard[2, y, 1];
            int c = currentBoard[1, y, 2];
            int d = currentBoard[0, y, 3];

            int e = currentBoard[0, y, 3];
            int f = currentBoard[1, y, 2];
            int g = currentBoard[2, y, 1];
            int h = currentBoard[3, y, 0];

            if ((a != 0 && (a == b && a == c && a == d)) || (e != 0 && (e == f && e == g && e == h)))
            {
                return (a != 0) ? a : e;
            }
        }

        return 0;
    }

}
