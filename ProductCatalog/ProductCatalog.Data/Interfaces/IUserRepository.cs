using ProductCatalog.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByEmail(string email);
    }
}
