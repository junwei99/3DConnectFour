public static class Helper
{
    public static bool isNOutOfFourChecked(int amountOfCheckedSlot, int a, int b, int c, int d)
    {
        int[] coordinateList = { a, b, c, d };

        int[] uniqueCoordinateList = coordinateList.Distinct().ToArray();

        //if there is empty space, player and AI moves in the coordinates
        if (uniqueCoordinateList.Length < 3)
        {
            //find the coordinate value (PLAYER or AI) that is checked
            int nonEmptyValue = Array.Find(coordinateList, coordinate => coordinate != 0);
            //find all the coordinate that is not zero (empty)
            int[] coordinateListToCheck = Array.FindAll(coordinateList, coordinate => coordinate == nonEmptyValue);
            return coordinateListToCheck.Length == amountOfCheckedSlot ? true : false;
        }
        else
        {
            return false;
        }
    }
    public static float getDiagonalCheckingScore(int a, int b, int c, int d, bool isMaximizer)
    {
        float diagonalCheckingScore = 0;

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

        if ((b != 0 && ((a == b && a == c && d == 0) || (b == c && b == d && a == 0))))
        {
            if (isMaximizer)
            {
                diagonalCheckingScore += ((a == 1) ? -5 : 5);
            }
            else
            {
                diagonalCheckingScore += ((a == 2) ? 5 : -5);
            }
        }

        if ((b != 0 && ((a == b && c == 0 && d == 0) || (b == c && a == 0 && d == 0))) || (c != 0 && c == d && a == 0 && b == 0))
        {
            int compareValue = (b != 0) ? b : c;
            if (isMaximizer)
            {
                diagonalCheckingScore += ((compareValue == 1) ? -1 : 1);
            }
            else
            {
                diagonalCheckingScore += ((compareValue == 2) ? 1 : -1);
            }
        }

        return diagonalCheckingScore;
    }
}
