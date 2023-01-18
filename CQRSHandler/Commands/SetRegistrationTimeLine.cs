using CQRSHandler.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSHandler.Commands
{
    public class SetRegistrationTimeLine : ICommand
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
