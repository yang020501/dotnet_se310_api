using AutoMapper;
using BLL.DTOs.MarkdownDocuments;
using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class MarkdownDocumentMapper
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<MarkdownDocumentDTO, MarkdownDocument>().AfterMap((source, destination) => destination.Id = Guid.NewGuid());
            config.CreateMap<UpdateDocumentRequest, MarkdownDocument>();
            config.CreateMap<MarkdownDocument, MarkdownDocumentResponse>();
            config.CreateMap<MarkdownDocument, MarkdownDocumentInBLock>();
        }
    }
}
