﻿using BLL.DTOs.Courses;
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
        private readonly ICourseUserService _courseUserService;

        public CourseController(ICourseService courseService, ICourseUserService courseUserService)
        {
            _courseService = courseService;
            _courseUserService = courseUserService;
        }

        [Authorize(Roles = "mod")]
        [HttpPost, Route("create")]
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
        [HttpDelete, Route("delete")]
        public IActionResult DeleteCourse([FromBody] CourseDTO request)
        {
            try
            {
                var course = _courseService.DeleteCourse(request);
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
        [HttpPatch, Route("update")]
        public IActionResult UpdateCourse([FromBody] CourseDTO request)
        {
            try
            {
                var course = _courseService.EditCourse(request);
                return Ok("Course has been editted");
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


    }
}
