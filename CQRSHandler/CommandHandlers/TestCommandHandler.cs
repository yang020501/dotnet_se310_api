using System;
using CQRSHandler.Abstractions;
using CQRSHandler.Commands;
using Dapper;

namespace CQRSHandler.CommandHandlers
{
	public static class TestCommandHandler
	{
		public static void Handle(InsertIntoTestTable param, IDapperContext context)
		{
			

			using(var connection = context.GetConnection())
			{
				var result = connection.Execute(InsertIntoTestTableSql, param);
				return;
			}

        }


        private const string InsertIntoTestTableSql = "EXEC InsertIntoTestTable @TestColumn1 , @TestColumn2";
    }

	
}

