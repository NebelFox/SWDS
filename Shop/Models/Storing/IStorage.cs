namespace Shop.Models.Storing
{
    public interface IStorage : IMutableStorage,
                                IProductsRegistry,
                                IReservation
    {
        
    }
}
