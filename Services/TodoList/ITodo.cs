using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DataContext;
using Models.Extension;
using Models.Request;
using Models.Response;

namespace Services.TodoList
{
    public interface ITodo
    {
        public PagedResult<TodoItem> GetTodoList(TodoReq req);
        public Response SaveTodoList(TodoItem req);
        public Response DelTodoList(TodoItem req);
    }
}