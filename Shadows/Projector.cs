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

    public static class Projector
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

            var projection = new bool[height, width];

            for (var i = 0; i < height; ++i)
            for (var j = 0; j < width; ++j)
            {
                projection[i, j] = Enumerable.Range(0, depth)
                                                     .Any(k => shape[i, j, k]);
            }

            return projection;
        }

        public static bool[,] ShadeLeftRight(bool[,,] shape)
        {
            int height = shape.GetLength(0);
            int width = shape.GetLength(1);
            int depth = shape.GetLength(2);

            var shade = new bool[height, depth];

            for (var i = 0; i < height; ++i)
            for (var j = 0; j < depth; ++j)
            {
                shade[i, j] = Enumerable.Range(0, width)
                                        .Any(k => shape[i, k, j]);
            }

            return shade;
        }

        public static bool[,] ShadeTopBottom(bool[,,] shape)
        {
            int height = shape.GetLength(0);
            int width = shape.GetLength(1);
            int depth = shape.GetLength(2);

            var shade = new bool[width, depth];

            for (var i = 0; i < depth; ++i)
            for (var j = 0; j < width; ++j)
            {
                shade[i, j] = Enumerable.Range(0, height)
                                        .Any(k => shape[k, j, i]);
            }

            return shade;
        }
    }
}
