using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.MarkdownDocuments
{
    public class DeleteDocumentRequest
    {
        public List<MarkdownDocument>? DeleteDocumentList;
    }
}
