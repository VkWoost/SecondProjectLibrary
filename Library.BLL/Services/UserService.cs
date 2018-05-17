using Library.BLL.Infrastructure;
using Library.BLL.Interfaces;
using Library.Entities.IdentityEntities;
using Library.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.ViewModels.IdentityViewModels;
using Library.Entities.IdentityEnums;
using System.Collections.Generic;

namespace Library.BLL.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWorkIdentity Database { get; set; }

        public UserService(IUnitOfWorkIdentity unitOfWork)
        {
            Database = unitOfWork;
        }

        public async Task<OperationDetails> Create(UserViewModel userViewModel)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userViewModel.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userViewModel.Email, UserName = userViewModel.Email };
                var result = await Database.UserManager.CreateAsync(user, userViewModel.Password);
                if (result.Errors.Count() > 0)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }
                if (userViewModel.Role != "admin")
                {
                    userViewModel.Role = IdentityRoles.user.ToString();
                }
                await Database.UserManager.AddToRoleAsync(user.Id, userViewModel.Role);              
                ClientProfile clientProfile = new ClientProfile { Id = user.Id };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration successful", "");
            }
            if (user != null)
            {
                return new OperationDetails(false, "User with such login already exists", "Email");
            }
            return new OperationDetails(false, "Error while registering this email: ", "Email");
        }

        public async Task<ClaimsIdentity> Authenticate(UserViewModel userViewModel)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userViewModel.Email, userViewModel.Password);
            if (user != null)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task SetInitialData(UserViewModel admin, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(admin);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
