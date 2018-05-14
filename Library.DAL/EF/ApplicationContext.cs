using Library.Enteties.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Library.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
