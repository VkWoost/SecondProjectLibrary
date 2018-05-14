using Library.Enteties.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace Library.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}
