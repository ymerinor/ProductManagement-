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
        /// <returns>El producto reci�n creado.</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductsRequestDto products)
        {
            var validator = new ProductsRequestDtoValidator(_productStatusCache);
            var validationResult = validator.Validate(products);

            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            var productCreate = await _prodcutService.CreateAsync(products);
            return Ok(productCreate);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">ID del producto a actualizar.</param>
        /// <param name="product">Nuevos datos del producto.</param>
        /// <returns>El producto actualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductsRequestDto products)
        {
            var productUpdate = await _prodcutService.UpdateAsync(id, products);
            return Ok(productUpdate);
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>Respuesta de confirmaci�n.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productRemove = await _prodcutService.RemoveAsync(id);
            return Ok(productRemove);
        }
    }
}