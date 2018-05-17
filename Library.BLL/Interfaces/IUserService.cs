using Library.BLL.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.ViewModels.IdentityViewModels;
using System.Collections.Generic;

namespace Library.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserViewModel userViewModel);
        Task<ClaimsIdentity> Authenticate(UserViewModel userViewModel);
        Task SetInitialData(UserViewModel admin, List<string> roles);
    }
}
