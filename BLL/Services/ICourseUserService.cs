using BLL.DTOs.CourseUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICourseUserService
    {
        public CourseUserRespone AddStudentToCourse(CourseUserRequest request);
        public CourseUserRespone RemoveStudentFromCourse(CourseUserRequest request);
        public CourseUserRespone MoveStudentToAnotherCourse(CourseUserRequest request);

    }
}
