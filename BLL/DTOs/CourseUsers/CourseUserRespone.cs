using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.CourseUsers
{
    public class CourseUserRespone
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public Guid? CourseId { get; set; }
        public string? CourseName { get; set; }


    }
}
