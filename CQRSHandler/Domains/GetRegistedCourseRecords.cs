using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSHandler.Domains
{
    public class GetRegistedCourseRecords
    {
        public Guid? Id { get; set; }
        public string? Coursename { get; set; }
        public string? LecturerId { get; set; }
        public string? Coursecode { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DateOfWeek { get; set; }
        public Boolean? Session { get; set; }
    }
}
