using BLL.DTOs.CourseUsers;
using BLL.DTOs.Users;
using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICourseUserService
    {            
        public AddStudentToCourseResponse AddStudentsToCourse(AddStudentToCourseRequest request);
        public RemoveStudentFromCourseResponse RemoveStudentsFromCourse(RemoveStudentFromCourseRequest request);
    }
}
