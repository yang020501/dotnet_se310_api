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
        private readonly IGenericRepository<CourseUser> _courseuserRepository;    
        private readonly ICommon _commonService;

        public CourseUserService(ISharedRepositories sharedRepositories, IMapper mapper, ICommon commonService)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
            _courseuserRepository = _sharedRepositories.RepositoriesManager.CourseUserRepository;
            _commonService = commonService;
        }       

        public AddStudentToCourseResponse? AddStudentsToCourse(AddStudentToCourseRequest request)
        {
            try
            {
                AddStudentToCourseResponse response = new AddStudentToCourseResponse();
                response.StudentIdList = new List<Guid>();
                response.CourseId = request.CourseId;

                if(request.StudentIdList is not null && request.StudentIdList.Count() == 0)
                {
                    return null;
                }

                foreach (Guid id in request.StudentIdList)
                {
                    User? student = _commonService.GetUserById(id.ToString());
                    if(student is not null)
                    {
                        CourseUser courseUser = new CourseUser();
                        courseUser.UserId = student.Id;
                        courseUser.CourseId = request.CourseId;
                        response.StudentIdList.Add(id);
                        _courseuserRepository.Insert(courseUser);
                    }
                }
                _sharedRepositories.RepositoriesManager.Saves();

                return response;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public IEnumerable<UserDTO> GetAllStundetsInCourse(Guid? course_id)
        {
            try
            {
                return _mapper.Map<List<UserDTO>>(_commonService.GetStudentsInCourse(course_id));
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }

        public RemoveStudentFromCourseResponse RemoveStudentsFromCourse(RemoveStudentFromCourseRequest request)
        {
            try
            {
                RemoveStudentFromCourseResponse response = new RemoveStudentFromCourseResponse();
                response.CourseId = request.CourseId;
                response.StudentIdList = new List<Guid>();
                if (request.StudentIdList is not null && request.StudentIdList.Count() == 0)
                {
                    return null;
                }

                foreach (Guid id in request.StudentIdList)
                {
                    User? student = _commonService.GetUserById(id.ToString());
                    if (student is not null)
                    {
                        if (_courseuserRepository.Get(cu => cu.UserId == student.Id && cu.CourseId == request.CourseId).Any())
                        {
                            response.StudentIdList.Add(id);
                            _courseuserRepository.Delete(_courseuserRepository.Get(cu => cu.UserId == student.Id && cu.CourseId == request.CourseId).FirstOrDefault());
                        }
                    }
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
