using System;
using System.Collections;
using System.Collections.Generic;

namespace BackwardMatrix
{
    public class Matrix<T> : IEnumerable<T>
    {
        private readonly T[,] _content;
        
        public Matrix(T[,] content)
        {
            _content = content;
        }

        public int Height => _content.GetLength(0);

        public int Width => _content.GetLength(1);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BackwardEnumerator(_content);
        }

        public class BackwardEnumerator : IEnumerator<T>
        {
            private readonly T[,] _source;
            private int _row;
            private int _column;

            public BackwardEnumerator(T[,] source)
            {
                _source = source;
                Reset();
            }

            public bool MoveNext()
            {
                if (--_column < 0)
                {
                    --_row;
                    _column = _source.GetLength(1) - 1;
                }
                return _row > -1;
            }

            public void Reset()
            {
                _row = _source.GetLength(0) - 1;
                _column = _source.GetLength(1);
            }

            public T Current => _source[_row, _column];

            object IEnumerator.Current => Current;

            public void Dispose()
            { }
        }
    }
}
