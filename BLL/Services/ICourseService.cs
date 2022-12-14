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
        public Guid? GetCourseIdByName(string name);
        public Course CreateCourse(CreateCourseRequest request);
        public Guid DeleteCourse(CourseDTO course);
        public Course EditCourse(CourseDTO course);
        public Course? GetCourseByName(string name);
        public Boolean CheckDuplicateCourseCode(CourseDTO course);
        public Boolean CheckDuplicateCourseName(CourseDTO course);

    }
}
