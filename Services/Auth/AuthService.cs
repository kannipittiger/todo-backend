using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DataContext;
using Models.Request;
using Models.Response;
using Services.ProductInterface;

namespace Services.Product
{
    public class AuthService : IAuth
    {
        private readonly DataContext _data;
        public AuthService(DataContext data)
        {
            _data = data;
        }

        public Response Login(Auth auth)
        {
            var user = _data.TodoUsers
                .FirstOrDefault(x =>
                    x.UserName == auth.User &&
                    x.Password == auth.Password);

            if (user == null)
            {
                return new Response
                {
                    Code = 400,
                    Message = "Username or Password is not valid."
                };
            }

            return new Response
            {
                Code = 200,
                IsSuccess = true,
                Message = "Login Success",
            };
        }
    }
}
