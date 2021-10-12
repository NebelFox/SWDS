using System;

namespace BackwardMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new Matrix<int>(new int[,]
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12}
            });

            Console.WriteLine("The matrix, enumerated in reversed order:");
            foreach (int i in matrix)
                Console.WriteLine(i);
        }
    }
}
