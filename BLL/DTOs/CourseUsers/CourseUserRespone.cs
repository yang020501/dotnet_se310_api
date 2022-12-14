using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.CourseUsers
{
    public class CourseUserRespone
    {
        public Guid? UserId;
        public string? UserName;
        public Guid? CourseId;
        public string? CourseName;

        public CourseUserRespone(Guid? userId, string? userName, Guid? courseId, string? courseName)
        {
            UserId = userId;
            UserName = userName;
            CourseId = courseId;
            CourseName = courseName;
        }
    }
}
