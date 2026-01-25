using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Data.DataContext;
using Models.Extension;
using Models.Request;
using Models.Response;

namespace Services.TodoList
{
    public class TodoService : ITodo
    {
        private readonly DataContext _data;
        public TodoService(DataContext data)
        {
            _data = data;
        }

        public PagedResult<TodoItem> GetTodoList(TodoReq req)
        {
            // กันค่า page พัง
            var page = req.Page <= 0 ? 1 : req.Page;
            var pageSize = req.PageSize <= 0 ? 10 : req.PageSize;

            var query = _data.TodoItems.AsQueryable();

            // 🔍 Filters
            // if (req.Id > 0)
            // {
            //     query = query.Where(x => x.Id == req.Id);
            // }

            // if (!string.IsNullOrWhiteSpace(req.Name))
            // {
            //     query = query.Where(x => x.Name.Contains(req.Name));
            // }

            // if (req.StatusId.HasValue)
            // {
            //     query = query.Where(x => x.StatusId == req.StatusId);
            // }

            // if (!string.IsNullOrWhiteSpace(req.Owner))
            // {
            //     query = query.Where(x => x.Owner.Contains(req.Owner));
            // }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query
                .OrderBy(x => x.Id) // สำคัญมากกับ pagination
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TodoItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner,
                    StatusId = x.StatusId
                })
                .ToList();

            return new PagedResult<TodoItem>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize
            };
        }

        public Response SaveTodoList(TodoItem req)
        {
            using var trans = _data.Database.BeginTransaction();
            try
            {
                var addition = _data.TodoItems.FirstOrDefault(x => x.Id == req.Id);

                if(addition == null)
                {
                    var add = new TodoItem
                    {
                        Name = req.Name,
                        Owner = req.Owner,
                        StatusId = req.StatusId
                    };
                    _data.TodoItems.Add(add);
                }
                else
                {
                    addition.Name = req.Name;
                    addition.StatusId = req.StatusId;
                    addition.Owner = req.Owner;

                    _data.TodoItems.Update(addition);
                }
                
                _data.SaveChanges();

                trans.Commit();

                return new Response
                {
                    Code = 200,
                    Message = "Save Successfully",
                    Result = true
                };
            }
            catch (Exception ex)
            {
                trans.Rollback();

                return new Response
                {
                    Code = 500,
                    Message = $"Failed! {ex.Message}",
                    Result = false
                };
            }
        }

        public Response DelTodoList(TodoItem req)
        {
            using var trans = _data.Database.BeginTransaction();
            try
            {
                var deletion = _data.TodoItems.FirstOrDefault(x => x.Id == req.Id);

                if(deletion != null)
                {
                    _data.Remove(deletion);
                }
                
                _data.SaveChanges();

                trans.Commit();

                return new Response
                {
                    Code = 200,
                    Message = "Delete Successfully",
                    Result = true
                };
            }
            catch (Exception ex)
            {
                trans.Rollback();

                return new Response
                {
                    Code = 500,
                    Message = $"Failed! {ex.Message}",
                    Result = false
                };
            }
        }
    }
}