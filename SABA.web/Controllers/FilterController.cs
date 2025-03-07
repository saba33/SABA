using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SABA.Core.Models.Enums;
using SABA.Services.Abstractiuons.Filtration;
using SABA.Services.Models.Enum;
using SABA.Services.Models.RequestModels.Products;

namespace SABA.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FilterController : ControllerBase
    {
        private readonly IFilterProductService _productService;
        private readonly IMapper _mapper;
        public FilterController(IFilterProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("FilterByName")]
        public async Task<ActionResult<List<ProductDto>>> FilterByName(string name)
        {
            var result = await _productService.FilterByProductNameService(name);
            var resultToReturn = _mapper.Map<List<ProductDto>>(result);
            return Ok(resultToReturn);
        }

        [HttpGet("FilterByPrice")]
        public async Task<ActionResult<List<ProductDto>>> FilterByPrice(decimal from, decimal to)
        {
            var result = await _productService.FilterProductByPriceService(from, to);
            var resultToReturn = _mapper.Map<List<ProductDto>>(result);
            return Ok(resultToReturn);
        }
        [HttpGet("Filter")]
        public async Task<ActionResult<List<ProductDto>>> Filter(
         string name = null,
         decimal? from = null,
         decimal? to = null,
         string productCode = null,
         ProductTypes? productType = null,
         string search = null)
        {
            var result = await _productService.FilterProductsService(name, from, to, productCode, productType, search);

            var resultToReturn = _mapper.Map<List<ProductDto>>(result);
            return Ok(resultToReturn);
        }

        [HttpGet("FilterByProductCode")]
        public async Task<ActionResult<List<ProductDto>>> FilterByPrice(string productCode)
        {
            var result = await _productService.FilterByProductProductCodeService(productCode);
            var resultToReturn = _mapper.Map<List<ProductDto>>(result);
            return Ok(resultToReturn);
        }


        [HttpGet("FilterByProductType")]
        public async Task<ActionResult<List<ProductDto>>> FilterByType(ProductTypes productType)
        {
            var result = await _productService.FilterByProductProductTypeService(((ProductType)productType));
            var resultToReturn = _mapper.Map<List<ProductDto>>(result);
            return Ok(resultToReturn);
        }


    }
}
