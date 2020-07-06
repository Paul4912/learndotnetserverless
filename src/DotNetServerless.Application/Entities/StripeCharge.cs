using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetServerless.Application.Entities
{
    public class StripeCharge
    {
        public int Amount { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
    }
}
