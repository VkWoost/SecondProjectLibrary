using System.Collections.Generic;

namespace Library.Entities.Entities
{
    public class Author : BaseEntity
    {
        public ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
