using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Services.ProductInterface;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _iproduct;
        public ProductController(IProduct iproduct)
        {
            _iproduct = iproduct;
        }

        [HttpPost("products")]
        public IActionResult SaveProduct (ProductReq req)
        {
            try
            {
                var res = _iproduct.SaveProduct(req);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("products/{category?}")]
        public IActionResult GetProduct (string category = null)
        {
            try
            {
                var res = _iproduct.GetProduct(category);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}