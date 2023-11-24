using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Product.Interfaces;

namespace ProductManagement.Controllers
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}