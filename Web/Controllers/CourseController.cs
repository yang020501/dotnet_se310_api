using BLL.DTOs.Courses;
using BLL.Exceptions;
using BLL.Services;
using DAL.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Presentation.Controllers
{
    [Route("api/courses/")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [Authorize(Roles = "lecturer,student")]
        [HttpGet, Route("{id:Guid?}")]
        public IActionResult GetCourseById(Guid? id)
        {
            try
            {
                var course = _courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (BaseCustomApplicationException e)
            {
                return Shared.SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "mod")]
        [HttpPost]
        public IActionResult CreateCourse([FromBody] CreateCourseRequest createRequest)
        {
            try
            {
                var course = _courseService.CreateCourse(createRequest);
                return Ok(course);
            }
            catch (BaseCustomApplicationException e)
            {
                return Shared.SharedControllerMethods.HandleExceptions(e, this);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       
        [Authorize(Roles = "mod")]
        [HttpDelete, Route("{id:Guid?}")]
        public IActionResult DeleteCourse(Guid id)
        {
            try
            {
                var course = _courseService.DeleteCourseById(id);
                return Ok("Course has been deleted");
            }
            catch (BaseCustomApplicationException e)
            {
                return Shared.SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "mod")]
        [HttpPatch]
        public IActionResult UpdateCourse([FromBody] CourseDTO request)
        {
            try
            {
                var course = _courseService.EditCourse(request);
                return Ok(course);
            }
            catch (BaseCustomApplicationException e)
            {
                return Shared.SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "mod")]
        [HttpGet, Route("get-all")]
        public IActionResult GetAllCourse()
        {
            try
            {
                IEnumerable<Course> list = _courseService.GetAllCourse();
                return Ok(list);
            }
            catch (BaseCustomApplicationException e)
            {
                return Shared.SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "student,lecturer")]
        [HttpGet, Route("get-assign-course")]
        public IActionResult GetAssignedCourse()
        {
            try
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var sid = claimIdentity?.FindFirst(ClaimTypes.Sid);   

                var resultSid = sid?.Value is null ? null : sid.Value;             
                if (resultSid != null)
                {                   
                    return Ok(_courseService.GetAssignedCourse(resultSid));
                }

                return Unauthorized("Can't found courses assigned to this user");
            }
            catch (BaseCustomApplicationException e)
            {
                return Shared.SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "mod")]
        [HttpPost, Route("create-courses-with-csv")]
        public IActionResult CreateCourseWithCSV([FromBody] List<CourseCSV>? csv)
        {
            try
            {
                _courseService.CreateCoursesWithCSV(csv);
                return Ok();
            }
            catch (BaseCustomApplicationException e)
            {
                return Shared.SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
