using Shop.Models;
using Shop.Models.Users;
using Verbox;

namespace Shop
{
    // public record Item(string Name, float Price, int Count);

    public class Program
    {
        private static void Main(string[] args)
        {
            ShopFacade shop = ShopFacade.Instance;
            shop.Authorizer.Register("root", "root", Role.Admin);
            shop.Authorizer.Register("user", "user");
            
            Box root = shop.Authorize("root", "root");
            const string script = @"create milk 12
create bread 5
request 0 4
request 1 7";
            foreach (string command in script.Split('\n'))
                root.Execute(command);
            
            Box client = shop.GetGuestClient();
            client.StartDialogue();
            /*var items = new Item[]
            {
                new("milk", 12, 2), new("bread", 4523, 53), new("marshmallow", 1, 0), new(null, 4141, 142)
            };
            var columns = new SortedDictionary<string, TableView<Item>.Selector>
            {
                { "Name", item => item.Name },
                { "Price", item => item.Price },
                { "Count", item => item.Count }
            };
            var table = new TableView<Item>(items, columns, " | ", "--", "Index");
            Console.WriteLine(string.Join('\n', table.View()));*/
        }
    }
}
