using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Application.Product.Validations;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _prodcutService;

        public ProductController(IProductService prodcutService)
        {
            _prodcutService = prodcutService;
        }

        // GET api/<ProductController>/5
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

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductsRequestDto products)
        {
            var validator = new ProductsRequestDtoValidator();
            var validationResult = validator.Validate(products);

            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            var productCreate = await _prodcutService.CreateAsync(products);
            return Ok(productCreate);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductsRequestDto products)
        {
            var productUpdate = await _prodcutService.UpdateAsync(id, products);
            return Ok(productUpdate);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}