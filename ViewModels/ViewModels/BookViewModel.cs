using System.Collections.Generic;

namespace ViewModels.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfPublication { get; set; }
        public int? AuthorId { get; set; }
        public AuthorViewModel Author { get; set; }

        public ICollection<PublicationHouseViewModel> PublicationHouses { get; set; }

        public BookViewModel()
        {
            PublicationHouses = new List<PublicationHouseViewModel>();
        }
    }
}
