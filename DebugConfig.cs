public static class DebugConfig
{
    public const string
        NoneStrValue = "none",
        HorizontalStrValue = "horizontal",
        VerticalStrValue = "vertical",
        DiagonalStrValue = "diagonal";

    public const bool isDebugMode = false;
    public const string TypeOfCheckingToDebug = isDebugMode ? HorizontalStrValue : NoneStrValue;
    //should game end after there is a winner
    public const bool shouldGameEndAfterWinningMove = isDebugMode ? false : true;
    public static bool shouldCheckHorizontal = (TypeOfCheckingToDebug == NoneStrValue) || (TypeOfCheckingToDebug == HorizontalStrValue);
    public static bool shouldCheckVertical = (TypeOfCheckingToDebug == NoneStrValue) || (TypeOfCheckingToDebug == VerticalStrValue);
    public static bool shouldCheckDiagonal = (TypeOfCheckingToDebug == NoneStrValue) || (TypeOfCheckingToDebug == DiagonalStrValue);
}