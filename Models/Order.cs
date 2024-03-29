using System;

namespace NexusReportingApi.Models
{
    public class Order{
        public int Id {get;set;}
        public Customer Customer {get;set;}
        public decimal orderTotal{get;set;}
        public DateTime Placed {get;set;}
        public DateTime? Completed {get;set;}
    }
}