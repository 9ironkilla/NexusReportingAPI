using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NexusReportingApi.Models;

namespace NexusReportingApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApiContext _ctx;

        public CustomerController(ApiContext ctx){
            _ctx = ctx;
        }

         // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var data = _ctx.Customers.OrderBy(c => c.Id);
            return Ok(data);
        }

        // GET api/values/5
        [HttpGet("{id}", Name ="GetCustomer")]
        public ActionResult<string> Get(int id)
        {
            var data = _ctx.Customers.Where( c => c.Id == id).FirstOrDefault();
            return Ok(data);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Customer customer)
        {
            if(customer == null){
                return BadRequest();
            }
            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();
            return Ok("Customer has been created");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Customer customer)
        {

            if(customer == null){
                return BadRequest();
            }
            _ctx.Customers.Update(customer);
            _ctx.SaveChanges();

             return Ok($"Customer {id} has been updated");

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            var data = _ctx.Customers.Where(c => c.Id == id).FirstOrDefault();
            _ctx.Customers.Remove(data);
            _ctx.SaveChanges();

            return Ok($"Customer {id} has been delete");
        }

    }

}