using AutoMapper;
using BLL.Common;
using BLL.DTOs.Courses;
using BLL.DTOs.CourseUsers;
using BLL.DTOs.Users;
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
        private readonly UserService _userService;
        private readonly CourseUserService _courseUserService;

        public CourseService(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
            _configuration = configuration;
            _courseRepository = _sharedRepositories.RepositoriesManager.CourseRepository;
            _userService = new UserService(sharedRepositories, mapper, configuration);
            _courseUserService = new CourseUserService(sharedRepositories, mapper, configuration);
        }

        public bool CheckDuplicateCourseCode(CourseDTO course)
        {
            try
            {
                if(_courseRepository.Get(c => c.Cousecode == course.Coursecode && c.Id != course.Id).Any())
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }

        public bool CheckDuplicateCourseName(CourseDTO course)
        {
            try
            {
                if (_courseRepository.Get(c => c.Coursename == course.Coursename && c.Id != course.Id).Any())
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
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

                Course newCourse = _mapper.Map<Course>(request);
                User? lecturer = _userService.GetUserByUsername(request.LecturerUserName);
                if (lecturer != null)
                {
                    newCourse.LecturerId = lecturer.Id; 
                    _courseUserService.AssignLecturerToCourse(lecturer, newCourse);
                }               

                _courseRepository.Insert(newCourse);
                _sharedRepositories.RepositoriesManager.Saves();

                return newCourse;
            }
            catch(Exception)
            {
                throw new ResourceConflictException(); 
            }

        }

        public Guid DeleteCourse(CourseDTO delete_course)
        {
            try
            {
                if (!_courseRepository.Get(course1 => course1.Coursename == delete_course.Coursename, null, 1).Any())
                {
                    throw new ResourceNotFoundException("This course is not exist");
                }

                Course delete = _courseRepository.Get(course1 => course1.Coursename == delete_course.Coursename, null, 1).First();
                User lecturer = _userService.GetUserById(delete.LecturerId.ToString());
                _courseRepository.Delete(delete);    
                _courseUserService.DeleteAllStudentsFromCourse(delete);
                _courseUserService.DeleteLecturerFromCourse(lecturer, delete);
                _sharedRepositories.RepositoriesManager.Saves();

                return delete.Id;
            }
            catch (Exception)
            {
                throw new ResourceConflictException();
            }
        }

        public Course EditCourse(CourseDTO update_version)
        {
            try
            {
                if (!_courseRepository.Get(course1 => course1.Coursename == update_version.Coursename, null, 1).Any())
                {
                    throw new ResourceNotFoundException("This course is not exist");
                }

                if (CheckDuplicateCourseName(update_version))
                {
                    throw new ResourceConflictException("This name is already existed");
                }

                if (CheckDuplicateCourseCode(update_version))
                {
                    throw new ResourceConflictException("This code is already existed");
                }

                Course current_course = _courseRepository.Get(course1 => course1.Coursename == update_version.Coursename, null, 1).First();
                User? lecturer = _userService.GetUserById(update_version.LecturerId);

                current_course.Coursename = update_version.Coursename;
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                current_course.LecturerId = lecturer.Id;
                #pragma warning restore CS8602 // Dereference of a possibly null reference.

                _courseRepository.Update(current_course);
                _sharedRepositories.RepositoriesManager.Saves();

                return current_course;
            }
            catch (Exception)
            {
                throw new ResourceConflictException();
            }
        }

        public IEnumerable<Course>? GetAllCourse()
        {
            try
            {
                return _courseRepository.GetAll();
            }
            catch (Exception)
            {
                throw new ResourceNotFoundException();
            }
        }

        public IEnumerable<Course>? GetAssignedCourse(string? userid)
        {
            try
            {
                #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                IEnumerable<CourseUserDTO> result_id_list = _courseUserService.GetAllCourseRefOfUser(userid);
                #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
                if (result_id_list.Count() == 0)
                {
                    return null;
                }

                List<Course> response = new List<Course>();
                #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                foreach (CourseUserDTO result in result_id_list)
                {
                    #pragma warning disable CS8604 // Possible null reference argument.
                    #pragma warning disable CS8602 // Dereference of a possibly null reference.
                    response.Add(_courseRepository.GetById(result.CourseId));
                    #pragma warning restore CS8602 // Dereference of a possibly null reference.
                    #pragma warning restore CS8604 // Possible null reference argument.
                }
                #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                return response;
            }
            catch (Exception)
            {
                throw new ResourceNotFoundException();
            }
        }

        public Course? GetCourseByName(string? name)
        {
            if (!_courseRepository.Get(c => c.Coursename == name, null, 1).Any()) return null;
            return _courseRepository.Get(c => c.Coursename == name, null, 1).First();
        }

        public Guid? GetCourseIdByName(string? name)
        {
            if (!_courseRepository.Get(course => course.Coursename == name, null, 1).Any()) return null;
            Course found =  _courseRepository.Get(course => course.Coursename == name, null, 1).First();

            return found.Id;
        }
    }
}
