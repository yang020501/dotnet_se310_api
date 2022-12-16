using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.MarkdownDocuments;

namespace BLL.Services
{
    public interface IMarkdownDocumentService
    {
        public MarkdownDocument? AddDocumentToBlock(MarkdownDocumentDTO request);
        public Guid? DeleteDocumentFromBlockById(Guid? id);
        public IEnumerable<MarkdownDocument>? GetDocumentFromBlock(Guid? block_id);
        public MarkdownDocument? UpdateDocument(UpdateDocumentRequest request);     
    }
}
