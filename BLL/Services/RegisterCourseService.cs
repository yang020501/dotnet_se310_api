using AutoMapper;
using BLL.Common;
using BLL.DTOs.RegisterCourses;
using BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static CQRSHandler.QueryHandlers.FindDuplicatedScheduleHandler;
using static CQRSHandler.CommandHandlers.RegisterCourseCommandHandler;
using static CQRSHandler.CommandHandlers.CancelRegistedCourseHandler;
using CQRSHandler.Queries;
using DAL.Aggregates;
using CQRSHandler.Commands;

namespace BLL.Services
{
    public class RegisterCourseService : IRegisterCourseService
    {
        private readonly IMapper _mapper;
        private readonly ISharedRepositories _sharedRepositories;

        public RegisterCourseService(ISharedRepositories sharedRepositories, IMapper mapper)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
        }

        public CancelRegistedCourseResponse? CancelRegistedCourse(CancelRegistedCourseRequest request, string user_id)
        {
            try
            {
                RegisterCourseDto dto = new RegisterCourseDto
                {
                    Student = new Guid(user_id),
                    Courses = request.CoursesId
                };

                string json = JsonSerializer.Serialize<RegisterCourseDto>(dto);
                var param = new CancelRegistedCourse() { Json = json };

                Handle(param, _sharedRepositories.DapperContext);
                return _mapper.Map<CancelRegistedCourseResponse>(request);
            }
            catch(Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }

        public List<Course>? CheckRegisterCourse(RegisterCourseRequest request, string user_id)
        {
            try
            {
                List<Course> error_courses = new List<Course>();

                RegisterCourseDto dto = new RegisterCourseDto
                {
                    Student = new Guid(user_id),
                    Courses = request.CoursesId
                };

                string json = JsonSerializer.Serialize<RegisterCourseDto>(dto);
                var query = new CheckRegisterCourse() { Json = json };
                Console.WriteLine(query);

                error_courses = Handle(query, _sharedRepositories.DapperContext).ToList();
                return error_courses;
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException(e.Message + ": Error in handel of check register courses");
            }
        }

        public RegisterCourseResponse RegisterCourseForStudent(RegisterCourseRequest request, string user_id)
        {
            try
            {
                RegisterCourseDto dto = new RegisterCourseDto
                {
                    Student = new Guid(user_id),
                    Courses = request.CoursesId
                };

                string json = JsonSerializer.Serialize<RegisterCourseDto>(dto);
                var param = new RegisterCourse() { Json = json };

                Handle(param, _sharedRepositories.DapperContext);
                return _mapper.Map<RegisterCourseResponse>(request);
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }
    }
}
