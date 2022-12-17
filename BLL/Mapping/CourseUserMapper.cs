using AutoMapper;
using BLL.DTOs.CourseUsers;
using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class CourseUserMapper
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<CourseUserDTO, CourseUser>();
            config.CreateMap<CourseUser, CourseUserDTO>(); 
        }
    }
}
