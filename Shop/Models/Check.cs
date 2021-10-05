using System;
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

        public string Footer => $"Total Price: {Price:N2}\nTotalWeight: {Weight:N2}";

        public void View()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            var index = 0;
            return _buys.Count > 0
                       ? string.Join('\n', _buys.Select(buy => $"{++index}. {buy}"))
                       : "~~Empty~~"
                         + "\n\n"
                         + Footer;
        }
    }
}
