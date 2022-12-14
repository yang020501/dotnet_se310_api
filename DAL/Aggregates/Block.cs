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
        public Block()
        {
            this.Documents = new HashSet<MarkdownDocument>();
        }

        [Key]
        [Column("id")]
        public Guid? Id { get; set; }

        [Column("course_id")]
        public Guid? CourseId;

        public virtual ICollection<MarkdownDocument> Documents { get; set; }
    }
}
