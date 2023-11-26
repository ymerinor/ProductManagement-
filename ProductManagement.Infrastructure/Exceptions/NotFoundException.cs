namespace ProductManagement.Infrastructure.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
        : base(message)
        {
        }
    }
}
