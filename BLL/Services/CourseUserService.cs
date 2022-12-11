using AutoMapper;
using BLL.Common;
using BLL.DTOs.CourseUser;
using BLL.Exceptions;
using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CourseUserService : ICourseUserService
    { 
        private readonly ISharedRepositories _sharedRepositories;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<CourseUser> _courseuserRepository;
        private readonly UserService _userService;
        private readonly CourseService _courseService;

        public CourseUserService(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
            _configuration = configuration;
            _courseRepository = _sharedRepositories.RepositoriesManager.CourseRepository;
            _userRepository = _sharedRepositories.RepositoriesManager.UserRepository;
            _courseuserRepository = _sharedRepositories.RepositoriesManager.CourseUserRepository;
            _userService = new UserService(sharedRepositories, mapper, configuration);
            _courseService = new CourseService(sharedRepositories, mapper, configuration);
        }

        public CourseUserRespone AddStudentToCourse(CourseUserRequest request)
        {
            User? student = _userService.GetUserByUsername(request.UserName);
            if(student == null)
            {
                throw new ResourceNotFoundException("Username not found");
            }

            Course? course = _courseService.GetCourseByName(request.CourseName);
            if (course == null)
            {
                throw new ResourceNotFoundException("Course not found");
            }

            CourseUser courseUser = new CourseUser();
            courseUser.UserId = student.Id;
            courseUser.CourseId = course.Id;

            _courseuserRepository.Insert(courseUser);
            _sharedRepositories.RepositoriesManager.Saves();

            return new CourseUserRespone(student.Id, student.Username, course.Id, course.Coursename);
        }

        public CourseUserRespone MoveStudentToAnotherCourse(CourseUserRequest request)
        {
            throw new NotImplementedException();
        }

        public CourseUserRespone RemoveStudentFromCourse(CourseUserRequest request)
        {
            User? student = _userService.GetUserByUsername(request.UserName);
            if (student == null)
            {
                throw new ResourceNotFoundException("Username not found");
            }

            Course? course = _courseService.GetCourseByName(request.CourseName);
            if (course == null)
            {
                throw new ResourceNotFoundException("Course not found");
            }

            CourseUser courseUser = new CourseUser();
            courseUser.UserId = student.Id;
            courseUser.CourseId = course.Id;

            _courseuserRepository.Delete(courseUser);
            _sharedRepositories.RepositoriesManager.Saves();

            return new CourseUserRespone(student.Id, student.Username, course.Id, course.Coursename);
        }
    }
}
