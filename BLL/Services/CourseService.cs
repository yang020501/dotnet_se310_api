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
using CQRSHandler.Commands;
using static CQRSHandler.CommandHandlers.CreateAvailableCoursesForRegistrationHandler;
using static CQRSHandler.CommandHandlers.AddAvailableCoursesHandler;
using System.Text.Json;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ISharedRepositories _sharedRepositories;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly ICommon _commonService;

        public CourseService(ISharedRepositories sharedRepositories, IMapper mapper, ICommon commonService)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
            _courseRepository = _sharedRepositories.RepositoriesManager.CourseRepository;
            _commonService = commonService;
        }      

        public CreateCourseRespone CreateCourse(CreateCourseRequest request)
        {
            try
            {               
                if (_courseRepository.Get(c => c.Coursename == request.Coursename).Any() || _courseRepository.Get(c => c.Coursecode == request.Coursecode).Any())
                {
                    throw new ResourceConflictException();
                }

                Course newCourse = _mapper.Map<Course>(request);
                User? lecturer = _commonService.GetUserByUsername(request.LecturerUserName);                                   
                if (lecturer != null && lecturer.Role == "lecturer")
                {
                    newCourse.LecturerId = lecturer.Id;                   
                }               
                _courseRepository.Insert(newCourse);
                _sharedRepositories.RepositoriesManager.Saves();

                if (newCourse.LecturerId != null && lecturer is not null)
                {
                    _commonService.AssignLecturerToCourse(lecturer, newCourse);
                }

                CourseDTO dto = _mapper.Map<CourseDTO>(newCourse);
                string json = JsonSerializer.Serialize<CourseDTO>(dto);
                var param = new AddAvailableCourses() { Json = json };
                Handle(param, _sharedRepositories.DapperContext);

                var respone = _mapper.Map<CreateCourseRespone>(newCourse);

                return respone;
            }
            catch(Exception e)
            {
                throw new ResourceConflictException(e.Message); 
            }

        }

        public void CreateCoursesWithCSV(List<CreateCourseRequest> csv)
        {
            try
            {
                List<Course> list = _mapper.Map<List<Course>>(csv);
                string json = JsonSerializer.Serialize<List<Course>>(list);
                var param = new CreateAvailableCourse() { Json = json };
                Handle(param, _sharedRepositories.DapperContext);
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public Guid DeleteCourseById(Guid? id)
        {
            try
            {
                if (!_courseRepository.Get(course1 => course1.Id == id, null, 1).Any())
                {
                    throw new ResourceNotFoundException("This course is not exist");
                }

                Course delete_course = _courseRepository.Get(course1 => course1.Id == id, null, 1).First();
                User? lecturer = null;
                if (delete_course.LecturerId is not null)
                {
                    lecturer = _commonService.GetUserById(delete_course.LecturerId.ToString());
                }

                _commonService.DeleteAllStudentsFromCourse(delete_course);
                if(lecturer is not null)
                {
                    _commonService.DeleteLecturerFromCourse(lecturer, delete_course);
                }
                _courseRepository.Delete(delete_course);
                _sharedRepositories.RepositoriesManager.Saves();

                return delete_course.Id;
            }
            catch (Exception)
            {
                throw new ResourceConflictException();
            }
        }

        public CourseDTO EditCourse(CourseDTO update_version)
        {
            try
            {
                if (!_courseRepository.Get(c => c.Id == update_version.Id).Any())
                {
                    throw new ResourceNotFoundException("This course is not exist");
                }

                if (_commonService.CheckDuplicateCourseName(update_version))
                {
                    throw new ResourceConflictException("This name is already existed");
                }

                if (_commonService.CheckDuplicateCourseCode(update_version))
                {
                    throw new ResourceConflictException("This code is already existed");
                }

                Course current_course = _courseRepository.Get(course1 => course1.Id == update_version.Id).First();
                User? lecturer = new User();

                if (update_version.LecturerId != null)
                {
                    lecturer = _commonService.GetUserById(update_version.LecturerId);
                    _commonService.ChangeLecturerOfCourse(lecturer, current_course);
                    current_course.LecturerId = lecturer.Id;
                }
                else
                {
                    lecturer = _commonService.GetUserById(current_course.LecturerId.ToString());
                    _commonService.DeleteLecturerFromCourse(lecturer, current_course);
                    current_course.LecturerId = null;
                }

                current_course.Coursename = update_version.Coursename;
                current_course.Coursecode = update_version.Coursecode;
                #pragma warning disable CS8602 // Dereference of a possibly null reference.             
                #pragma warning restore CS8602 // Dereference of a possibly null reference.

                _courseRepository.Update(current_course);
                _sharedRepositories.RepositoriesManager.Saves();

                return _mapper.Map<CourseDTO>(current_course);
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
                IEnumerable<CourseUserDTO> result_id_list = _commonService.GetAllCourseRefOfUser(userid);
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

        public CourseDTO? GetCourseById(Guid? id)
        {
            try
            {
                if(id is not null)
                {
                    Course? course = _courseRepository.GetById(id);
                    return _mapper.Map<CourseDTO>(course);  
                }

                return null;
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }
    }
}
