public static class Helper
{
    //return player (1 or 2) that checks N out of 4 coordinates, if there is 2 players in the 4 coordinates or there is not enough coordinates checked, this function will return 0
    private static int getPlayerThatChecksNOutOfFour2(int amountOfCheckedSlot, int a, int b, int c, int d)
    {
        if (a != 0 && b != 0 && c != 0 && d != 0)
        {
            return 0;
        }

        int[] coordinateList = { a, b, c, d };

        bool isTherePlayer1 = false;
        bool isTherePlayer2 = false;

        for (int i = 0; i < coordinateList.Length; i++)
        {
            if (!isTherePlayer1 || !isTherePlayer2)
            {
                if (coordinateList[i] == 1)
                {
                    isTherePlayer1 = true;
                }
                else if (coordinateList[i] == 2)
                {
                    isTherePlayer2 = true;
                }
            }
            else
            {
                break;
            }
        }

        if (!(isTherePlayer1 && isTherePlayer2))
        {
            int nonEmptyValue = isTherePlayer1 ? 1 : 2;
            int coordinatesChecked = 0;
            for (int i = 0; i < coordinateList.Length; i++)
            {
                if (coordinateList[i] != 0)
                {
                    coordinatesChecked += 1;
                }
            }
            return coordinatesChecked == amountOfCheckedSlot ? nonEmptyValue : 0;
        }
        else
        {
            return 0;
        }
    }

    private static int getPlayerThatChecksNOutOfFour(int amountOfCheckedSlot, int a, int b, int c, int d)
    {
        if ((a != 0 && b != 0 && c != 0 && d != 0) || (a == 0 && b == 0 && c == 0 && d == 0))
        {
            return 0;
        }

        int[] coordinateList = { a, b, c, d };

        bool player1 = false;
        bool player2 = false;

        int nonEmptyValue = player1 ? 1 : 2;
        int coordinatesChecked = 0;
        for (int i = 0; i < coordinateList.Length; i++)
        {
            if (coordinateList[i] != 0)
            {
                if (coordinateList[i] == 1)
                {
                    player1 = true;
                }
                else
                {
                    player2 = true;
                }
                coordinatesChecked += 1;
            }
        }
        return !(player1 == true && player2 == true) && (coordinatesChecked == amountOfCheckedSlot) ? nonEmptyValue : 0;
    }

    public static float getHorizontalCheckingScore(int a, int b, int c, int d, int z, bool isMaximizer)
    {
        float horizontalCheckingScore = 0;

        int playerThatChecks3OutOf4 = getPlayerThatChecksNOutOfFour(3, a, b, c, d);
        int playerThatChecks2OutOf4 = getPlayerThatChecksNOutOfFour(2, a, b, c, d);

        //all 4 is checked
        if (a != 0 && a == b && a == c && a == d)
        {
            if (isMaximizer)
            {
                horizontalCheckingScore += ((a == 1) ? -1000 : 1000);
            }
            else
            {
                horizontalCheckingScore += ((a == 2) ? 1000 : -1000);
            }
        }

        //3 out of 4 checked
        // if ((b != 0) && (a == b && a == c && d == 0) || (a == 0 && b == c && b == d))
        if (playerThatChecks3OutOf4 != 0)
        {

            int compareValue = (a != 0) ? a : b;

            if (isMaximizer)
            {
                horizontalCheckingScore += ((compareValue == 1) ? -5 : 5);
            }
            else
            {
                horizontalCheckingScore += ((compareValue == 2) ? 5 : -5);
            }
        }

        //2 out of 4 checked
        // if ((a != 0 && a == b && c == 0 && d == 0) || ((c != 0) && (c == d && a == 0 && b == 0) || (b == c && a == 0 && d == 0)))
        if (playerThatChecks2OutOf4 != 0)
        {
            int compareValue = (a != 0) ? a : c;
            if (isMaximizer)
            {
                horizontalCheckingScore += ((compareValue == 1) ? -1 : 1);
            }
            else
            {
                horizontalCheckingScore += ((compareValue == 2) ? 1 : -1);
            }
        }

        //1 checked in middle (advantageous spot)
        if ((z == 1 || z == 2) && ((b != 0 && a == 0 && c == 0 && d == 0) || (c != 0 && a == 0 && b == 0 && d == 0)))
        {
            int compareValue = (b != 1) ? b : c;
            if (isMaximizer)
            {
                horizontalCheckingScore += ((compareValue == 1) ? -1 : 1);
            }
            else
            {
                horizontalCheckingScore += ((compareValue == 2) ? 1 : -1);
            }
        }

        return horizontalCheckingScore;
    }
    public static float getDiagonalCheckingScore(int a, int b, int c, int d, bool isMaximizer)
    {
        float diagonalCheckingScore = 0;

        int playerThatChecks3OutOf4 = getPlayerThatChecksNOutOfFour(3, a, b, c, d);
        int playerThatChecks2OutOf4 = getPlayerThatChecksNOutOfFour(2, a, b, c, d);

        if ((a != 0 && (a == b && a == c && a == d)))
        {
            if (isMaximizer)
            {
                diagonalCheckingScore += ((a == 1) ? -1000 : 1000);
            }
            else
            {
                diagonalCheckingScore += ((a == 2) ? 1000 : -1000);
            }
        }

        // if ((b != 0 && ((a == b && a == c && d == 0) || (b == c && b == d && a == 0))))
        if (playerThatChecks3OutOf4 != 0)
        {
            if (isMaximizer)
            {
                diagonalCheckingScore += ((playerThatChecks3OutOf4 == 1) ? -5 : 5);
            }
            else
            {
                diagonalCheckingScore += ((playerThatChecks3OutOf4 == 2) ? 5 : -5);
            }
        }

        // if ((b != 0 && ((a == b && c == 0 && d == 0) || (b == c && a == 0 && d == 0))) || (c != 0 && c == d && a == 0 && b == 0))
        if (playerThatChecks2OutOf4 != 0)
        {
            // int compareValue = (b != 0) ? b : c;
            if (isMaximizer)
            {
                diagonalCheckingScore += ((playerThatChecks2OutOf4 == 1) ? -1 : 1);
            }
            else
            {
                diagonalCheckingScore += ((playerThatChecks2OutOf4 == 2) ? 1 : -1);
            }
        }

        return diagonalCheckingScore;
    }

    public static float getVerticalCheckingScore(int a, int b, int c, int d, bool isMaximizer)
    {
        float verticalCheckingScore = 0;

        if (a != 0)
        {
            //all 4 is checked
            if (a == b && a == c && a == d)
            {
                if (isMaximizer)
                {
                    verticalCheckingScore += ((a == 1) ? -1000 : 1000);
                }
                else
                {
                    verticalCheckingScore += ((a == 2) ? 1000 : -1000);
                }
            }

            //3 is checked
            if (a == b && a == c && d == 0)
            {
                if (isMaximizer)
                {
                    verticalCheckingScore += ((a == 1) ? -5 : 5);
                }
                else
                {
                    verticalCheckingScore += ((a == 2) ? 5 : -5);
                }
            }

            //2 out of 4 checked
            if (a == b && c == 0 && d == 0)
            {
                if (isMaximizer)
                {
                    verticalCheckingScore += ((a == 1) ? -1 : 1);
                }
                else
                {
                    verticalCheckingScore += ((a == 2) ? 1 : -1);
                }
            }
        }

        return verticalCheckingScore;
    }
}
