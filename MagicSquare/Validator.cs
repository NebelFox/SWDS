using System.Linq;

namespace MagicSquare
{
    public class Validator
    {
        private readonly int _expectedSum;
        private readonly int[,] _square;

        public Validator(int[,] square)
        {
            _square = square;
            _expectedSum = _area * (_area + 1) / 2 / _size;
        }

        private int _size => _square.GetLength(0);
        private int _area => _square.Length;

        public bool IsValid()
        {
            return Enumerable.Range(0, 3)
                             .All(i => _isRowSumValid(i)
                                       && _isColumnSumValid(i))
                   && _isDiagonalSumValid(1)
                   && _isDiagonalSumValid(-1);
        }

        private bool _isRowSumValid(int index)
        {
            return _isSumValid(0, 1, index, 0);
        }

        private bool _isColumnSumValid(int index)
        {
            return _isSumValid(index, 0, 0, 1);
        }

        private bool _isDiagonalSumValid(int direction)
        {
            int start = _size / 2 - _size / 2 * direction;
            return _isSumValid(start, direction, start, direction);
        }

        private bool _isSumValid(int x, int vx, int y, int vy)
        {
            return _sum(x, vx, y, vy) == _expectedSum;
        }

        private int _sum(int x, int vx, int y, int vy)
        {
            int size = _size;
            var sum = 0;
            for (var i = 0; i < size; ++i)
            {
                sum += _square[y, x];
                x += vx;
                y += vy;
            }

            return sum;
        }
    }
}
