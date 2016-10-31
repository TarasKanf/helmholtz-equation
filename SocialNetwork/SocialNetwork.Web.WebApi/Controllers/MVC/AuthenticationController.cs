using System.Web.Mvc;

namespace SocialNetwork.UI.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }
    }    
}