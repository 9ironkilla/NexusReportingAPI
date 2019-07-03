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
    public class ServerController : ControllerBase
    {

        private readonly ApiContext _ctx;

        public ServerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var data = _ctx.Servers.OrderBy(c => c.Id);
            return Ok(data);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetServer")]
        public ActionResult<string> Get(int id)
        {
            var data = _ctx.Servers.Where(c => c.Id == id).FirstOrDefault();
            return Ok(data);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Server server)
        {
            if (server == null)
            {
                return BadRequest();
            }
            _ctx.Servers.Add(server);
            _ctx.SaveChanges();
            return Ok("Server has been created");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Server server)
        {

            if (server == null)
            {
                return BadRequest();
            }
            _ctx.Servers.Update(server);
            _ctx.SaveChanges();

            return Ok($"Server {id} has been updated");

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            var data = _ctx.Servers.Where(c => c.Id == id).FirstOrDefault();
            _ctx.Servers.Remove(data);
            _ctx.SaveChanges();

            return Ok($"Server {id} has been delete");
        }

    }


}

