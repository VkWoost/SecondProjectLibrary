using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return Redirect("/Book/Index/");
        }
         
        //protected override void Dispose(bool disposing)
        //{
        //    //_bookService.Dispose();
        //    //_authorService.Dispose();
        //    //_magazineService.Dispose();
        //    //service.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
