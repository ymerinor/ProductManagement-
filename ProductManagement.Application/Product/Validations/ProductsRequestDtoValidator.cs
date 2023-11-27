using FluentValidation;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Domain.Core;

namespace ProductManagement.Application.Product.Validations
{
    public class ProductsRequestDtoValidator : AbstractValidator<ProductsRequestDto>
    {
        private readonly IProductStatusCache _productStatusCache;
        public ProductsRequestDtoValidator(IProductStatusCache productStatusCache)
        {
            _productStatusCache = productStatusCache ?? throw new ArgumentNullException(nameof(productStatusCache));

            RuleFor(x => x.Name).NotEmpty().WithMessage("El campo 'Name' es obligatorio.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("El campo 'Description' es obligatorio.");
            RuleFor(x => x.Price).NotNull().WithMessage("El campo 'Price' no puede ser nulo.");
            // Agregar regla de validación para StatusName
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("El campo 'StatusName' es obligatorio.")
                .Must(statusName =>
                {
                    // Obtener estados del producto desde el caché
                    var productStatus = _productStatusCache.SetProductStatus();

                    // Verificar si el StatusName se encuentra en los estados del producto
                    return productStatus.ContainsKey(statusName);
                })
                .WithMessage("El campo 'StatusName' no es válido.");
        }
    }
}

