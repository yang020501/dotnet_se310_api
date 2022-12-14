using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.CourseUsers
{
    public class CourseUserDTO
    {
        public Guid? UserId {get; set;}
        public Guid? CourseId {get; set;}
    }
}
