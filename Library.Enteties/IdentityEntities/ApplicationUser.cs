using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.Enteties.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
