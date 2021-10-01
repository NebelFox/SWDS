using System;
using Task.Utils;

namespace Task.Models
{
    public class Product
    {
        protected float _price;
        protected double _weight;

        public Product()
        {
            Name = string.Empty;
            Price = 0;
            Weight = 0;
        }

        public Product(string name,
                       float price,
                       double weight)
        {
            Name = name;
            Price = price;
            Weight = weight;
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
            protected set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Weight),
                                                          value,
                                                          "Weight can't be negative");
                _weight = value;
            }
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
