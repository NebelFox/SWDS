using System;

namespace MagicSquare
{
    public static class Generator
    {
        public static int[,] Generate(int size, Strategy strategy = Strategy.DOWN_RIGHT)
        {
            if (size < 0 || size % 2 == 0)
                throw new ArgumentException("Only positive odd size is supported", nameof(size));

            var square = new int[size, size];
            var step = (int)strategy;

            int x = size >> 1;
            int y = x + x * step;
            square[x, y] = 1;

            for (var i = 1; i < size * size; ++i)
            {
                if (square[Utils.Mod(x + 1, size), Utils.Mod(y + step, size)] != 0)
                {
                    y = Utils.Mod(y - step, size);
                }
                else
                {
                    x = Utils.Mod(x + 1, size);
                    y = Utils.Mod(y + step, size);
                }

                square[x, y] = i + 1;
            }

            return square;
        }
    }
}
