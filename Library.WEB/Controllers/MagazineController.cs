using Library.BLL.Services;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Library.ViewModels.ViewModels;
using Library.ViewModels.IdentityEnums;

namespace Library.WEB.Controllers
{
    public class MagazineController : Controller
    {
        private MagazineService _magazineService;

        public MagazineController()
        {
            _magazineService = new MagazineService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = nameof(IdentityRolesViewModels.Admin))]
        public ActionResult AddMagazine(MagazineViewModel magazineViewModel)
        {
            _magazineService.AddMagazine(magazineViewModel);
            return Json(magazineViewModel);
        }

        [Authorize(Roles = nameof(IdentityRolesViewModels.Admin))]
        public ActionResult DeleteMagazine(int id)
        {
            _magazineService.DeleteMagazine(id);
            return RedirectToAction("GetMagazines");
        }
        
        [HttpPost]
        [Authorize(Roles = nameof(IdentityRolesViewModels.Admin))]
        public ActionResult MagazineEdit(MagazineViewModel magazineViewModel)
        {
            _magazineService.UpdateMagazine(magazineViewModel);
            return Json(magazineViewModel);
        }

        [HttpGet]
        public void SaveData(int? id)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(MagazineViewModel));
            MagazineViewModel magazine = _magazineService.GetMagazine(id.Value);
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, magazine);
                    xml = sww.ToString();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    doc.Save(Server.MapPath("~/uploads/magazine.xml"));
                }
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/uploads/magazine.xml"));
            string fileName = "magazine.xml";
            File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpGet]
        public ActionResult GetMagazines()
        {
            var magazines = _magazineService.GetMagazines();
            return Json(magazines, JsonRequestBehavior.AllowGet);
        }
    }
}