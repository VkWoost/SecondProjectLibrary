using System.Collections.Generic;

namespace Library.ViewModels.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookViewModel> Books { get; set; }

        public AuthorViewModel()
        {
            Books = new List<BookViewModel>();
        }
    }
}