using Library.BLL.Services;
using System.Configuration;
using System.Web.Mvc;
using ViewModels.ViewModels;

namespace Library.WEB.Controllers
{
    public class AuthorController : Controller
    {
        AuthorService _authorService;

        public AuthorController()
        {
            _authorService = new AuthorService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddAuthor(AuthorViewModel authorViewModel)
        {
            _authorService.AddAuthor(authorViewModel);
            return Json(authorViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteAuthor(int id)
        {
            _authorService.DeleteAuthor(id);
            return RedirectToAction("GetAuthors");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AuthorsEdit(AuthorViewModel authorViewModel)
        {
            _authorService.UpdateAuthor(authorViewModel);
            return Json(authorViewModel);
        }

        [HttpGet]
        public ActionResult GetAuthors()
        {
            var authors = _authorService.GetAuthors();
            return Json(authors, JsonRequestBehavior.AllowGet);
        }
    }
}