using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSHandler.Abstractions;
using CQRSHandler.Domains;

namespace CQRSHandler.Queries
{
    public class GetRegistedCourse : IQuery<GetRegistedCourseRecords>
    {
        public string? Id { get; set; }
    }
}
