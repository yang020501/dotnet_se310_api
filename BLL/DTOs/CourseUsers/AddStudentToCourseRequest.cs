using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.CourseUsers
{
    public class AddStudentToCourseRequest
    {
        public Guid? CourseId { get; set; }
        public List<Guid>? StudentIdList { get; set; }
    }
}
