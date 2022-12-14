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
        public IEnumerable<MarkdownDocument>? AddDocumentToBlock(AddDocumentIntoBlockRequest request);
        public IEnumerable<Guid?>? DeleteDocumentFromBlock(DeleteDocumentRequest request);
        public IEnumerable<MarkdownDocument>? GetDocumentFromBlock(Guid? block_id);
        public IEnumerable<MarkdownDocument>? UpdateDocument(UpdateDocumentRequest request);
    }
}
