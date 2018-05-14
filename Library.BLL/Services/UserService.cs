using Library.BLL.Infrastructure;
using Library.BLL.Interfaces;
using Library.Enteties.IdentityEntities;
using Library.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.ViewModels.IdentityViewModels;
using Library.Enteties.IdentityEnums;

namespace Library.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWorkIdentity Database { get; set; }

        public UserService(IUnitOfWorkIdentity uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserViewModel userViewModel)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userViewModel.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userViewModel.Email, UserName = userViewModel.Email };
                var result = await Database.UserManager.CreateAsync(user, userViewModel.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                userViewModel.Role = IdentityRoles.user.ToString();
                await Database.UserManager.AddToRoleAsync(user.Id, userViewModel.Role);              
                ClientProfile clientProfile = new ClientProfile { Id = user.Id };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration successful", "");
            }
            else
            {
                return new OperationDetails(false, "User with such login already exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserViewModel userViewModel)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userViewModel.Email, userViewModel.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
