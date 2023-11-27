using ProductManagement.Application.Common.Exeptions;
using System.Text.Json;

namespace ProductManagement.API.Middleware
{
    /// <summary>
    /// Middleware para el manejo centralizado de excepciones no controladas en la aplicación.
    /// Captura excepciones específicas, registra información y devuelve respuestas de error con detalles de la excepción.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Inicializa una nueva instancia del middleware de excepciones.
        /// </summary>
        /// <param name="next">El siguiente delegado de solicitud en la canalización.</param>
        /// <param name="logger">El registrador utilizado para registrar información sobre excepciones.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Invocado para manejar la solicitud y capturar excepciones no controladas.
        /// </summary>
        /// <param name="context">El contexto de la solicitud.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NoContentException ex)
            {
                await HandleNotFoundExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Maneja excepciones de tipo NoContentException y devuelve una respuesta de error específica.
        /// </summary>
        /// <param name="context">El contexto de la solicitud.</param>
        /// <param name="ex">La excepción capturada.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        private async Task HandleNotFoundExceptionAsync(HttpContext context, NoContentException ex)
        {
            _logger.LogInformation($"Recurso no encontrado: {ex.Message}");

            context.Response.StatusCode = 204;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                StatusCode = 404,
                Message = "Recurso no encontrado.",
                ExceptionType = ex.GetType().Name,
                ExceptionMessage = ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }

        /// <summary>
        /// Maneja otras excepciones y devuelve una respuesta de error genérica.
        /// </summary>
        /// <param name="context">El contexto de la solicitud.</param>
        /// <param name="ex">La excepción capturada.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError($"Se ha producido una excepción: {ex}");

            context.Response.StatusCode = GetStatusCodeForException(ex);
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ha ocurrido un error interno en el servidor.",
                ExceptionType = ex.GetType().Name,
                ExceptionMessage = ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }

        /// <summary>
        /// Obtiene el código de estado HTTP para una excepción específica.
        /// </summary>
        /// <param name="ex">La excepción para la cual se debe obtener el código de estado.</param>
        /// <returns>El código de estado HTTP.</returns>
        private int GetStatusCodeForException(Exception ex)
        {
            return 500; // Por defecto, devuelve un código de estado 500 para otras excepciones.
        }
    }

    /// <summary>
    /// Clase que representa la estructura de una respuesta de error que incluye detalles de la excepción.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Obtiene o establece el código de estado HTTP.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje de error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Obtiene o establece el tipo de la excepción.
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje de la excepción.
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Obtiene o establece la pila de llamadas de la excepción.
        /// </summary>
        public string StackTrace { get; set; }
    }

    /// <summary>
    /// Clase de extensión que proporciona un método para agregar el middleware de excepciones a la canalización de solicitud.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Agrega el middleware de excepciones a la canalización de solicitud.
        /// </summary>
        /// <param name="builder">El generador de aplicaciones.</param>
        /// <returns>El generador de aplicaciones con el middleware de excepciones agregado.</returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
