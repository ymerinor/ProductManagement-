using ProductManagement.Application.Product.Dto;
using ProductManagement.Domain.Product;

namespace ProductManagement.UnitTest.System.Fixtures
{
    public static class ProductFixtures
    {
        public static Products ProductTest => new()
        {
            ProductId = 1,
            Description = "Prodcut",
            Discount = 1,
            FinalPrice = 1,
            Name = "Name",
            Price = 1,
            StatusName = "Activo",
            Stock = 1,
            DateRegistration = DateTime.Now,
            DateUpdate = DateTime.Now.AddDays(1),

        };
        public static Products ProductCreateTest => new()
        {
            Description = "Prodcut",
            Discount = 1,
            FinalPrice = 1,
            Name = "Name",
            Price = 1,
            StatusName = "Activo",
            Stock = 1,
            DateRegistration = DateTime.Now,
            DateUpdate = DateTime.Now.AddDays(1),

        };
        public static ProductsRequestDto ProductRequestDtoTest => new()
        {
            Description = "Prodcut",
            Name = "Name",
            Price = 1,
            Status = 1,
            Stock = 1
        };

        public static ProductsRequestDto ProductNoContentDtoTest => new()
        {
            Description = "Prodcut",
            Name = "Name",
            Price = 1,
            Status = 3,
            Stock = 1
        };
        public static ProductsRequestDto ProductBadRequestDtoTest => new()
        {
            Description = "Prodcut",
            Status = 1,
            Stock = 1
        };

        public static Products ProductUpdateTest => new()
        {
            ProductId = 1,
            Description = "Prodcut",
            Discount = 1,
            FinalPrice = 1,
            Name = "Name",
            Price = 1,
            StatusName = "Activo",
            Stock = 1,
            DateRegistration = DateTime.Now,
            DateUpdate = DateTime.Now.AddDays(1),

        };
        public static ProductsDto ProductDtoTest => new()
        {
            Description = "Prodcut",
            Discount = 1,
            FinalPrice = 1,
            Name = "Name",
            Price = 1,
            StatusName = "Activo",
            Stock = 1,
            DateRegistration = DateTime.Now,
            DateUpdate = DateTime.Now.AddDays(1),

        };

        public static Dictionary<int, string> StatusValues = new()
        {
            { 1, "Active" },
            { 0, "Inactive" }

        };
    }
}
