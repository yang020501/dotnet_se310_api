using AutoMapper;
using BLL.Common;
using BLL.DTOs.Block;
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
    }
}
