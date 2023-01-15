using CQRSHandler.Abstractions;
using CQRSHandler.Commands;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSHandler.CommandHandlers
{
    public class CancelRegistedCourseHandler
    {
		public static void Handle(CancelRegistedCourse param, IDapperContext context)
		{

			using (var connection = context.GetConnection())
			{
				var result = connection.Execute(DeleteRegistrationSql, param);
				return;
			}

		}


		private const string DeleteRegistrationSql = "EXEC DeleteRegistration @Json";
	}
}
