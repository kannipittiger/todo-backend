using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.ProductInterface;
using Models.Request;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {
        private readonly ILogger<Auth> _logger;
        private readonly IAuth _iauth;

        public Auth(ILogger<Auth> logger , IAuth iauth)
        {
            _logger = logger;
            _iauth = iauth;
        }

        [HttpPost("login")]
        public IActionResult GetTodoList ([FromBody] Models.Request.Auth auth)
        {
            try
            {
                var res = _iauth.Login(auth);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}