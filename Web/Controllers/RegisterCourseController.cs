using BLL.DTOs.RegisterCourses;
using BLL.Exceptions;
using BLL.Services;
using DAL.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Shared;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/register-course")]
    [ApiController]
    public class RegisterCourseController : ControllerBase
    {
        private readonly IRegisterCourseService _registerCourseService;

        public RegisterCourseController(IRegisterCourseService registerCourseService)
        {
            _registerCourseService = registerCourseService;
        }

        [Authorize(Roles = "student")]
        [HttpPost, Route("register")]
        public IActionResult RegisterCourse([FromBody] RegisterCourseRequest registerRequest)
        {
            try
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var sid = claimIdentity?.FindFirst(ClaimTypes.Sid);

                var resultSid = sid?.Value is null ? null : sid.Value;
                if (resultSid != null)
                {
                    List<Course>? error_list = _registerCourseService.CheckRegisterCourse(registerRequest, resultSid);
                    if(error_list is not null && error_list.Count() > 0)
                    {
                        return StatusCode(409, error_list);
                    }
                    else
                    {
                        return Ok(_registerCourseService.RegisterCourseForStudent(registerRequest, resultSid));
                    }
                }
                else
                {
                    return BadRequest("User id is invalided");
                }
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "student")]
        [HttpPost, Route("cancel-course")]
        public IActionResult CancelRegistedCourse([FromBody] CancelRegistedCourseRequest cancelRequest)
        {
            try
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var sid = claimIdentity?.FindFirst(ClaimTypes.Sid);

                var resultSid = sid?.Value is null ? null : sid.Value;
                if (resultSid != null)
                {                
                    return Ok(_registerCourseService.CancelRegistedCourse(cancelRequest, resultSid));                
                }
                else
                {
                    return BadRequest("User id is invalided");
                }
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "student")]
        [HttpGet, Route("registed-course")]
        public IActionResult GetRegistedCourseOfStudent()
        {
            try
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var sid = claimIdentity?.FindFirst(ClaimTypes.Sid);

                var resultSid = sid?.Value is null ? null : sid.Value;
                if (resultSid != null)
                {
                    return Ok(_registerCourseService.GetRegistedCourseOfStudent(resultSid));
                }
                else
                {
                    return BadRequest("User id is invalided");
                }
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "student,mod")]
        [HttpGet, Route("available-courses")]
        public IActionResult GetAvailableCourses()
        {
            try
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var sid = claimIdentity?.FindFirst(ClaimTypes.Sid);

                var resultSid = sid?.Value is null ? null : sid.Value;
                if (resultSid != null)
                {
                    #pragma warning disable CS8604 // Possible null reference argument.
                    List<Course>? listCourses = _registerCourseService.GetAvailableCourses(resultSid);
                    #pragma warning restore CS8604 // Possible null reference argument.
                    if (listCourses != null)
                    {
                        return Ok(listCourses);
                    }

                    return Ok("No course is available");
                }
                else
                {
                    return BadRequest("User id is invalided");
                }
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Test API for get registationtimeline demo
        [AllowAnonymous]
        [HttpGet, Route("get-registration-timeline")]
        public IActionResult GetRegistrationTimeLineAPI()
        {
            try
            {
                return Ok(_registerCourseService.GetRegistrationTimeLineService());
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet, Route("check-registration-timeline")]
        public IActionResult CheckRegistrationTimeLineAPI()
        {
            try
            {
                if (_registerCourseService.IsRegistrationTimeLineRight())
                {
                    return Ok();
                }
                
                return StatusCode(404);
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "mod")]
        [HttpPost, Route("set-registration-timeline")]
        public IActionResult SetRegistrationTimeLineAPI([FromBody] SetRegistrationTimeLineRequest request)
        {
            try
            {
                return Ok(_registerCourseService.SetRegistrationTimeLineService(request));
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "mod")]
        [HttpPost, Route("finalize-courses-registraiton")]
        public IActionResult FinalizeCourseRegistraiotn()
        {
            try
            {
                _registerCourseService.FinishRegistCourseForAllStudent();
                return Ok();
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
