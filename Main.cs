using System;
using System.Collections;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager(4, 4, 4);

            while (gm.playerThatWon == 0)
            {
                string val = "";
                Console.WriteLine("input slot (0-15)");
                val = Console.ReadLine();
                int slotNumPlayed = Convert.ToInt32(val);
                //if 0 game will go on, if 1 or 2 means there is a winner
                int gameWinValue = player1MakeMove(gm, slotNumPlayed);

                printBoard(gm.boardState);
            }
        }

        private static int player1MakeMove(GameManager gm, int player1SlotPlayed)
        {
            int player1WinValue = makeMove(gm, player1SlotPlayed);

            if (player1WinValue != 0)
            {
                return player1WinValue;
            }

            Move bestMove = gm.getAIBestMove();

            int aiSlotNum = gm.getSlotNumFromCoordinates(bestMove.xCoordinate, bestMove.yCoordinate, bestMove.zCoordinate);

            int aiWinValue = makeMove(gm, aiSlotNum);

            Console.WriteLine($"best move: {bestMove.xCoordinate}, {bestMove.yCoordinate}, {bestMove.xCoordinate} \n slotNum: {aiSlotNum}");

            if (aiWinValue != 0)
            {
                return aiWinValue;
            }

            return 0;
        }

        //return 0 if no winning condition is matched after the move, return the player number if winning condiiton is matched
        private static int makeMove(GameManager gm, int slot)
        {
            gm.takeTurn(slot);
            int winningPlayer = gm.winningConditionCheck() != 0 ? gm.winningConditionCheck() : 0;

            return winningPlayer;
        }

        private static void printBoard(int[,,] boardState)
        {
            char[] symbols = { ' ', '●', '○' };

            for (int y = (boardState.GetLength(0) - 1); y >= 0; y--)
            {
                for (int z = 0; z < boardState.GetLength(1); z++)
                {
                    for (int x = 0; x < boardState.GetLength(2); x++)
                    {
                        Console.Write(symbols[boardState[x, y, z]] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("-------");
            }

        }
    }
}