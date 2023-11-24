namespace ProductManagement.Domain.Product
{
    public class Products
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool StatusName { get; set; }
        public decimal Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
