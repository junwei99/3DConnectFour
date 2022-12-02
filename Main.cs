using System;
using System.Collections;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //start a new game with 4 x 4 x 4 board
            Game gm = new Game(4, 4, 4);

            int playerThatWon = 0;

            Console.WriteLine($"{DebugConfig.shouldCheckHorizontal},{DebugConfig.shouldCheckVertical},{DebugConfig.shouldCheckDiagonal}");

            //loop while there is no winner
            while (playerThatWon == 0)
            {
                playerThatWon = player1MakeMove(gm);

                if (playerThatWon != 0)
                {
                    Console.WriteLine($"{(playerThatWon == 1 ? "Player" : "AI")} won !");
                }
            }
        }

        private static int player1MakeMove(Game gm)
        {
            //call getAIBestMove to get a new Move object (x,y,z coordinates)
            Move bestMove = gm.ai.getAIBestMove(gm.boardState);

            //convert xyz coordinates to slot number
            int aiSlotNum = gm.getSlotNumFromCoordinates(bestMove.xCoordinate, bestMove.yCoordinate, bestMove.zCoordinate);

            //AI play a move
            bool didAIWon = makeMove(gm, aiSlotNum);

            printBoard(gm.boardState);

            //if there is a winner, return number (1 = player 1 won, 2 = player 2 won) 
            if (didAIWon)
            {
                return gm.playerThatWon;
            }


            string val = "";
            //get user input for slot to play
            Console.WriteLine("input slot (0-15)");
            val = Console.ReadLine();
            int slotNumPlayed = Convert.ToInt32(val);

            //player 1 play a move
            bool didPlayerWon = makeMove(gm, slotNumPlayed);

            //if there is a winner, return number (1 = player 1 won, 2 = player 2 won) 
            printBoard(gm.boardState);

            if (didPlayerWon)
            {
                return gm.playerThatWon;
            }

            Console.WriteLine($"best move: {bestMove.xCoordinate}, {bestMove.yCoordinate}, {bestMove.zCoordinate} \n slotNum: {aiSlotNum}");

            //no winner, game will proceed (retunr 0)
            return 0;
        }

        //return 0 if no winning condition is matched after the move, return the player number if winning condiiton is matched
        private static bool makeMove(Game gm, int slot)
        {
            gm.takeTurn(slot);
            return gm.didSomeoneWonTheGame();
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