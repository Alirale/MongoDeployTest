using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MongoDeployTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            var product = await _productRepository.GetById(id);
            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductViewModel value)
        {
            var product = new Product(value.Description);
            _productRepository.Add(product);
            var testProduct = await _productRepository.GetById(product.Id);
            return Ok(testProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(Guid id, [FromBody] ProductViewModel value)
        {
            var product = new Product(id, value.Description);

            _productRepository.Update(product);

            return Ok(await _productRepository.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _productRepository.Remove(id);
            return Ok();
        }
    }
}