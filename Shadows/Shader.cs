using System.ComponentModel;
using System.Linq;

namespace Shadows
{
    public enum Prospect
    {
        FrontRear,
        LeftRight,
        TopBottom
    }

    public static class Shader
    {
        public static bool[,] Shade(bool[,,] shape, Prospect prospect)
        {
            return prospect switch
            {
                Prospect.FrontRear => ShadeFrontRear(shape),
                
                Prospect.LeftRight => ShadeLeftRight(shape),
                
                Prospect.TopBottom => ShadeTopBottom(shape),
                
                _ => throw new InvalidEnumArgumentException(nameof(prospect), 
                                                            (int)prospect, 
                                                            typeof(Prospect))
            };
        }

        public static bool[,] ShadeFrontRear(bool[,,] shape)
        {
            int height = shape.GetLength(0);
            int width = shape.GetLength(1);
            int depth = shape.GetLength(2);

            var shade = new bool[height, width];

            for (int i = height * width - 1; i > -1; --i)
            {
                shade[i / height, i % height]
                    = Enumerable.Range(0, depth)
                                .Any(k => shape[i / height, i % height, k]);
            }

            return shade;
            /*for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < width; ++j)
                {
                    shade[i, j] = Enumerable.Range(0, depth).Any(k => shape[i, j, k]);
                }
            }*/
        }

        public static bool[,] ShadeLeftRight(bool[,,] shape)
        {
            int height = shape.GetLength(0);
            int width = shape.GetLength(1);
            int depth = shape.GetLength(2);

            var shade = new bool[height, depth];

            for (int i = height * depth - 1; i > -1; --i)
            {
                shade[i / height, i % height]
                    = Enumerable.Range(0, width)
                                .Any(k => shape[i / height, k, i % height]);
            }

            return shade;
        }

        public static bool[,] ShadeTopBottom(bool[,,] shape)
        {
            int height = shape.GetLength(0);
            int width = shape.GetLength(1);
            int depth = shape.GetLength(2);

            var shade = new bool[width, depth];

            for (int i = width * depth - 1; i > -1; --i)
            {
                shade[i / width, i % width]
                    = Enumerable.Range(0, height)
                                .Any(k => shape[k, i % width, i / width]);
            }

            return shade;
        }
    }
}
