using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Services.TodoList;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodo _itodo;

        public TodoController(ITodo itodo)
        {
            _itodo = itodo;
        }

        [HttpPost("GetTodoList")]
        public IActionResult GetTodoList ([FromBody] TodoReq req)
        {
            try
            {
                var res = _itodo.GetTodoList(req);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveTodoList")]
        public IActionResult SaveTodoList ([FromBody] TodoItem req)
        {
            try
            {
                var res = _itodo.SaveTodoList(req);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DelTodoList")]
        public IActionResult DelTodoList ([FromBody] TodoItem req)
        {
            try
            {
                var res = _itodo.DelTodoList(req);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}