using AutoMapper;
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
            try
            {
                var result = await _productService.FilterByProductNameService(name);
                var resultToReturn = _mapper.Map<List<ProductDto>>(result);
                return Ok(resultToReturn);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        [HttpGet("FilterByPrice")]
        public async Task<ActionResult<List<ProductDto>>> FilterByPrice(decimal from, decimal to)
        {
            try
            {
                var result = await _productService.FilterProductByPriceService(from, to);
                var resultToReturn = _mapper.Map<List<ProductDto>>(result);
                return Ok(resultToReturn);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }

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
            try
            {
                var result = await _productService.FilterProductsService(name, from, to, productCode, productType, search);
                var resultToReturn = _mapper.Map<List<ProductDto>>(result);
                return Ok(resultToReturn);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }

        }

        [HttpGet("FilterByProductCode")]
        public async Task<ActionResult<List<ProductDto>>> FilterByPrice(string productCode)
        {
            try
            {
                var result = await _productService.FilterByProductProductCodeService(productCode);
                var resultToReturn = _mapper.Map<List<ProductDto>>(result);
                return Ok(resultToReturn);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }


        [HttpGet("FilterByProductType")]
        public async Task<ActionResult<List<ProductDto>>> FilterByType(ProductTypes productType)
        {
            try
            {
                var result = await _productService.FilterByProductProductTypeService(((ProductType)productType));
                var resultToReturn = _mapper.Map<List<ProductDto>>(result);
                return Ok(resultToReturn);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }

        }


    }
}
