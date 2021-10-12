using System;
using System.Collections.Generic;

namespace Statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new Loader("visits.log");
            var analyzer = new Analyzer(loader.Load());
        }
    }
}
