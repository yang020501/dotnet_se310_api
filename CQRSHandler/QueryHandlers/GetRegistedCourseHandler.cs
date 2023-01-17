using CQRSHandler.Abstractions;
using CQRSHandler.Queries;
using DAL.Aggregates;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSHandler.QueryHandlers
{
    public class GetRegistedCourseHandler
    {
        public static IEnumerable<Course> Handle(GetRegistedCourse Id, IDapperContext context)
        {
            using (var connection = context.GetConnection())
            {
                var result = connection.Query<Course>(GetRegistationRecordsSql, Id);

                return result.ToList();

            }
        }

        private const string GetRegistationRecordsSql = "EXEC GetRegistationRecords @Id";
    }
}
