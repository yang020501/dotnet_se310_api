using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Aggregates
{
    [Table("markdown_document")]
    public class MarkdownDocument
    {
        [Key]
        [Column("id")]
        public Guid? Id { get; set; }

        [Column("block_id")]
        public Guid? BlockId { get; set; }

        [Column("markdown")]
        public string? Markdown { get; set; }
    }
}
