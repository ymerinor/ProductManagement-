using ProductManagement.Domain.Product;

namespace ProductManagement.UnitTest.System.Fixtures
{
    public static class ProductFixtures
    {
        public static List<Products> GetProductTest => new()
        {
            new() {
                ProductId = 1,
                Description = "Prodcut",
                Discount = 1,
                FinalPrice = 1,
                Name = "Name",
                Price = 1,
                StatusName = true,
                Stock = 1
            }
        };

        public static Products ProductTest => new()
        {
            ProductId = 1,
            Description = "Prodcut",
            Discount = 1,
            FinalPrice = 1,
            Name = "Name",
            Price = 1,
            StatusName = true,
            Stock = 1

        };
    }
}
