using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Aggregates
{
    [Table("course")]
    public class Course
    {
        public Course()
        {
            this.Users = new HashSet<User>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("coursename")]
        public string? Coursename { get; set; }

        [Column("lecture_id")]
        [ForeignKey("user")]
        public Guid? LecturerId { get; set; }

        [Column("course_code")]
        public string? Cousecode { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
