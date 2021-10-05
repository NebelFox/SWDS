using System;
using Shop.Utils;

namespace Shop.Models
{
    public class Product
    {
        private const string DefaultName = "Unknown";
        private const float DefaultPrice = 0f;
        private const double DefaultWeight = 0f;
        private const uint DefaultDaysToExpire = 7u;

        private float _price;
        private readonly double _weight;
        private readonly DateTime _dateCreated;
        private readonly uint _daysToExpire;

        public Product()
        {
            Name = DefaultName;
            Price = DefaultPrice;
            Weight = DefaultWeight;
            _daysToExpire = DefaultDaysToExpire;
        }

        public Product(string name,
                          float price,
                          double weight,
                          DateTime dateCreated,
                          uint daysToExpire)
        {
            Name = name;
            Price = price;
            Weight = weight;
            _dateCreated = dateCreated;
            _daysToExpire = daysToExpire;
        }

        public string Name { get; }

        public float Price
        {
            get => _price;
            protected set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Price),
                                                          value,
                                                          "Price can't be negative");
                _price = value;
            }
        }

        public double Weight
        {
            get => _weight;
            protected init
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Weight),
                                                          value,
                                                          "Weight can't be negative");
                _weight = value;
            }
        }

        public bool IsExpired(DateTime now)
        {
            return now.Subtract(_dateCreated).Days > _daysToExpire;
        }

        public virtual void ChangePrice(int percents)
        {
            Price *= NumUtils.ExtraPercentsToFloat(percents);
        }

        public override bool Equals(object? another)
        {
            if (another == null || GetType() != another.GetType())
                return false;

            var product = (Product)another;

            return Name == product.Name
                && Price == product.Price
                && Weight == product.Weight;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Price.GetHashCode() + Weight.GetHashCode();
        }

        public override string ToString()
        {
            return $"\"{Name}\" priced at {Price:N2} weighing {Weight:N2}";
        }
    }
}
