using Library.BLL.Interfaces;
using Library.BLL.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ViewModels.ViewModels;

namespace Library.WEB.Controllers
{
    public class AdminController : Controller
    {
        BookService _bookService;
        AuthorService _authorService;
        MagazineService _magazineService;
        BrochureService _brochureService;
        PublicationHouseService _publicationHouseService;

        public AdminController()
        {
            _bookService = new BookService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _authorService = new AuthorService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _magazineService = new MagazineService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _brochureService = new BrochureService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _publicationHouseService = new PublicationHouseService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return Redirect("/Admin/BookIndex/");
        }

        [Authorize(Roles = "admin")]
        public ActionResult BookIndex()
        {
            IEnumerable<BookViewModel> books = _bookService.GetBooks();
            foreach (var item in books)
            {
                item.Author = _authorService.GetAuthor(item.AuthorId);
            }
            return View(books);
        }

        [Authorize(Roles ="admin")]
        public ActionResult AuthorIndex()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult MagazineIndex()
        {
            IEnumerable<MagazineViewModel> magazines = _magazineService.GetMagazines();
            return View(magazines);
        }

        [Authorize(Roles = "admin")]
        public ActionResult BrochureIndex()
        {
            IEnumerable<BrochureViewModel> brochures = _brochureService.GetBrochures();
            return View(brochures);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminAuthorDetails(int? id)
        {
            IEnumerable<BookViewModel> books = _bookService.GetBooks().Where(c => c.AuthorId == id).ToList();
            return View(books);
        }

        [Authorize(Roles = "admin")]
        public ActionResult PublicationHouseIndex()
        {
            IEnumerable<PublicationHouseViewModel> publicationHouses = _publicationHouseService.GetPublicationHouses();
            return View(publicationHouses);
        }
    }
}