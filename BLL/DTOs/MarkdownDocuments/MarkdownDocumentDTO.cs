using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.MarkdownDocuments
{
    public class MarkdownDocumentDTO
    {
        public Guid? BlockId { get; set; }
        public string? Markdown { get; set; }
    }
}
