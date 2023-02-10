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
    public class AddAvailableCoursesHandler
    {
		public static void Handle(AddAvailableCourses param, IDapperContext context)
		{

			using (var connection = context.GetConnection())
			{
				var result = connection.Execute(AddAvailableCoursesSql, param);
				return;
			}

		}


		private const string AddAvailableCoursesSql = "EXEC AddAvailableCourses @Json";
	}
}
