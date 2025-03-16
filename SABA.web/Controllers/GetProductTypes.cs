using Microsoft.AspNetCore.Mvc;
using SABA.Services.Models.Enum;

namespace SABA.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProductTypes : ControllerBase
    {
        [HttpGet("GetProductTypesForMainPage")]
        public ActionResult getProductTypes()
        {
            var types = Enum.GetValues(typeof(ProductType))
                            .Cast<ProductType>()
                            .ToDictionary(t => t.ToString(), t => (int)t);

            return Ok(types);
        }
    }
}
