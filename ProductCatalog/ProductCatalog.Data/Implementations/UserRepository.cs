using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.DbContext;
using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<User> _user;
        public UserRepository(ApplicationDbContext context )
        {
            _context = context;
            _user = _context.Set<User>();
        }
        public Task<User> GetUserByEmail(string email)
        {
            var user = _user.FirstAsync(u=>u.Email == email);
            if(user == null) { return null; }
            return user;
        }
    }
}
