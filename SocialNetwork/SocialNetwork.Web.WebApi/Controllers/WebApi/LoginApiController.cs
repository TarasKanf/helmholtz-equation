using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services;
using SocialNetwork.Services.Identity;
using System.IO;

namespace SocialNetwork.Web.WebApi.Controllers.WebApi
{
    public class LoginApiController : ApiController
    {
        private readonly IdentityUserManager userManager = new IdentityUserManager(new UnitOfWork().Users);

        public LoginApiController(AuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        private AuthenticationService AuthenticationService { get; }

        private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

        [HttpGet]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            LogOut();
            string urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            return Redirect(urlBase);            
        }


        [HttpPut]
        [Route("LoginApi/ExternalLogout")]
        public void LogoutExternal([FromBody] string sessionKey)
        {
            LogOut();    
            AuthenticationService.LogOut(sessionKey);
        }


        [HttpPost]
        [Route("LoginApi/Login")]
        public async Task<IHttpActionResult> Login([FromBody] LoginDTO model)
        {
            string urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var rightPartUrl = "/Authentication/Login";
            if (!ModelState.IsValid)
                return Redirect(urlBase + rightPartUrl);

            var user = await userManager.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                SignIn(user);
                rightPartUrl = "";
            }

            AuthenticationService.LogIn(user.Email, user.HashPassword);


            return Redirect(urlBase + rightPartUrl);
        }

        [HttpPost]
        [Route("LoginApi/ExternalLogin")]
        public SessionInfo LoginExternal([FromBody] LoginDTO model)
        {
            // UserModel returnUser = null;           
            if (!ModelState.IsValid)
                return null;

            var user = userManager.FindAsync(model.UserName, model.Password).Result;
            if (user != null)
            {
                SignIn(user);
                string sessionKey = AuthenticationService.LogIn(user.Email, user.HashPassword);

                var session = AuthenticationService.GetSession(sessionKey);
                return session;
            }

            return null;
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded)
                return null;

            if (result.Errors != null)
            {
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }

        private void SignIn(User user)
        {
            var claims = new List<Claim>(); //містить інформацію про юзера
            claims.Add(new Claim(ClaimTypes.Name, user.UserName)); //додаємо логін юзера
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer)); //додаємо ід
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name)); //додаємо роль
            }

            var id = new ClaimsIdentity(claims,
                DefaultAuthenticationTypes.ApplicationCookie);
            Authentication.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, id);
        }

       
        private void LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
        }
    }
}