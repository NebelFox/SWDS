using System;
using Shop.Utils;
using Task.Data;

namespace Shop.Models.Products
{
    public class Meat : Product
    {
        public Meat(string name,
                    float price,
                    double weight,
                    DateTime dateCreated,
                    uint daysToExpire,
                    Category category,
                    MeatKind meatKind) : base(name, price, weight, dateCreated, daysToExpire)
        {
            Category = category;
            MeatKind = meatKind;
        }

        public Category Category { get; }
        public MeatKind MeatKind { get; }

        public override void ChangePrice(int percents)
        {
            // percents >= 0 => percents * (1 + <extra-percents-of-category>)
            // percents < 0 => percents * (1 - <extra-percents-of-category>)
            Price *= NumUtils.ExtraPercentsToFloat(percents)
                   * (1
                    + NumUtils.PercentsToFloat(ExtraPriceChangePercents.ForCategory(Category))
                    * percents
                   >= 0
                         ? 1
                         : -1);
        }

        public override bool Equals(object? another)
        {
            var meat = (Meat)another;

            return base.Equals(another)
                && meat.Category == Category
                && meat.MeatKind == MeatKind;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + Category.GetHashCode() + MeatKind.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Category.ToTitle()} {MeatKind.ToTitle()} {base.ToString()}";
        }
    }
}
