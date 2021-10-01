using System;

namespace Task.Models
{
    public class Dairy : Product
    {
        private int _daysToExpire;

        public Dairy(string name,
                     float price,
                     double weight,
                     int daysToExpire) : base(name, price, weight)
        {
            DaysToExpire = daysToExpire;
        }

        public int DaysToExpire
        {
            get => _daysToExpire;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(DaysToExpire),
                                                          value,
                                                          "Can't be negative");
                _daysToExpire = value;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + DaysToExpire.GetHashCode();
        }

        public override string ToString()
        {
            return $"Dairy {base.ToString()} expiring after {DaysToExpire} days";
        }
    }
}
