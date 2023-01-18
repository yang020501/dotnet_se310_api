using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSHandler.Abstractions;
using CQRSHandler.Domains;
using CQRSHandler.Queries;
using Dapper;

namespace CQRSHandler.QueryHandlers
{
    public class GetRegistrationTimeLineHandler
    {
        public static IEnumerable<GetRegistrationTimeLineRecord> Handle(GetRegistrationTimeLine query, IDapperContext context)
        {
            using (var connection = context.GetConnection())
            {
                var result = connection.Query<GetRegistrationTimeLineRecord>(GetRegistrationTimeLineSql);

                return result.ToList();

            }
        }

        private const string GetRegistrationTimeLineSql = "EXEC GetRegistrationTimeLine";
    }
}
