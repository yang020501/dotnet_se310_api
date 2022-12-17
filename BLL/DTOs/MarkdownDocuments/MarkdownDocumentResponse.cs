using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.MarkdownDocuments
{
    public class MarkdownDocumentResponse
    {
        public Guid? Id { get; set; }
        public Guid? BlockId { get; set; }
        public string? Markdown { get; set; }
    }
}
