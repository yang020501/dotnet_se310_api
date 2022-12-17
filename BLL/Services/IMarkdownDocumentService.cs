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
        public MarkdownDocumentResponse? AddDocumentToBlock(MarkdownDocumentDTO request);
        public Guid? DeleteDocumentFromBlockById(Guid? id);
        public IEnumerable<MarkdownDocumentResponse>? GetDocumentFromBlock(Guid? block_id);
        public MarkdownDocumentResponse? UpdateDocument(UpdateDocumentRequest request);     
    }
}
