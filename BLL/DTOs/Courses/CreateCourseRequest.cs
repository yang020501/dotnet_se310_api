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
        public string? LecturerUserName { get; set; }
        public string? Coursecode { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DateOfWeek { get; set; }
        public Boolean? Session { get; set; }
    }
}
