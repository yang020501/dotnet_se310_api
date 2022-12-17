using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Courses
{
    public class CreateCourseRespone
    {
        public Guid? Id { get; set; }
        public string? Coursename { get; set; }
        public Guid? LecturerId { get; set; }
        public string? Coursecode { get; set; }
    }
}
