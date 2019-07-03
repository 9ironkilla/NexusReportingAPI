using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusReportingApi.API;
using NexusReportingApi.Models;

namespace NexusReportingApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApiContext _ctx;

        public OrdersController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/values
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public ActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Orders.Include(o => o.Customer).OrderByDescending(c => c.Placed);
            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);
            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);


            var response = new {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        //GET api/values/5
        [HttpGet("ByState")]
        public ActionResult<string> ByState()
        {
              var data = _ctx.Orders.Include(o => o.Customer).ToList();

              var groupedResult = data.GroupBy(o => o.Customer.State)
              .ToList()
              .Select(grp => new {
                State = grp.Key,
                Total = grp.Sum(x => x.orderTotal)
              }).ToList();

            return Ok(groupedResult);
        }

          [HttpGet("ByCustomer/{n}")]
        public ActionResult<string> ByCustomer(int n)
        {
              var data = _ctx.Orders.Include(o => o.Customer).ToList();
              var groupedResult = data.GroupBy(o => o.Customer.Id).ToList()
              .Select(grp => new {
                Name = _ctx.Customers.Find(grp.Key).Name,
                Total = grp.Sum(x => x.orderTotal)
              }).OrderByDescending(res => res.Total).Take(n).ToList();

            return Ok(groupedResult);
        }

        [HttpGet("GetOrder/{id}")]
        public ActionResult<string> GetOrder(int id)
        {
            var  order = _ctx.Orders.
                Include(o => o.Customer)
                .FirstOrDefault(o => o.Id == id);

            return Ok(order);
        }

    }

}

