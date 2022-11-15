using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Aggregates
{
    [Table("course_user")]
    public class CourseUser
    {
        [ForeignKey("user")]
        public Guid UserId { get; set; }
        [ForeignKey("course")]
        public Guid CourseId { get; set; }
    }
}
