using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserMessages()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Id = new Guid(User.Identity.GetUserId());

                return View();
            }
            return View();
        }
    }
}
