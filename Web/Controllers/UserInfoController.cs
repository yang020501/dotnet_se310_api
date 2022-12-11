using BLL.DTOs.Users;
using BLL.Exceptions;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Shared;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserInfoController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [Authorize]
        [HttpGet, Route("info")]
        public IActionResult GetUserInformation()
        {
            try
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var sid = claimIdentity?.FindFirst(ClaimTypes.Sid);

                var resultSid = sid?.Value is null ? null : sid.Value;
                if (resultSid != null)
                {
                    return Ok(_userServices.GetUserById(resultSid));
                }

                return NotFound("Can't found this user information");
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpPatch, Route("info/update-info")]
        public IActionResult UpdateUserInfo(UpdateInfoRequest request)
        {
            try
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var sid = claimIdentity?.FindFirst(ClaimTypes.Sid);

                var resultSid = sid?.Value is null ? null : sid.Value;
                if (resultSid != null)
                {
                    return Ok(_userServices.UpdateUserInfo(request));
                }

                return Unauthorized("You have no right to change this user info");
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpPatch, Route("info/change-password")]
        public IActionResult ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var sid = claimIdentity?.FindFirst(ClaimTypes.Sid);

                var resultSid = sid?.Value is null ? null : sid.Value;
                if (resultSid != null)
                {
                    return Ok(_userServices.ChangePassword(request));
                }

                return Unauthorized("You have no right to change this user password");
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete, Route("users/{id:Guid}")]
        public IActionResult DeleteUser(Guid? id)
        {
            try
            {
                return Ok(_userServices.DeleteUserById(id));
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
