using System.Collections.Generic;

namespace Library.Entities.Entities
{
    public class PublicationHouse : BaseEntity
    {
        public string Adress { get; set; }

        public ICollection<Book> Books { get; set; }
        public PublicationHouse()
        {
            Books = new List<Book>();
        }
    }
}
