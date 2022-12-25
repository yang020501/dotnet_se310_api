using System;
using AutoMapper;
using BLL.Common;
using BLL.DTOs.Test;
using CQRSHandler.Commands;
using CQRSHandler.Queries;
using static CQRSHandler.CommandHandlers.TestCommandHandler;
using static CQRSHandler.QueryHandlers.TestQueryHandlers;

namespace BLL.Services
{
	public class TestTableService : ITestTableService
	{
        private readonly IMapper _mapper;
        private readonly ISharedRepositories _sharedRepositories;

        public TestTableService(ISharedRepositories sharedRepositories, IMapper mapper)
		{
            _sharedRepositories = sharedRepositories;
            _mapper = mapper;
		}

       

        public void InsertTestTable(InsertTestTableRequestDto request)
        {
            var command = new InsertIntoTestTable
            {
                TestColumn1 = request.TestColumn1,
                TestColumn2 = request.TestColumn2
            };

            Handle(command, _sharedRepositories.DapperContext);
        }

        public IEnumerable<InsertTestTableResponseDto> GetAll()
        {
            var query = new GetAllValueTestTable();
            var result = Handle(query, _sharedRepositories.DapperContext);

            var response = _mapper.Map<IEnumerable<InsertTestTableResponseDto>>(result);

            return response;

        }
    }
}

