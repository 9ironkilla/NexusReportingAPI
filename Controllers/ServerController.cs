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

        [HttpGet]
        public ActionResult Get(){
            var response = _ctx.Servers.OrderBy(s => s.Id).ToList();
            return Ok(response);

        }

        [HttpGet("{id}")]
         public ActionResult Get(int id){

            var response = _ctx.Servers.Find(id);
            return Ok(response);

        }

        [HttpPut("{id}")]
        public ActionResult Message(int id, [FromBody] ServerMessage msg){

            var server = _ctx.Servers.Find(id);
            if(server == null){
                return NotFound();
            }
            if(msg.Payload == "activate"){
                server.isHealthy = true;
                
            }
              if(msg.Payload == "deactivate"){
                server.isHealthy = false;
                
            }

            _ctx.SaveChanges();
            return new NoContentResult();

        }


    }


}

