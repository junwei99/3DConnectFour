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
                        float score = minimax(copyOfBoard, minimaxMaxDepth, false);
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

    float minimax(int[,,] board, int searchDepth, bool isMaximizer)
    {
        float bestScore = 0;
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

    public int getWinnerValue(int[,,] currentBoard)
    {
        //horizontal check
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

        //diagonal check
        for (int i = 0; i < 2; i++)
        {
            for (int y = 0; y < currentBoard.GetLength(1); y++)
            {
                int a;
                int b;
                int c;
                int d;

                if (i == 0)
                {
                    a = currentBoard[3, y, 0];
                    b = currentBoard[2, y, 1];
                    c = currentBoard[1, y, 2];
                    d = currentBoard[0, y, 3];
                }
                else
                {
                    a = currentBoard[0, y, 3];
                    b = currentBoard[1, y, 2];
                    c = currentBoard[2, y, 1];
                    d = currentBoard[3, y, 0];
                }

                if ((a != 0 && (a == b && a == c && a == d)))
                {
                    return a;
                }
            }

        }

        //swap diagonal check positions
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < currentBoard.GetLength(0); j++)
            {
                int a = 0;
                int b = 0;
                int c = 0;
                int d = 0;

                switch (i)
                {
                    case 0:
                        a = currentBoard[3, j, 0];
                        b = currentBoard[2, j, 1];
                        c = currentBoard[1, j, 2];
                        d = currentBoard[0, j, 3];
                        break;
                    case 1:
                        a = currentBoard[0, j, 3];
                        b = currentBoard[1, j, 2];
                        c = currentBoard[2, j, 1];
                        d = currentBoard[3, j, 0];
                        break;
                    case 2:
                        a = currentBoard[j, 0, 3];
                        b = currentBoard[j, 1, 2];
                        c = currentBoard[j, 2, 1];
                        d = currentBoard[j, 3, 0];
                        break;
                    case 3:
                        a = currentBoard[j, 3, 0];
                        b = currentBoard[j, 2, 1];
                        c = currentBoard[j, 1, 2];
                        d = currentBoard[j, 0, 3];
                        break;
                    case 4:
                        a = currentBoard[3, 0, j];
                        b = currentBoard[2, 1, j];
                        c = currentBoard[1, 2, j];
                        d = currentBoard[0, 3, j];
                        break;
                    case 5:
                        a = currentBoard[0, 3, j];
                        b = currentBoard[1, 2, j];
                        c = currentBoard[2, 1, j];
                        d = currentBoard[3, 0, j];
                        break;
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
                }

                if ((a != 0 && (a == b && a == c && a == d)))
                {
                    return a;
                }
            }
        }

        return 0;
    }

    float evaluateBoard(int[,,] currentBoard, bool isMaximizer)
    {
        float boardScore = 0;
        boardScore += horizontalChecking(currentBoard, isMaximizer);
        boardScore += verticalChecking(currentBoard, isMaximizer);
        boardScore += diagonalCheck(currentBoard, isMaximizer);

        return boardScore;
    }

    //check if there is any horizontal checking winning condition matched, return 0 for game continues without winner, 1 for player 1 won, 2 for player 2 won
    float horizontalChecking(int[,,] currentBoard, bool isMaximizer)
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

                    //all 4 is checked
                    if (a != 0 && a == b && a == c && a == d)
                    {
                        if (isMaximizer)
                        {
                            score += ((a == 1) ? -1000 : 1000);
                        }
                        else
                        {
                            score += ((a == 2) ? 1000 : -1000);
                        }
                    }

                    //3 out of 4 checked
                    if ((b != 0) && (a == b && a == c && d == 0) || (a == 0 && b == c && b == d))
                    {

                        int compareValue = (a != 0) ? a : b;

                        if (isMaximizer)
                        {
                            score += ((compareValue == 1) ? -5 : 5);
                        }
                        else
                        {
                            score += ((compareValue == 2) ? 5 : -5);
                        }
                    }

                    //2 out of 4 checked
                    if ((a != 0 && a == b && c == 0 && d == 0) || ((c != 0) && (c == d && a == 0 && b == 0) || (b == c && a == 0 && d == 0)))
                    {
                        int compareValue = (a != 0) ? a : c;
                        if (isMaximizer)
                        {
                            score += ((compareValue == 1) ? -1 : 1);
                        }
                        else
                        {
                            score += ((compareValue == 2) ? 1 : -1);
                        }
                    }

                    //1 checked in middle (advantageous spot)
                    if ((z == 1 || z == 2) && ((b != 0 && a == 0 && c == 0 && d == 0) || (c != 0 && a == 0 && b == 0 && d == 0)))
                    {
                        int compareValue = (b != 1) ? b : c;
                        if (isMaximizer)
                        {
                            score += ((compareValue == 1) ? -1 : 1);
                        }
                        else
                        {
                            score += ((compareValue == 2) ? 1 : -1);
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

        for (int x = 0; x < currentBoard.GetLength(0); x++)
        {
            for (int z = 0; z < currentBoard.GetLength(2); z++)
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
                            score += ((a == 1) ? -1000 : 1000);
                        }
                        else
                        {
                            score += ((a == 2) ? 1000 : -1000);
                        }
                    }

                    //3 is checked
                    if (a == b && a == c && d == 0)
                    {
                        if (isMaximizer)
                        {
                            score += ((a == 1) ? -5 : 5);
                        }
                        else
                        {
                            score += ((a == 2) ? 5 : -5);
                        }
                    }

                    //2 out of 4 checked
                    if (a == b && c == 0 && d == 0)
                    {
                        if (isMaximizer)
                        {
                            score += ((a == 1) ? -1 : 1);
                        }
                        else
                        {
                            score += ((a == 2) ? 1 : -1);
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

        //swap diagonal check positions
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < currentBoard.GetLength(0); j++)
            {
                int a = 0;
                int b = 0;
                int c = 0;
                int d = 0;

                switch (i)
                {
                    case 0:
                        a = currentBoard[3, j, 0];
                        b = currentBoard[2, j, 1];
                        c = currentBoard[1, j, 2];
                        d = currentBoard[0, j, 3];
                        break;
                    case 1:
                        a = currentBoard[0, j, 3];
                        b = currentBoard[1, j, 2];
                        c = currentBoard[2, j, 1];
                        d = currentBoard[3, j, 0];
                        break;
                    case 2:
                        a = currentBoard[j, 0, 3];
                        b = currentBoard[j, 1, 2];
                        c = currentBoard[j, 2, 1];
                        d = currentBoard[j, 3, 0];
                        break;
                    case 3:
                        a = currentBoard[j, 3, 0];
                        b = currentBoard[j, 2, 1];
                        c = currentBoard[j, 1, 2];
                        d = currentBoard[j, 0, 3];
                        break;
                    case 4:
                        a = currentBoard[3, 0, j];
                        b = currentBoard[2, 1, j];
                        c = currentBoard[1, 2, j];
                        d = currentBoard[0, 3, j];
                        break;
                    case 5:
                        a = currentBoard[0, 3, j];
                        b = currentBoard[1, 2, j];
                        c = currentBoard[2, 1, j];
                        d = currentBoard[3, 0, j];
                        break;
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
                }

                if ((a != 0 && (a == b && a == c && a == d)))
                {
                    if (isMaximizer)
                    {
                        score += ((a == 1) ? -1000 : 1000);
                    }
                    else
                    {
                        score += ((a == 2) ? 1000 : -1000);
                    }
                }

                if ((b != 0 && ((a == b && a == c && d == 0) || (b == c && b == d && a == 0))))
                {
                    if (isMaximizer)
                    {
                        score += ((a == 1) ? -5 : 5);
                    }
                    else
                    {
                        score += ((a == 2) ? 5 : -5);
                    }
                }

                if ((b != 0 && ((a == b && c == 0 && d == 0) || (b == c && a == 0 && d == 0))) || (c != 0 && c == d && a == 0 && b == 0))
                {
                    int compareValue = (b != 0) ? b : c;
                    if (isMaximizer)
                    {
                        score += ((compareValue == 1) ? -1 : 1);
                    }
                    else
                    {
                        score += ((compareValue == 2) ? 1 : -1);
                    }
                }
            }
        }

        return score;
    }
}