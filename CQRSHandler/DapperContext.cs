using System;
using System.Data;
using CQRSHandler.Abstractions;
using Microsoft.Data.SqlClient;

namespace CQRSHandler
{
	public class DapperContext : IDapperContext
	{
		private readonly string _connectionString;

		public DapperContext(string connectionString)
		{
			_connectionString = connectionString;
		}

        public IDbConnection GetConnection()
        {
			return new SqlConnection(_connectionString);
        }
    }
}

