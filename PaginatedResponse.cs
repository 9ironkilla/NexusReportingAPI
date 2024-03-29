using System.Collections.Generic;
using System;
using NexusReportingApi.Models;
using System.Linq;

namespace NexusReportingApi.API
{
    public class PaginatedResponse<T>
    {

        public PaginatedResponse(IEnumerable<T> data, int i, int len)
        {

            Data = data.Skip((i - 1) * len).Take(len).ToList();
            Total = data.Count();
        }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }

    }
}
