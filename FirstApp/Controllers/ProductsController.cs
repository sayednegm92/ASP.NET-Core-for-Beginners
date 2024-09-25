using FirstApp.DTO;
using FirstApp.Filters;
using FirstApp.Model;
using FirstApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service,ILogger<ProductsController> logger) {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var result=await _service.GetAllProducts();
            return Ok(result);
        }
        [HttpGet]
        //[Route("GetProductById/{id}")]
        [Route("GetProductById")]//From Query String
        [LogSensitiveAction]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogDebug("Getting Product By Id #{id}",id);
            var result = await _service.GetProductById(id);
            if (result == null) { 
                _logger.LogWarning("Product Not Found #{id} ---time {time}",id,DateTime.Now);
                return NotFound();
            }
           else return Ok(result);
        }
        [HttpPost]
        [Route("")]
        //using Prefix
        // ?p1.name=Honor x9b &p1.sku=50241 & p2.name=Nokia 6020 &p2.sku=777
        public async Task<IActionResult> CreateProduct([FromQuery(Name ="p1")] Product product,
            [FromQuery(Name ="p2")] Product product2)
        {
            var res = await _service.AddProduct(product);
            return Ok(res);
        }
        ////?name=Honor x9b &sku=50241
        //public async Task<IActionResult> CreateProduct([FromQuery] Product product)
        //{
        //    var res = await _service.AddProduct(product);
        //    return Ok(res);
        //}
        //public async Task<IActionResult> CreateProduct([FromBody]Product product)
        //{
        //    var res = await _service.AddProduct(product);
        //    return Ok(res);
        //}
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var res = await _service.EditProduct(product);
            return Ok(res);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var res = await _service.RemoveProduct(id);
            return Ok(res);
        }
    }
}
/*--------------------Log Level-------------------*/
/*
1-Critical  or Fetal Log Level
2-Error Log Level
3-Warning Log Level
4-Information Log Level
5-Debug Log Level
6-Trace Log Level
*/
/*--------------------Model Binding-------------------*/
/*
[FromBody] => Default
[FromQuery]
[FromRoute]
[FromForm]
[FromHeader]
*/
