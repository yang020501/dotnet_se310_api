using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.RegisterCourses
{
    public class RegisterCourseDto
    {
        public Guid? Student { get; set; }
        public List<string>? Courses { get; set; }
    }
}
