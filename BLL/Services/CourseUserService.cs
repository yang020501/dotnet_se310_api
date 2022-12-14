using AutoMapper;
using BLL.Common;
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

        public CourseUserRespone AddUserToCourse(CourseUserRequest request)
        {
            User? student = _userService.GetUserByUsername(request.Username);
            if(student == null)
            {
                throw new ResourceNotFoundException("Username not found");
            }

            Course? course = _courseService.GetCourseByName(request.Coursename);
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

        public CourseUserDTO AssignLecturerToCourse(User lecturer, Course course)
        {
            try
            {
                CourseUser course_user = new CourseUser();
                course_user.UserId = lecturer.Id;
                course_user.CourseId = course.Id;
                if (!_courseuserRepository.Get(cu => cu.UserId == course_user.UserId && cu.CourseId == course_user.CourseId).Any())
                {
                    _courseuserRepository.Insert(course_user);
                    _sharedRepositories.RepositoriesManager.Saves();
                    CourseUserDTO dto = _mapper.Map<CourseUserDTO>(course_user);

                    return dto;
                }

                throw new ResourceConflictException("There is another of ref of this course and lecturer existed");
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public CourseUserDTO ChangeLecturerOfCourse(User lecturer, Course course)
        {
            try
            {
                CourseUser old_course_user = new CourseUser();
                old_course_user.UserId = course.LecturerId;
                old_course_user.CourseId = course.Id;
                if (_courseuserRepository.Get(cu => cu.UserId == old_course_user.UserId && cu.CourseId == old_course_user.CourseId).Any())
                {
                    _courseuserRepository.Delete(old_course_user);
                }

                CourseUser new_course_user = new CourseUser();
                new_course_user.UserId = lecturer.Id;
                new_course_user.CourseId = course.Id;
                _courseuserRepository.Insert(new_course_user);
                _sharedRepositories.RepositoriesManager.Saves();
                CourseUserDTO dto = _mapper.Map<CourseUserDTO>(new_course_user);

                return dto;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public CourseUserDTO DeleteLecturerFromCourse(User lecturer, Course course)
        {
            try
            {
                CourseUser course_user = new CourseUser();
                course_user.UserId = lecturer.Id;
                course_user.CourseId = course.Id;

                if(_courseuserRepository.Get(cu => cu.UserId == course_user.UserId && cu.CourseId == course_user.CourseId).Any())
                {
                    _courseuserRepository.Delete(course_user);
                    _sharedRepositories.RepositoriesManager.Saves();
                    CourseUserDTO dto = _mapper.Map<CourseUserDTO>(course_user);

                    return dto;
                }

                throw new ResourceNotFoundException("There is no ref of this course and user existed");
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public IEnumerable<CourseUserDTO> DeleteAllStudentsFromCourse(Course course)
        {
            try
            {              
                List<CourseUser> ref_list = _courseuserRepository.Get(c => c.CourseId == course.Id).ToList();
                if (ref_list.Any())
                {
                    foreach (CourseUser user in ref_list)
                    {
                        User student = _userRepository.GetById(user.UserId);
                        if(student != null && student.Role == "student")
                        {
                            _courseuserRepository.Delete(user);
                        }
                    }

                    _sharedRepositories.RepositoriesManager.Saves();
                    return _mapper.Map<List<CourseUserDTO>>(ref_list);
                }

                throw new ResourceNotFoundException();

            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public IEnumerable<CourseUserDTO> GetAllCourseRefOfUser(string? userid)
        {
            try
            {
                List<CourseUser> list = _courseuserRepository.Get(cu => cu.UserId.ToString() == userid).ToList();
                return _mapper.Map<List<CourseUserDTO>>(list);              
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }

        public CourseUserRespone MoveUserToAnotherCourse(CourseUserRequest request)
        {
            throw new NotImplementedException();
        }


        public CourseUserRespone RemoveUserFromCourse(CourseUserRequest request)
        {
            User? student = _userService.GetUserByUsername(request.Username);
            if (student == null)
            {
                throw new ResourceNotFoundException("Username not found");
            }

            Course? course = _courseService.GetCourseByName(request.Coursename);
            if (course == null)
            {
                throw new ResourceNotFoundException("Course not found");
            }

            CourseUser courseUser = new CourseUser();
            courseUser.UserId = student.Id;
            courseUser.CourseId = course.Id;

            _courseuserRepository.Delete(courseUser);
            _sharedRepositories.RepositoriesManager.Saves();

            CourseUserRespone respone = _mapper.Map<CourseUserRespone>(courseUser);
            respone.UserName = request.Username;
            respone.CourseName = request.Coursename;
            return respone;
        }

        public AddStudentToCourseResponse AddStudentsToCourse(AddStudentToCourseRequest request)
        {
            try
            {
                AddStudentToCourseResponse response = new AddStudentToCourseResponse();
                response.CourseId = request.CourseId;
                foreach (string username in request.StudentnameList)
                {
                    User student = _userService.GetUserByUsername(username);
                    CourseUser courseUser = new CourseUser();
                    courseUser.UserId = student.Id;
                    courseUser.CourseId = request.CourseId;
                    response.StudentList.Add(student);
                    _courseuserRepository.Insert(courseUser);
                }
                _sharedRepositories.RepositoriesManager.Saves();

                return response;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public RemoveStudentFromCourseResponse RemoveStudentsFromCourse(RemoveStudentFromCourseRequest request)
        {
            try
            {
                RemoveStudentFromCourseResponse response = new RemoveStudentFromCourseResponse();
                response.CourseId = request.CourseId;
                foreach (string username in request.StudentnameList)
                {
                    User student = _userService.GetUserByUsername(username);                  
                    response.StudentList.Add(student);
                    _courseuserRepository.Delete(_courseuserRepository.Get(cu => cu.UserId == student.Id && cu.CourseId == request.CourseId));
                }
                _sharedRepositories.RepositoriesManager.Saves();

                return response;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }
    }
}
