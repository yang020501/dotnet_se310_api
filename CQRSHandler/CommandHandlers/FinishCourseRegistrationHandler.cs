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
    public class FinishCourseRegistrationHandler
    {
		public static void Handle(FinishCourseRegistration param, IDapperContext context)
		{

			using (var connection = context.GetConnection())
			{
				var result = connection.Execute(FinishCourseRegistrationSql, param);
				return;
			}

		}


		private const string FinishCourseRegistrationSql = "EXEC FinializeRegistration";
	}
}
