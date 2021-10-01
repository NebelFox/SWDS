namespace Task.Data
{
    public static class ExtraPriceChangePercents
    {
        public static readonly int PerCategory = 7;
        public static readonly int PerDayToExpire = 3;

        public static int ForCategory(Category category)
        {
            return (int)category * PerCategory;
        }

        public static int ForDaysToExpire(int daysToExpire)
        {
            return daysToExpire * PerDayToExpire;
        }
    }
}
