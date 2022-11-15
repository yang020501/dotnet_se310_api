using BLL.DTOs.Courses;
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
        public IEnumerable<Course> GetAllCourse();
        public Guid? GetCourseId_byName(string name);
        public Course CreateCourse(CreateCourseRequest request);
        public Guid DeleteCourse(CourseDTO course);
        public Course EditCourse(CourseDTO course);
        public Course? GetCourseByName(string name);
    }
}
