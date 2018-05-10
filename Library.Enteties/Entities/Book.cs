﻿using System.Collections.Generic;

namespace Library.Enteties.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
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