using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Courses
{
    public class CourseDTO
    {
        public Guid? Id { get; set; }
        public string? Coursename { get; set; }
        public string? LecturerId { get; set; }
        public string? Coursecode { get; set; }

    }
}
