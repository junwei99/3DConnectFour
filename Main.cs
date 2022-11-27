using System;
using System.Collections;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager(4, 4, 4);

            Helper helper = new Helper();

            // while (gm.playerThatWon == 0)
            // {
            //     string val = "";
            //     Console.WriteLine("input slot (0-15)");
            //     val = Console.ReadLine();
            //     int slotNumPlayed = Convert.ToInt32(val);

            //     gm.takeTurn(slotNumPlayed);
            //     Move bestMove = gm.getAIBestMove();

            //     int slotNum = gm.getSlotNumFromCoordinates(bestMove.xCoordinate, bestMove.yCoordinate, bestMove.zCoordinate);

            //     gm.takeTurn(slotNum);

            //     Console.WriteLine($"best move: {bestMove.xCoordinate}, {bestMove.yCoordinate}, {bestMove.xCoordinate} \n slotNum: {slotNum}");

            //     printBoard(gm.boardState);
            // }


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