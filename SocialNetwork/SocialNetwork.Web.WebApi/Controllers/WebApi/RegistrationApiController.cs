using Microsoft.AspNet.Identity;
using SocialNetwork.Services;
using SocialNetwork.Domain.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SocialNetwork.Web.WebApi.Controllers
{
    public class RegistrationApiController : ApiController
    {
        [Route("RegistrationApi/Register")]
        public async Task<IHttpActionResult> Register([FromBody] RegistrationDTO model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            IdentityResult result = await RegistrationService.Registration(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
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

            return null;
        }
    }
}
