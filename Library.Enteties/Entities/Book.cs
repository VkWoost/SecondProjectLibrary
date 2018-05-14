using System.Collections.Generic;

namespace Library.Entities.Entities
{
    public class Book : Basic
    {       
        public int YearOfPublication { get; set; }

        public int? AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<PublicationHouse> PublicationHouses { get; set; }
        public Book()
        {
            PublicationHouses = new List<PublicationHouse>();
        }
    }
}
