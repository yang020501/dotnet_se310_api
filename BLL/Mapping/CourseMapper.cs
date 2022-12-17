using AutoMapper;
using BLL.DTOs.Courses;
using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class CourseMapper
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<Course, CourseDTO>();
            config.CreateMap<CreateCourseRequest, Course>().AfterMap((source, destination) => destination.Id = Guid.NewGuid());
            config.CreateMap<Course, CreateCourseRespone>();
        }
    }
}
