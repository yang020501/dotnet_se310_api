using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.CourseUsers
{
    public class AddStudentToCourseResponse
    {
        public Guid? CourseId { get; set; }
        public List<User?>? StudentList { get; set; }
    }
}
