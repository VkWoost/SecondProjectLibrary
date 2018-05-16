using Library.BLL.Services;
using System.Configuration;
using System.Web.Mvc;
using Library.ViewModels.ViewModels;
using Library.ViewModels.IdentityEnums;

namespace Library.WEB.Controllers
{
    public class PublicationHouseController : Controller
    {
        private PublicationHouseService _publicationHouseService;
        private BookService _bookService;

        public PublicationHouseController()
        {
            _publicationHouseService = new PublicationHouseService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _bookService = new BookService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult AddPublicationHouse(PublicationHouseViewModel publicationHouseViewModel)
        {
            _publicationHouseService.AddPublicationHouse(publicationHouseViewModel);
            return Json(publicationHouseViewModel);
        }

        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult DeletePublicationHouse(int id)
        {
            _publicationHouseService.DeletePublicationHouse(id);
            return RedirectToAction("GetPublicationHouses");
        }

        [HttpPost]
        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult PublicationHouseEdit(PublicationHouseViewModel publicationHouseViewModel)
        {
            _publicationHouseService.UpdatePublicationHouse(publicationHouseViewModel);
            return Json(publicationHouseViewModel);
        }

        [HttpGet]
        public ActionResult GetPublicationHouses()
        {
            var products = _publicationHouseService.GetPublicationHouses();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}