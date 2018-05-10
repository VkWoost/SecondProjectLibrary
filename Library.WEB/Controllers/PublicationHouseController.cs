using Library.BLL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.ViewModels;

namespace Library.WEB.Controllers
{
    public class PublicationHouseController : Controller
    {
        PublicationHouseService _publicationHouseService;
        BookService _bookService;

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
        [Authorize(Roles = "admin")]
        public ActionResult AddPublicationHouse(PublicationHouseViewModel publicationHouseViewModel)
        {
            _publicationHouseService.AddPublicationHouse(publicationHouseViewModel);
            return Json(publicationHouseViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeletePublicationHouse(int id)
        {
            _publicationHouseService.DeletePublicationHouse(id);
            return RedirectToAction("GetPublicationHouses");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
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