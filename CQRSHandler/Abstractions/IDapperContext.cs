using System;
using System.Data;

namespace CQRSHandler.Abstractions
{
	public interface IDapperContext
	{
		public IDbConnection GetConnection();
	}
}

