using Microsoft.AspNetCore.Mvc;
using SABA.Services.Abstractiuons.ProductSaleService;
using SABA.Services.Models.RequestModels.Products;
using SABA.Services.Models.ResponseModels.ProductsAndSales;

namespace SABA.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductSaleService _service;
        public ProductsController(IProductSaleService servece)
        {
            _service = servece;
        }

        [HttpPost("Addproducts")]
        public async Task<ActionResult> AddProducts(ProductDto product)
        {
            try
            {
                await _service.AddProduct(product);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetProducts")]
        public async Task<ActionResult<GetProductsResponse>> GetProducts()
        {
            try
            {
                var result = await _service.GetProducts();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }


        //[HttpGet("GetProductById")]
        //public async Task<ActionResult<ProductDto>> GetProductById(int id)
        //{
        //    var result = await _service.GetById(id);
        //    if (result.Products == null)
        //        return NotFound(result);

        //    return Ok(result);
        //}

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<UpdateProductResponse>> UpdateProduct(ProductDto product, int Id)
        {
            try
            {
                var response = await _service.UpdateProduct(product, Id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
