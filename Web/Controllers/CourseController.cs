using BLL.DTOs.Courses;
using BLL.Services;
using DAL.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = "moderator")]
        [HttpPost, Route("create")]
        public IActionResult CreateCourse([FromBody] CreateCourseRequest createRequest)
        {
            try
            {
                var course = _courseService.CreateCourse(createRequest);
                return Ok(course);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "moderator")]
        [HttpPost, Route("delete")]
        public IActionResult DeleteCourse([FromBody] CourseDTO request)
        {
            try
            {
                var course = _courseService.DeleteCourse(request);
                return Ok("Course has been deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "moderator")]
        [HttpPost, Route("update")]
        public IActionResult UpdateCourse([FromBody] CourseDTO request)
        {
            try
            {
                var course = _courseService.EditCourse(request);
                return Ok("Course has been editted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet, Route("get-all")]
        public IActionResult GetAllCourse()
        {
            try
            {
                IEnumerable<Course> list = _courseService.GetAllCourse();
                return Ok(list);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
