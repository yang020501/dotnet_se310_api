using System;
using System.Linq;
using CQRSHandler.Abstractions;
using CQRSHandler.Commands;
using CQRSHandler.Domains;
using CQRSHandler.Queries;
using Dapper;

namespace CQRSHandler.QueryHandlers
{
	public static class TestQueryHandlers
	{
        public static IEnumerable<TestTableRecords> Handle(GetAllValueTestTable criteria, IDapperContext context)
        {


            using (var connection = context.GetConnection())
            {
                var result = connection.Query<TestTableRecords>(GetAllValueTestTableSql, criteria);

                return result.ToList();
                
            }

        }

        private const string GetAllValueTestTableSql = "EXEC GetAllValuesTestTable";
    }
}

