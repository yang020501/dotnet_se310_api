using BLL.DTOs.Courses;
using BLL.DTOs.Users;
using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICourseService
    {
        public IEnumerable<Course>? GetAllCourse();
        public IEnumerable<Course>? GetAssignedCourse(string? userid);
        public CreateCourseRespone CreateCourse(CreateCourseRequest request);
        public Guid DeleteCourseById(Guid? id);
        public Course EditCourse(CourseDTO course);

   

    }
}
