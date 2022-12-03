using System.Collections;
public class AIEngine
{
    public int minimaxMaxDepth = 4;
    public AIEngine(int _minimaxDepth)
    {
        minimaxMaxDepth = _minimaxDepth;
    }

    //get AI best move (xyz coordinates) using minimax
    public Move getAIBestMove(int[,,] currentBoardState)
    {
        // int bestScore = int.MinValue;
        float bestScore = float.NegativeInfinity;
        Move bestMove = new Move(0, 0, 0);
        int[,,] copyOfBoard = (int[,,])currentBoardState.Clone();
        for (int x = 0; x < currentBoardState.GetLength(0); x++)
        {
            for (int z = 0; z < currentBoardState.GetLength(2); z++)
            {
                for (int y = 0; y < currentBoardState.GetLength(1); y++)
                {
                    if (copyOfBoard[x, y, z] == 0)
                    {
                        copyOfBoard[x, y, z] = 2;
                        // int score = minimax(copyOfBoard, 0, false);
                        // float score = minimax(copyOfBoard, minimaxMaxDepth, false);
                        float score = alphaBetaMinimax(copyOfBoard, minimaxMaxDepth, float.NegativeInfinity, float.PositiveInfinity, false);
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

    private float minimax(int[,,] board, int searchDepth, bool isMaximizer)
    {
        float bestScore = 0;
        //for debugging purposes (turn off winning condition in terminal node to ease the debugging process for each method of checking e.g vertical checking or horizontal checking) 
        int winner = getWinnerValue(board);

        if (searchDepth == 0 || winner != 0)
        {
            if (winner != 0)
            {
                return winner == 1 ? -1000000000 : 1000000000;
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

            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int z = 0; z < board.GetLength(2); z++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        if (board[x, y, z] == 0)
                        {
                            board[x, y, z] = 2;
                            float score = minimax(board, searchDepth - 1, false);
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

            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int z = 0; z < board.GetLength(2); z++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        if (board[x, y, z] == 0)
                        {
                            board[x, y, z] = 1;
                            float score = minimax(board, searchDepth - 1, true);
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

    private float alphaBetaMinimax(int[,,] board, int searchDepth, float alpha, float beta, bool isMaximizer)
    {
        float bestScore = 0;
        //for debugging purposes (turn off winning condition in terminal node to ease the debugging process for each method of checking e.g vertical checking or horizontal checking) 
        int winner = getWinnerValue(board);

        if (searchDepth == 0 || winner != 0)
        {
            if (winner != 0)
            {
                return winner == 1 ? -1000000000 : 1000000000;
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
            bool endLoop = false;

            for (int x = 0; (x < board.GetLength(0)) && !endLoop; x++)
            {
                for (int z = 0; (z < board.GetLength(2)) && !endLoop; z++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        if (board[x, y, z] == 0)
                        {
                            board[x, y, z] = 2;
                            float score = alphaBetaMinimax(board, searchDepth - 1, alpha, beta, false);
                            board[x, y, z] = 0;
                            bestScore = Math.Max(score, bestScore);
                            alpha = Math.Max(alpha, bestScore);
                            if (bestScore >= beta) endLoop = true;
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
            bool endLoop = false;

            for (int x = 0; (x < board.GetLength(0)) && !endLoop; x++)
            {
                for (int z = 0; (z < board.GetLength(2)) && !endLoop; z++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        if (board[x, y, z] == 0)
                        {
                            board[x, y, z] = 1;
                            float score = alphaBetaMinimax(board, searchDepth - 1, alpha, beta, true);
                            board[x, y, z] = 0;
                            bestScore = Math.Min(score, bestScore);
                            beta = Math.Min(beta, bestScore);
                            if (bestScore <= alpha) endLoop = true;
                            break;
                        }
                    }
                }
            }
            return bestScore;
        }
    }
    public int getWinnerValue(int[,,] currentBoard)
    {
        //horizontal check
        if (DebugConfig.shouldCheckHorizontal)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int y = 0; y < currentBoard.GetLength(1); y++)
                {
                    for (int xOrZ = 0; xOrZ < currentBoard.GetLength(0); xOrZ++)
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
        }

        if (DebugConfig.shouldCheckVertical)
        {
            //vertical check
            for (int x = 0; x < currentBoard.GetLength(0); x++)
            {
                for (int z = 0; z < currentBoard.GetLength(2); z++)
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
        }

        if (DebugConfig.shouldCheckDiagonal)
        {
            //swap diagonal check positions
            for (int i = 0; i < 10; i++)
            {

                int a = 0;
                int b = 0;
                int c = 0;
                int d = 0;

                if (i > 5 && i < 10)
                {
                    switch (i)
                    {

                        case 6:
                            a = currentBoard[0, 0, 0];
                            b = currentBoard[1, 1, 1];
                            c = currentBoard[2, 2, 2];
                            d = currentBoard[3, 3, 3];
                            //0,0,0 -> 1,1,1 --> 2,2,2 -> 3,3,3
                            break;
                        case 7:
                            a = currentBoard[0, 3, 0];
                            b = currentBoard[1, 2, 1];
                            c = currentBoard[2, 1, 2];
                            d = currentBoard[3, 0, 3];
                            break;
                        case 8:
                            a = currentBoard[3, 0, 0];
                            b = currentBoard[2, 1, 1];
                            c = currentBoard[1, 2, 2];
                            d = currentBoard[0, 3, 3];
                            break;
                        case 9:
                            a = currentBoard[0, 0, 3];
                            b = currentBoard[1, 1, 2];
                            c = currentBoard[2, 2, 1];
                            d = currentBoard[3, 3, 0];
                            break;
                        default:
                            break;
                    }

                    if ((a != 0 && (a == b && a == c && a == d)))
                    {
                        return a;
                    }
                }
                else
                {
                    for (int j = 0; j < currentBoard.GetLength(0); j++)
                    {

                        switch (i)
                        {
                            case 0:
                                a = currentBoard[3, j, 0];
                                b = currentBoard[2, j, 1];
                                c = currentBoard[1, j, 2];
                                d = currentBoard[0, j, 3];
                                break;
                            case 1:
                                a = currentBoard[0, j, 0];
                                b = currentBoard[1, j, 1];
                                c = currentBoard[2, j, 2];
                                d = currentBoard[3, j, 3];
                                break;
                            case 2:
                                a = currentBoard[j, 0, 3];
                                b = currentBoard[j, 1, 2];
                                c = currentBoard[j, 2, 1];
                                d = currentBoard[j, 3, 0];
                                break;
                            case 3:
                                a = currentBoard[j, 0, 0];
                                b = currentBoard[j, 1, 1];
                                c = currentBoard[j, 2, 2];
                                d = currentBoard[j, 3, 3];
                                break;
                            case 4:
                                a = currentBoard[3, 0, j];
                                b = currentBoard[2, 1, j];
                                c = currentBoard[1, 2, j];
                                d = currentBoard[0, 3, j];
                                break;
                            case 5:
                                a = currentBoard[0, 0, j];
                                b = currentBoard[1, 1, j];
                                c = currentBoard[2, 2, j];
                                d = currentBoard[3, 3, j];
                                break;
                            default:
                                break;
                        }

                        if ((a != 0 && (a == b && a == c && a == d)))
                        {
                            return a;
                        }
                    }
                }

            }
        }
        return 0;
    }

    private float evaluateBoard(int[,,] currentBoard, bool isMaximizer)
    {
        float boardScore = 0;
        if (DebugConfig.shouldCheckHorizontal) boardScore += horizontalChecking(currentBoard, isMaximizer);
        if (DebugConfig.shouldCheckVertical) boardScore += verticalChecking(currentBoard, isMaximizer);
        if (DebugConfig.shouldCheckDiagonal) boardScore += diagonalCheck(currentBoard, isMaximizer);

        return boardScore;
    }

    //check if there is any horizontal checking winning condition matched, return 0 for game continues without winner, 1 for player 1 won, 2 for player 2 won
    private float horizontalChecking(int[,,] currentBoard, bool isMaximizer)
    {
        float score = 0;

        for (int i = 0; i < 2; i++)
        {
            for (int y = 0; y < currentBoard.GetLength(1); y++)
            {
                for (int z = 0; z < currentBoard.GetLength(2); z++)
                {
                    int a = 0;
                    int b = 0;
                    int c = 0;
                    int d = 0;

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

                    score += Helper.getHorizontalCheckingScore(a, b, c, d, z, isMaximizer);
                }
            }
        }
        return score;
    }

    private float verticalChecking(int[,,] currentBoard, bool isMaximizer)
    {
        float score = 0;

        for (int x = 0; x < currentBoard.GetLength(0); x++)
        {
            for (int z = 0; z < currentBoard.GetLength(2); z++)
            {
                int a = currentBoard[x, 0, z];
                int b = currentBoard[x, 1, z];
                int c = currentBoard[x, 2, z];
                int d = currentBoard[x, 3, z];

                score += Helper.getVerticalCheckingScore(a, b, c, d, isMaximizer);
            }
        }
        return score;
    }

    private float diagonalCheck(int[,,] currentBoard, bool isMaximizer)
    {
        float score = 0;

        int a = 0;
        int b = 0;
        int c = 0;
        int d = 0;

        for (int i = 0; i < 10; i++)
        {
            if (i > 5 && i < 10)
            {
                switch (i)
                {
                    case 6:
                        a = currentBoard[0, 0, 0];
                        b = currentBoard[1, 1, 1];
                        c = currentBoard[2, 2, 2];
                        d = currentBoard[3, 3, 3];
                        //0,0,0 -> 1,1,1 --> 2,2,2 -> 3,3,3
                        break;
                    case 7:
                        a = currentBoard[0, 3, 0];
                        b = currentBoard[1, 2, 1];
                        c = currentBoard[2, 1, 2];
                        d = currentBoard[3, 0, 3];
                        break;
                    case 8:
                        a = currentBoard[3, 0, 0];
                        b = currentBoard[2, 1, 1];
                        c = currentBoard[1, 2, 2];
                        d = currentBoard[0, 3, 3];
                        break;
                    case 9:
                        a = currentBoard[0, 0, 3];
                        b = currentBoard[1, 1, 2];
                        c = currentBoard[2, 2, 1];
                        d = currentBoard[3, 3, 0];
                        break;
                    default:
                        break;
                }

                score += Helper.getDiagonalCheckingScore(a, b, c, d, isMaximizer);
            }
            else
            {
                for (int j = 0; j < currentBoard.GetLength(0); j++)
                {
                    switch (j)
                    {
                        case 0:
                            a = currentBoard[3, j, 0];
                            b = currentBoard[2, j, 1];
                            c = currentBoard[1, j, 2];
                            d = currentBoard[0, j, 3];
                            break;
                        case 1:
                            a = currentBoard[0, j, 0];
                            b = currentBoard[1, j, 1];
                            c = currentBoard[2, j, 2];
                            d = currentBoard[3, j, 3];
                            break;
                        case 2:
                            a = currentBoard[j, 0, 3];
                            b = currentBoard[j, 1, 2];
                            c = currentBoard[j, 2, 1];
                            d = currentBoard[j, 3, 0];
                            break;
                        case 3:
                            a = currentBoard[j, 0, 0];
                            b = currentBoard[j, 1, 1];
                            c = currentBoard[j, 2, 2];
                            d = currentBoard[j, 3, 3];
                            break;
                        case 4:
                            a = currentBoard[3, 0, j];
                            b = currentBoard[2, 1, j];
                            c = currentBoard[1, 2, j];
                            d = currentBoard[0, 3, j];
                            break;
                        case 5:
                            a = currentBoard[0, 0, j];
                            b = currentBoard[1, 1, j];
                            c = currentBoard[2, 2, j];
                            d = currentBoard[3, 3, j];
                            break;
                        default:
                            break;
                    }

                    score += Helper.getDiagonalCheckingScore(a, b, c, d, isMaximizer);
                }
            }
        }
        return score;
    }

}