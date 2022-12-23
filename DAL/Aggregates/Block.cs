using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Aggregates
{
    [Table("block")]
    public class Block
    {       
        [Key]
        [Column("id")]
        public Guid? Id { get; set; }

        [Column("block_name")]
        public string? Name { get; set; }

        [Column("course_id")]
        public Guid? CourseId { get; set; }

        [Column("markdown_document")]
        public string? MarkdownDocument { get; set; }
    }
}
