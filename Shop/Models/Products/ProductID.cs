namespace Shop.Models.Products
{
    public readonly struct ProductID
    {
        public readonly ulong Value;

        public ProductID(ulong value)
        {
            Value = value;
        }
        
        public static bool TryParse(string token, out ProductID id)
        {
            if (ulong.TryParse(token, out ulong value) == false)
            {
                id = new ProductID(0);
                return false;
            }
            id = new ProductID(value);
            return true;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
