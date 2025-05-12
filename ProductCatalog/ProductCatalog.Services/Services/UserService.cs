using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Interfaces;
using ProductCatalog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task Login(User user)
        {
            throw new NotImplementedException();
        }

        public async Task Register(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            else
            {
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
