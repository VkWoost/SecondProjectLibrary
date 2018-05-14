using System.Collections.Generic;

namespace Library.Entities.Entities
{
    public class PublicationHouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public ICollection<Book> Books { get; set; }
        public PublicationHouse()
        {
            Books = new List<Book>();
        }
    }
}
