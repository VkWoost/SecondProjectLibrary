using System.Web.Mvc;
using Library.ViewModels.ViewModels;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Library.BLL.Services;
using System.Configuration;
using Library.ViewModels.IdentityEnums;

namespace Library.WEB.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        private AuthorService _authorService;
        private PublicationHouseService _publicationHouseService;

        public BookController()
        {          
            _bookService = new BookService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _authorService = new AuthorService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _publicationHouseService = new PublicationHouseService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult AddBook(BookViewModel bookViewModel)
        {
            bookViewModel.Author = _authorService.GetAuthor(bookViewModel.AuthorId);
            _bookService.AddBook(bookViewModel);
            return Json(bookViewModel);
        }

        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction("/Book/Getbooks");
        }

        [HttpPost]
        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult BookEdit(BookViewModel bookViewModel)
        {
            bookViewModel.Author = _authorService.GetAuthor(bookViewModel.AuthorId);
            _bookService.UpdateBook(bookViewModel);
            return Json(bookViewModel);
        }

        //[HttpGet]
        //public void SaveData(int? id)
        //{
        //    XmlSerializer xsSubmit = new XmlSerializer(typeof(BookViewModel));
        //    BookViewModel book = _bookService.GetBook(id.Value);
        //    var xml = "";

        //    using (var sww = new StringWriter())
        //    {
        //        using (XmlWriter writer = XmlWriter.Create(sww))
        //        {
        //            xsSubmit.Serialize(writer, book);
        //            xml = sww.ToString();

        //            XmlDocument doc = new XmlDocument();
        //            doc.LoadXml(xml);
        //            doc.Save(Server.MapPath("~/uploads/book.xml"));
        //        }
        //    }
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/uploads/book.xml"));
        //    string fileName = "book.xml";
        //    File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}

        [HttpGet]
        public ActionResult GetBooks()
        {
            var books = _bookService.GetBooks();
            return Json(books, JsonRequestBehavior.AllowGet);
        }
    }
}
