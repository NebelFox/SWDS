using System.Linq;

namespace Shadows
{
    public class Renderer
    {
        private readonly char _separator;
        private readonly char _trueMark;
        private readonly char _falseMark;

        public Renderer(char separator = ' ',
                        char trueMark = '1',
                        char falseMark = '0')
        {
            _separator = separator;
            _trueMark = trueMark;
            _falseMark = falseMark;
        }

        public string Render(bool[,] shade)
        {
            return string.Join('\n',
                               Enumerable.Range(0, shade.GetLength(0))
                                         .Select(y => RenderRow(shade, y)));
        }

        private string RenderRow(bool[,] shade,
                                 int y)
        {
            return string.Join(_separator,
                               Enumerable.Range(0, shade.GetLength(1))
                                         .Select(x => GetMark(shade[y, x])));
        }

        private char GetMark(bool value)
        {
            return value ? _trueMark : _falseMark;
        }
    }
}
