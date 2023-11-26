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
            StatusName = true,
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
            StatusName = true,
            Stock = 1,
            DateRegistration = DateTime.Now,
            DateUpdate = DateTime.Now.AddDays(1),

        };
        public static ProductsRequestDto ProductRequestDtoTest => new()
        {
            Description = "Prodcut",
            Name = "Name",
            Price = 1,
            StatusName = true,
            Stock = 1
        };
        public static ProductsRequestDto ProductBadRequestDtoTest => new()
        {
            Description = "Prodcut",
            StatusName = true,
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
            StatusName = true,
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
            StatusName = true,
            Stock = 1,
            DateRegistration = DateTime.Now,
            DateUpdate = DateTime.Now.AddDays(1),

        };
    }
}
