using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Courses
{
    public class CreateCourseRequest
    {
        public string? Coursename { get; set; }
        public string? Lecturename { get; set; }
        public string? Coursecode { get; set; }
    }
}
