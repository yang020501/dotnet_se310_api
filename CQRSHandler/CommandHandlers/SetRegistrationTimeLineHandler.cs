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
    public class SetRegistrationTimeLineHandler
    {
		public static void Handle(SetRegistrationTimeLine param, IDapperContext context)
		{

			using (var connection = context.GetConnection())
			{
				var result = connection.Execute(SetRegistrationTimelineSql, param);
				return;
			}

		}

		private const string SetRegistrationTimelineSql = "EXEC SetRegistrationTimeline @StartDate , @EndDate";

	}
}
