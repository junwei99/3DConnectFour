using System.Collections;
public class Helper
{
    public List<int[,]> slice(int[,,] board)
    {
        List<int[,]> slices = new List<int[,]>();

        // horizontal
        for (int d = 0; d < 4; d++)
        {
            int[,] slice = new int[4, 4];
            for (int h = 0; h < 4; h++)
            {
                for (int w = 0; w < 4; w++)
                {
                    slice[h, w] = board[h, d, w];
                }
            }
            slices.Add(slice);
        }

        // vertical
        for (int w = 0; w < 4; w++)
        {
            int[,] slice = new int[4, 4];
            for (int h = 0; h < 4; h++)
            {
                for (int d = 0; d < 4; d++)
                {
                    slice[h, d] = board[h, d, w];
                }
            }
            slices.Add(slice);
        }

        // diagonal
        for (int s = 0; s < 2; s++)
        {
            int[,] slice = new int[4, 4];
            for (int h = 0; h < 4; h++)
            {
                for (int i = 0; i < 4; i++)
                {
                    slice[h, i] = board[h, i, (s == 0) ? i : 3 - i];
                }
            }
            slices.Add(slice);
        }

        return slices;
    }

}