using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.DAL.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
