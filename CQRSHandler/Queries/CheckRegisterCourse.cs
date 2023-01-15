using CQRSHandler.Abstractions;
using CQRSHandler.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSHandler.Queries
{
    public class CheckRegisterCourse : IQuery<CheckRegisterCourseRecords>
    {
        public string? Json { get; set; }
    }
}
