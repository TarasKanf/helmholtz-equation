using Microsoft.AspNet.Identity;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.UI.Web.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            var userSerchingServ = new UserSearchingService();
            User user = userSerchingServ.SearchById(new Guid(User.Identity.GetUserId()));
            return View(new UserModel(user));
        }

        public PartialViewResult _UserLayout()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.RoleAdmin = User.IsInRole(@"Admin");
                ViewBag.RoleUser = User.IsInRole(@"User");
                ViewBag.Id = new Guid(User.Identity.GetUserId());  
                ViewBag.Name = User.Identity.Name;
                ViewBag.IsAuthenticated = true;
                return PartialView("_UserLayout");
            }

            ViewBag.IsAuthenticated = false;
            return PartialView("_UserLayout");
        }

        public PartialViewResult UserMessenger()
        {
            return PartialView("UserMessenger");
        }
    }
}