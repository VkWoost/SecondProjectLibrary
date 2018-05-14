using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
