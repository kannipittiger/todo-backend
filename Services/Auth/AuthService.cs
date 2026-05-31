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

        public Response login ()
        {
            return null;
        }
    }
}
