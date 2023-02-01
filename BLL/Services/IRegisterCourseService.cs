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
        public RegisterCourseResponse? RegisterCourseForStudent(RegisterCourseRequest request, string user_id);
        public CancelRegistedCourseResponse? CancelRegistedCourse(CancelRegistedCourseRequest request, string user_id);
        public List<Course>? GetRegistedCourseOfStudent(string user_id);
        public List<Course>? GetAvailableCourses(string user_id);
        public RegistrationTimelineResponse? GetRegistrationTimeLineService();
        public RegistrationTimelineResponse? SetRegistrationTimeLineService(SetRegistrationTimeLineRequest request);
        public Boolean IsRegistrationTimeLineRight();
        public Boolean FinishRegistCourseForAllStudent(); 
    }
}
