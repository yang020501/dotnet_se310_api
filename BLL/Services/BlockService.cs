using AutoMapper;
using BLL.Common;
using BLL.DTOs.Blocks;
using BLL.DTOs.MarkdownDocuments;
using BLL.Exceptions;
using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BlockService : IBlockService
    {
        private readonly ISharedRepositories _sharedRepositories;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Block> _blockRepository;
        private readonly ICommon _commonService;

        public BlockService(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration, ICommon commonService)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
            _configuration = configuration;
            _blockRepository = _sharedRepositories.RepositoriesManager.BlockRepository;
            _commonService = commonService;
        }

        public Block CreaetBlock(CreateBlockRequest? request)
        {
            try
            {
                Block block = _mapper.Map<Block>(request);
                _blockRepository.Insert(block);
                _sharedRepositories.RepositoriesManager.Saves();
                return block;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public Guid? DeleteBlock(Guid? blockId)
        {
            try
            {
                if(blockId is not null && _blockRepository.GetById(blockId) is not null)
                {
                    _commonService.DeleteAllMarkdownDocFromBlock(blockId);
                    _blockRepository.Delete(_blockRepository.GetById(blockId));
                    _sharedRepositories.RepositoriesManager.Saves();
                    return blockId;
                }
                
                return null;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }

        public IEnumerable<GetBlocksResponse>? GetAllBlocksFromCourse(Guid? courseId)
        {
            try
            {
                List<GetBlocksResponse> responses = new List<GetBlocksResponse>();
                if(_blockRepository.Get(block => block.CourseId == courseId).Any())
                {
                    List<Block> blocks = _blockRepository.Get(block => block.CourseId == courseId).ToList();
                    foreach (Block b in blocks)
                    {
                        GetBlocksResponse res = new GetBlocksResponse();
                        res.MarkdownDocuments = new List<MarkdownDocumentInBLock>();
                        res.Id = b.Id;

                        if(!_commonService.IsBlockEmpty(b.Id))
                        {
                            List<MarkdownDocument>? docs = _commonService.GetAllDocumentFromBlock(b.Id).ToList();
                            res.MarkdownDocuments = _mapper.Map<List<MarkdownDocumentInBLock>>(docs);
                        }
                        responses.Add(res);
                    }

                    return responses;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new ResourceNotFoundException(e.Message);
            }
        }

        public Guid? UpdateBlock(UpdateBlockRequest request)
        {
            try
            {
                if(_blockRepository.Get(b => b.Id == request.Id).Any())
                {
                    Block new_block = _blockRepository.Get(b => b.Id == request.Id).FirstOrDefault();
                    new_block.Name = request.Name;
                    _blockRepository.Update(new_block);
                    _sharedRepositories.RepositoriesManager.Saves();
                    return request.Id;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new ResourceConflictException(e.Message);
            }
        }
    }
}
