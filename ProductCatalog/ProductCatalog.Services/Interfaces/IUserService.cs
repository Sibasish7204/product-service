using ProductCatalog.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Services.Interfaces
{
    public interface IUserService
    {
        public Task Register(User user);
        public Task Login(User user);
    }
}
