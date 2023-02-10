﻿using AutoMapper;
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
using static CQRSHandler.QueryHandlers.GetRegistedCourseHandler;
using static CQRSHandler.QueryHandlers.GetAvailableCoursesHandler;
using static CQRSHandler.QueryHandlers.GetRegistrationTimeLineHandler;
using static CQRSHandler.CommandHandlers.SetRegistrationTimeLineHandler;
using static CQRSHandler.CommandHandlers.FinishCourseRegistrationHandler;
using CQRSHandler.Queries;
using DAL.Aggregates;
using CQRSHandler.Commands;
using CQRSHandler.Domains;

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

        public Boolean FinishRegistCourseForAllStudent()
        {
            try
            {
                var param = new FinishCourseRegistration();
                Handle(param, _sharedRepositories.DapperContext);
                return true;
            }
            catch(Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public List<Course>? GetAvailableCourses(string user_id)
        {
            try
            {
                var queryAC = new GetAvailableCourses();
                List<Course>? listAvailableCourses = Handle(queryAC, _sharedRepositories.DapperContext).ToList();

                var queryRC = new GetRegistedCourse() { Id = user_id };
                List<Course>? listRegistedCourses = Handle(queryRC, _sharedRepositories.DapperContext).ToList();

                List<Course>? removeList = new List<Course>();

                foreach (var courseA in listAvailableCourses)
                {
                    foreach(var courseR in listRegistedCourses)
                    {
                        if (courseA.Id == courseR.Id)
                        {
                            removeList.Add(courseA);
                        }
                    }
                }

                foreach(var courseRemove in removeList)
                {
                    listAvailableCourses.Remove(courseRemove);
                }

                return listAvailableCourses;
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }

        public List<Course>? GetRegistedCourseOfStudent(string user_id)
        {
            try
            {
                var query = new GetRegistedCourse() { Id = user_id };
                List<Course> list = Handle(query, _sharedRepositories.DapperContext).ToList();
                return list;
            }
            catch(Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }

        public RegistrationTimelineResponse? GetRegistrationTimeLineService()
        {
            try
            {
                var query = new GetRegistrationTimeLine();
                List<GetRegistrationTimeLineRecord> list_time_line = Handle(query, _sharedRepositories.DapperContext).ToList();

                return _mapper.Map<RegistrationTimelineResponse>(list_time_line[0]);
            }
            catch(Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }

        public bool IsRegistrationTimeLineRight()
        {
            try
            {
                var query = new GetRegistrationTimeLine();
                List<GetRegistrationTimeLineRecord> list_time_line = Handle(query, _sharedRepositories.DapperContext).ToList();
                DateTime now = DateTime.Now;
                if (list_time_line[0].EndDate >= now && list_time_line[0].StartDate <= now)
                {
                    return true;
                }

                return false;
            }
            catch(Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
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

        public RegistrationTimelineResponse? SetRegistrationTimeLineService(SetRegistrationTimeLineRequest request)
        {
            try
            {
                SetRegistrationTimeLine param = new SetRegistrationTimeLine()
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };

                Handle(param, _sharedRepositories.DapperContext);
                return new RegistrationTimelineResponse() { StartDate = param.StartDate, EndDate = param.EndDate, Finished = false };
            }
            catch(Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }
    }
}
