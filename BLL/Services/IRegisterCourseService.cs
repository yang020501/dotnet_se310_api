using BLL.DTOs.RegisterCourses;
using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IRegisterCourseService
    {
        public List<Course>? CheckRegisterCourse(RegisterCourseRequest request, string user_id);
        RegisterCourseResponse? RegisterCourseForStudent(RegisterCourseRequest request, string user_id);
        CancelRegistedCourseResponse? CancelRegistedCourse(CancelRegistedCourseRequest request, string user_id);
        List<Course>? GetRegistedCourseOfStudent(string user_id);
        List<Course>? GetAvailableCourses();
    }
}
