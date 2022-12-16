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

        public MarkdownDocument? AddDocumentToBlock(MarkdownDocumentDTO request)
        {
            try
            {
                if(request == null)
                {
                    throw new ResourceNotFoundException();
                }
               
                MarkdownDocument doc = _mapper.Map<MarkdownDocument>(request);
                _markdownDocumentRepository.Insert(doc);
                _sharedRepositories.RepositoriesManager.Saves();
                return doc;                        
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public Guid? DeleteDocumentFromBlockById(Guid? id)
        {
            try
            {
                if (!_markdownDocumentRepository.Get(doc => doc.Id == id).Any()) ;
                {
                    throw new ResourceNotFoundException();
                }

                MarkdownDocument doc = _markdownDocumentRepository.Get(doc => doc.Id == id).FirstOrDefault();
                _markdownDocumentRepository.Delete(doc);
                _sharedRepositories.RepositoriesManager.Saves();
                return id;
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

        public MarkdownDocument? UpdateDocument(UpdateDocumentRequest request)
        {
            try
            {
                if(request == null)
                {
                    throw new ResourceNotFoundException();
                }

                MarkdownDocument doc = _mapper.Map<MarkdownDocument>(request);
                _markdownDocumentRepository.Update(doc);
                _sharedRepositories.RepositoriesManager.Saves();
                return doc;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }
    }
}
