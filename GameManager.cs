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
        int maxSearchDepth = 5;
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

        if (searchDepth == 0)
        {
            bestScore = evaluateBoard(board, isMaximizer);
            return bestScore;
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

        for (int y = 0; y < yOfBoard; y++)
        {
            for (int z = 0; z < zOfBoard; z++)
            {
                int a = currentBoard[0, y, z];
                int b = currentBoard[1, y, z];
                int c = currentBoard[2, y, z];
                int d = currentBoard[3, y, z];

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

                    //3 out of 4 checked
                    if ((a == b && a == c && d == 0) || (a == 0 && b == c && b == d))
                    {
                        if (a != 0)
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
                        else
                        {
                            if (isMaximizer)
                            {
                                score = score + ((b == 1) ? -5 : 5);
                            }
                            else
                            {
                                score = score + ((b == 2) ? 5 : -5);
                            }
                        }
                    }

                    //2 out of 4 checked
                    if ((a == b && c == 0 && d == 0) || (c == d && a == 0 && b == 0) || (b == c && a == 0 && d == 0))
                    {
                        if (a != 0)
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
                        else if (c != 0)
                        {
                            if (isMaximizer)
                            {
                                score = score + ((c == 1) ? -1 : 1);
                            }
                            else
                            {
                                score = score + ((c == 2) ? 1 : -1);
                            }
                        }
                    }
                }

            }
        }

        return score;
    }
}
