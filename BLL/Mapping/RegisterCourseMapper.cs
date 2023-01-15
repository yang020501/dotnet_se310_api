using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.RegisterCourses;

namespace BLL.Mapping
{
    public class RegisterCourseMapper
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<RegisterCourseRequest, RegisterCourseResponse>();
            config.CreateMap<CancelRegistedCourseRequest, CancelRegistedCourseResponse>();
        }
    }
}
