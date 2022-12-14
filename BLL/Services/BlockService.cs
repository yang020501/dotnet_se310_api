using AutoMapper;
using BLL.Common;
using BLL.DTOs.Block;
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
    public class BlockService : IBlockService
    {
        private readonly ISharedRepositories _sharedRepositories;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Block> _blockRepository;

        public BlockService(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration)
        {
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
            _configuration = configuration;
            _blockRepository = _sharedRepositories.RepositoriesManager.BlockRepository;
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
    }
}
