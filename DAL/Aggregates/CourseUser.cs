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
        [Column("user_ref")]
        public Guid UserId { get; set; }

        [ForeignKey("course")]
        [Column("course_ref")]
        public Guid CourseId { get; set; }

        public Course? Course { get; set; }
        public User? User { get; set; }
    }
}
