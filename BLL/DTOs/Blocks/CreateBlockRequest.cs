using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Blocks
{
    public class CreateBlockRequest
    {
        public string? Name { get; set; }
        public Guid? CourseId { get; set; }
        public string? MarkdownDocument { get; set; }
    }
}
