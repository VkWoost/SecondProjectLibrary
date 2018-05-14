using Library.Enteties.Entities;
using System.Collections.Generic;

namespace Library.Entities.Entities
{
    public class Author : Basic
    {
        public ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
