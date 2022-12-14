using AutoMapper;
using BLL.Common;
using BLL.DTOs.MarkdownDocuments;
using BLL.Exceptions;
using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MarkdownDocumentService : IMarkdownDocumentService
    {
        private readonly ISharedRepositories _sharedRepositories;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<MarkdownDocument> _markdownDocumentRepository;

        public MarkdownDocumentService(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
            _configuration = configuration;
            _markdownDocumentRepository = _sharedRepositories.RepositoriesManager.MarkdownDocumentRepository;
        }

        public IEnumerable<MarkdownDocument>? AddDocumentToBlock(AddDocumentIntoBlockRequest request)
        {
            try
            {
                List<MarkdownDocument> response = new List<MarkdownDocument>();
                foreach (MarkdownDocumentDTO dTO in request.MarkdownDocumentList)
                {
                    MarkdownDocument markdownDocument = _mapper.Map<MarkdownDocument>(dTO);
                    _markdownDocumentRepository.Insert(markdownDocument);
                    response.Add(markdownDocument);
                }

                _sharedRepositories.RepositoriesManager.Saves();
                return response;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public IEnumerable<Guid?>? DeleteDocumentFromBlock(DeleteDocumentRequest request)
        {
            try
            {
                List<Guid?> response = new List<Guid?>();
                foreach (MarkdownDocument doc in request.DeleteDocumentList)
                {                   
                    _markdownDocumentRepository.Delete(doc);
                    response.Add(doc.Id);
                }

                _sharedRepositories.RepositoriesManager.Saves();
                return response;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public IEnumerable<MarkdownDocument>? GetDocumentFromBlock(Guid? block_id)
        {
            try
            {
                List<MarkdownDocument> response = new List<MarkdownDocument>();
                response = _markdownDocumentRepository.Get(doc => doc.BlockId == block_id).ToList();

                return response;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public IEnumerable<MarkdownDocument>? UpdateDocument(UpdateDocumentRequest request)
        {
            try
            {
                List<MarkdownDocument> response = new List<MarkdownDocument>();
                foreach (MarkdownDocument doc in request.UpdateDocumentList)
                {                   
                    _markdownDocumentRepository.Update(doc);
                    response.Add(doc);
                }

                _sharedRepositories.RepositoriesManager.Saves();
                return response;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }
    }
}
