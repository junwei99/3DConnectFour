using System;
using System.Collections;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //start a new game with 4 x 4 x 4 board
            GameManager gm = new GameManager(4, 4, 4);

            //loop while there is no winner
            while (gm.playerThatWon == 0)
            {
                string val = "";
                //get user input for slot to play
                Console.WriteLine("input slot (0-15)");
                val = Console.ReadLine();
                int slotNumPlayed = Convert.ToInt32(val);

                //if 0 game will go on, if 1 or 2 means there is a winner
                int gameWinValue = player1MakeMove(gm, slotNumPlayed);

                //print board to console
                printBoard(gm.boardState);
            }
        }

        private static int player1MakeMove(GameManager gm, int player1SlotPlayed)
        {
            //player 1 play a move
            int player1WinValue = makeMove(gm, player1SlotPlayed);

            //if there is a winner, return number (1 = player 1 won, 2 = player 2 won) 
            if (player1WinValue != 0)
            {
                return player1WinValue;
            }

            //call getAIBestMove to get a new Move object (x,y,z coordinates)
            Move bestMove = gm.getAIBestMove();

            //convert xyz coordinates to slot number
            int aiSlotNum = gm.getSlotNumFromCoordinates(bestMove.xCoordinate, bestMove.yCoordinate, bestMove.zCoordinate);

            //AI play a move
            int aiWinValue = makeMove(gm, aiSlotNum);

            Console.WriteLine($"best move: {bestMove.xCoordinate}, {bestMove.yCoordinate}, {bestMove.zCoordinate} \n slotNum: {aiSlotNum}");

            //if there is a winner, return number (1 = player 1 won, 2 = player 2 won) 
            if (aiWinValue != 0)
            {
                return aiWinValue;
            }

            //no winner, game will proceed (retunr 0)
            return 0;
        }

        //return 0 if no winning condition is matched after the move, return the player number if winning condiiton is matched
        private static int makeMove(GameManager gm, int slot)
        {
            gm.takeTurn(slot);

            // //check if anyone won
            // int winningPlayer = gm.winningConditionCheck() != 0 ? gm.winningConditionCheck() : 0;

            // //if there is a winner, return number (1 = player 1 won, 2 = player 2 won) 
            // return winningPlayer;
            return 0;
        }

        //print board to console
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