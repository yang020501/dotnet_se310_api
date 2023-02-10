using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSHandler.Abstractions;
using CQRSHandler.Commands;
using Dapper;

namespace CQRSHandler.CommandHandlers
{
    public class CreateAvailableCoursesForRegistrationHandler
    {
		public static void Handle(CreateAvailableCourse param, IDapperContext context)
		{

			using (var connection = context.GetConnection())
			{
				var result = connection.Execute(CreateAvailableCoursesForRegistrationSql, param);
				return;
			}

		}


		private const string CreateAvailableCoursesForRegistrationSql = "EXEC CreateAvailableCoursesForRegistration @Json";
	}
}
