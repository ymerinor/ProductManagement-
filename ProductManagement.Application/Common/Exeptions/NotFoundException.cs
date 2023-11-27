namespace ProductManagement.Application.Common.Exeptions
{
    /// <summary>
    /// Excepción lanzada cuando no se encuentra un recurso.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="NotFoundException"/> con un mensaje de error.
        /// </summary>
        /// <param name="message">Mensaje de error que describe la excepción.</param>
        public NotFoundException(string message)
        : base(message)
        {
        }
    }
}
