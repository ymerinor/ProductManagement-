namespace ProductManagement.Domain.Core
{
    public interface IProductStatusCache
    {
        Dictionary<int, string> SetProductStatus();
    }
}
