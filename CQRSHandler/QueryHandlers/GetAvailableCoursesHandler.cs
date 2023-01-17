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
    public class GetAvailableCoursesHandler
    {
        public static IEnumerable<Course> Handle(GetAvailableCourses query, IDapperContext context)
        {
            using (var connection = context.GetConnection())
            {
                var result = connection.Query<Course>(GetAvailableCoursesSql);

                return result.ToList();

            }
        }

        private const string GetAvailableCoursesSql = "EXEC GetAvailableCourses";
    }
}
