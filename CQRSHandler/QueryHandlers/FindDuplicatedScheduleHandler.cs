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
    public class FindDuplicatedScheduleHandler
    {

        public static IEnumerable<Course> Handle(CheckRegisterCourse json, IDapperContext context)
        {
            using (var connection = context.GetConnection())
            {
                var result = connection.Query<Course>(FindDuplicatedScheduleSql, json);

                return result.ToList();

            }
        }

        private const string FindDuplicatedScheduleSql = "EXEC FindDuplicatedSchedule @Json";
    }
}
