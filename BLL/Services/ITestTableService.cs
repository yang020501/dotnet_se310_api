using System;
using BLL.DTOs.Test;

namespace BLL.Services
{
	public interface ITestTableService
	{
		public void InsertTestTable(InsertTestTableRequestDto request);
		public IEnumerable<InsertTestTableResponseDto> GetAll();

    }
}

