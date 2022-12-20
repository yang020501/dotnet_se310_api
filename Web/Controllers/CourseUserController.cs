using BLL.DTOs.CourseUsers;
using BLL.Exceptions;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Shared;

namespace Presentation.Controllers
{
    [Route("api/course-user")]
    [ApiController]
    public class CourseUserController : ControllerBase
    {
        private readonly ICourseUserService _courseUserService;

        public CourseUserController(ICourseUserService courseUserService)
        {
            _courseUserService = courseUserService;
        }

        [Authorize(Roles = "mod")]
        [HttpPost, Route("add-students")]
        public IActionResult AddStudentsToCourse([FromBody] AddStudentToCourseRequest request)
        {
            try
            {
                return Ok(_courseUserService.AddStudentsToCourse(request));
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

        [Authorize(Roles = "mod")]
        [HttpPatch, Route("remove-students")]
        public IActionResult RemoveStudentsFromCourse([FromBody] RemoveStudentFromCourseRequest request)
        {
            try
            {
                return Ok(_courseUserService.RemoveStudentsFromCourse(request));
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

        [Authorize(Roles = "mod,lecturer")]
        [HttpGet, Route("course-students/{id:Guid?}")]
        public IActionResult GetAllStudentOfCourse(Guid? id)
        {
            try
            {
                return Ok(_courseUserService.GetAllStundetsInCourse(id));
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
