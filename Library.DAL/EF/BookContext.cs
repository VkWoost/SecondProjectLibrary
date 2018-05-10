using Library.Enteties.Entities;
using System.Data.Entity;

namespace Library.DAL.EF
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<PublicationHouse> PublicationHouses { get; set; }

        public BookContext(string connectionString) : base(connectionString)
        {}

        public BookContext() : base("DefaultConnection")
        {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PublicationHouse>().HasMany(x => x.Books)
                .WithMany(x => x.PublicationHouses)
                .Map(x => x.MapLeftKey("PublicationHouse_Id")
                .MapRightKey("Book_Id")
                .ToTable("PublicationHouseBooks"));
        }
    }
}
