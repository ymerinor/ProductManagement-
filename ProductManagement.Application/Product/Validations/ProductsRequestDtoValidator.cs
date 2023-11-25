using FluentValidation;
using ProductManagement.Application.Product.Dto;

namespace ProductManagement.Application.Product.Validations
{
    public class ProductsRequestDtoValidator : AbstractValidator<ProductsRequestDto>
    {
        public ProductsRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El campo 'Name' es obligatorio.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("El campo 'Description' es obligatorio.");
            RuleFor(x => x.Price).NotNull().WithMessage("El campo 'Price' no puede ser nulo.");
        }
    }
}
