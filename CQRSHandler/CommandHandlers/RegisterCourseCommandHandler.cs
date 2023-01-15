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
    public class RegisterCourseCommandHandler
    {
		public static void Handle(RegisterCourse param, IDapperContext context)
		{

			using (var connection = context.GetConnection())
			{
				var result = connection.Execute(RegisterCourseSql, param);
				return;
			}

		}


		private const string RegisterCourseSql = "EXEC RegisterCourse @Json";
	}
}
