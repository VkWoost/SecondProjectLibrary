using Library.BLL.Services;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Library.ViewModels.ViewModels;
using Library.ViewModels.IdentityEnums;

namespace Library.WEB.Controllers
{
    public class BrochureController : Controller
    {
        BrochureService _brochureService;

        public BrochureController()
        {
            _brochureService = new BrochureService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult AddBrochure(BrochureViewModel brochureViewModel)
        {
            _brochureService.AddBrochure(brochureViewModel);
            return Json(brochureViewModel);
        }

        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult DeleteBrochure(int id)
        {
            _brochureService.DeleteBrochure(id);
            return RedirectToAction("GetBrochures");
        }

        [HttpPost]
        [Authorize(Roles = IdentityRolesViewModels.Admin)]
        public ActionResult BrochureEdit(BrochureViewModel brochureViewModel)
        {
            _brochureService.UpdateBrochure(brochureViewModel);
            return Json(brochureViewModel);
        }

        [HttpGet]
        public void SaveData(int? id)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(BrochureViewModel));
            BrochureViewModel brochure = _brochureService.GetBrochure(id.Value);
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, brochure);
                    xml = sww.ToString();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    doc.Save(Server.MapPath("~/uploads/brochure.xml"));
                }
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/uploads/brochure.xml"));
            string fileName = "brochure.xml";
            File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpGet]
        public ActionResult GetBrochures()
        {
            var brochures = _brochureService.GetBrochures();            
            return Json(brochures, JsonRequestBehavior.AllowGet);
        }
    }
}