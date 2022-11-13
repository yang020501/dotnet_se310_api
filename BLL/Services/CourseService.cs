using AutoMapper;
using BLL.Common;
using BLL.DTOs.Courses;
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
    public class CourseService : ICourseService
    {
        private readonly ISharedRepositories _sharedRepositories;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly UserService _userService;

        public CourseService(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
            _configuration = configuration;
            _courseRepository = _sharedRepositories.RepositoriesManager.CourseRepository;
            _userRepository = _sharedRepositories.RepositoriesManager.UserRepository;
            _userService = new UserService(sharedRepositories, mapper, configuration);
        }

        public Course CreateCourse(CreateCourseRequest request)
        {
            try
            {
                var foundCourse = _courseRepository.Get(course => course.Coursename == request.Coursename, null, 1);
                if (foundCourse.Any())
                {
                    throw new InvalidOperationException($"Coursename {request.Coursename} has existed in the current context");
                }

                Course newCourse = new Course();
                User? lecturer = new User();
                if (request.Lecturename != null && request.Lecturename != "")
                {
                    lecturer = _userService.GetUserByUsername(request.Lecturename);
                }

                newCourse.Id = new Guid();
                if (lecturer != null)
                {
                    newCourse.LectureId = lecturer.Id;
                }
                newCourse.Coursename = request.Coursename;
                newCourse.Cousecode = request.Coursecode;

                _courseRepository.Insert(newCourse);
                _sharedRepositories.RepositoriesManager.Saves();

                return newCourse;
            }
            catch(Exception e)
            {
                throw; 
            }

        }

        public Guid DeleteCourse(CourseDTO delete_course)
        {
            if (!_courseRepository.Get(course1 => course1.Coursename == delete_course.Coursename, null, 1).Any())
            {
                throw new ResourceNotFoundException("This course is not exist");
            }

            Course delete = _courseRepository.Get(course1 => course1.Coursename == delete_course.Coursename, null, 1).First();
            _courseRepository.Delete(delete);
            _sharedRepositories.RepositoriesManager.Saves();

            return delete.Id;
        }

        public Course EditCourse(CourseDTO update_version)
        {
            if (!_courseRepository.Get(course1 => course1.Coursename == update_version.Coursename, null, 1).Any())
            {
                throw new ResourceNotFoundException("This course is not exist");
            }

            Course current_course = _courseRepository.Get(course1 => course1.Coursename == update_version.Coursename, null, 1).First();        
            User? lecturer = _userService.GetUserByUsername(update_version.Lecturename);

            current_course.Coursename = update_version.Coursename;
            current_course.LectureId = lecturer.Id;
            
            _courseRepository.Update(current_course);
            _sharedRepositories.RepositoriesManager.Saves();

            return current_course;

        }

        public IEnumerable<Course> GetAllCourse()
        {
            try
            {
                return _courseRepository.GetAll();
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException();
            }
        }

        public Guid? GetCourseId_byName(string name)
        {
            if (!_courseRepository.Get(course => course.Coursename == name, null, 1).Any()) return null;
            Course found =  _courseRepository.Get(course => course.Coursename == name, null, 1).First();

            return found.Id;
        }
    }
}
