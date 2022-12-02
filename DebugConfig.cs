public static class DebugConfig
{
    public const string
        NoneStrValue = "none",
        HorizontalStrValue = "horizontal",
        VerticalStrValue = "vertical",
        DiagonalStrValue = "diagonal";
    public const string TypeOfCheckingToDebug = DiagonalStrValue;
    //should game end after there is a winner
    public const bool shouldGameEndAfterWinningMove = false;
    public static bool shouldCheckHorizontal = (TypeOfCheckingToDebug == NoneStrValue) || (TypeOfCheckingToDebug == HorizontalStrValue);
    public static bool shouldCheckVertical = (TypeOfCheckingToDebug == NoneStrValue) || (TypeOfCheckingToDebug == VerticalStrValue);
    public static bool shouldCheckDiagonal = (TypeOfCheckingToDebug == NoneStrValue) || (TypeOfCheckingToDebug == DiagonalStrValue);
}