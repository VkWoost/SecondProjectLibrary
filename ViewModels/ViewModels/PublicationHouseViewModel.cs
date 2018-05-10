using System.Collections.Generic;

namespace ViewModels.ViewModels
{
    public class PublicationHouseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public ICollection<BookViewModel> Books { get; set; }

        public PublicationHouseViewModel()
        {
            Books = new List<BookViewModel>();
        }
    }
}
