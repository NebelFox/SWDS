using System.Collections.Generic;
using System.Linq;

namespace Shop.Models
{
    public class Check
    {
        private readonly LinkedList<Buy> _buys;

        public Check(IEnumerable<Buy> buys)
        {
            _buys = new LinkedList<Buy>(buys);
        }

        public float Price => _buys.Sum(buy => buy.Price);
        public double Weight => _buys.Sum(buy => buy.Weight);

        private string Footer => $"Total Price: {Price:N2}\nTotalWeight: {Weight:N2}";

        public override string ToString()
        {
            var index = 0;
            string body = _buys.Count > 0 
                ? string.Join('\n', _buys.Select(buy => $"{++index}. {buy}")) 
                : "~~Empty~~";

            return $@"{body}\n\n{Footer}";
        }
    }
}
