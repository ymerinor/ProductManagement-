using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Application.Product.Validations;
using ProductManagement.Domain.Core;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _prodcutService;
        private readonly IProductStatusCache _productStatusCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodcutService"></param>
        /// <param name="productStatusCache"></param>
        public ProductController(IProductService prodcutService, IProductStatusCache productStatusCache)
        {
            _prodcutService = prodcutService;
            _productStatusCache = productStatusCache;
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>El producto con el ID especificado.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _prodcutService.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="product">Datos del nuevo producto.</param>
        /// <returns>El producto recién creado.</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductsRequestDto product)
        {
            var validator = new ProductsRequestDtoValidator(_productStatusCache);
            var validationResult = validator.Validate(product);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var productCreate = await _prodcutService.CreateAsync(product);
            return Ok(productCreate);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">ID del producto a actualizar.</param>
        /// <param name="product">Nuevos datos del producto.</param>
        /// <returns>El producto actualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductsRequestDto product)
        {
            var validator = new ProductsRequestDtoValidator(_productStatusCache);
            var validationResult = validator.Validate(product);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var productUpdate = await _prodcutService.UpdateAsync(id, product);
            return Ok(productUpdate);
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>Respuesta de confirmación.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productRemove = await _prodcutService.RemoveAsync(id);
            return Ok(productRemove);
        }
    }
}