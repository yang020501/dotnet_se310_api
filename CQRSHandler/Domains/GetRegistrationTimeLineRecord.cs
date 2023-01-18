using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSHandler.Domains
{
    public class GetRegistrationTimeLineRecord
    {
        Boolean? Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean? Finished { get; set; }
    }
}
