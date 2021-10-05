using System;

namespace Shadows
{
    public static class Shaper
    {
        /*public static bool[,,] FromInts(int[,,] source, Func<int, >)
        {
            int height = source.GetLength(0);
            int width = source.GetLength(1);
            int depth = source.GetLength(2);

            var shape = new bool[height, width, depth];

            for (int i = height; i > 0; --i)
            for (int j = width; j > 0; --j)
            for (int k = depth; k > 0; --k)
                shape[i, j, k] = source[i, j, k] == 1;

            return shape;
        }*/
        
        public static bool[,,] FromStrings(string[,] source, Func<char, bool> predicate)
        {
            int height = source.GetLength(0);
            int width = source.GetLength(1);
            int depth = source[0, 0].Length;
            
            var shape = new bool[height, width, depth];
            
            // depth -> width
            // width -> height
            // height -> depth
            for (var i = 0; i < height; ++i)
            for (var j = 0; j < width; ++j)
            for (var k = 0; k < depth; ++k)
                shape[i, j, k] = predicate(source[k, i][j]);

            return shape;
        }
    }
}
