using System;

namespace MagicSquare
{
    public static class Utils
    {
        public static int Mod(int x, int m)
        {
            return x < 0 ? x % m + m : x % m;
        }

        public static void PrintMatrix(int[,] matrix)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < width; ++j)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write(' ');
                }

                Console.WriteLine();
            }
        }
    }
}
