using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors.Client;
using Student.Interfaces;


namespace Student.Api.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(Guid id)
        {
            IStudent student = ActorProxy.Create<IStudent>(new Microsoft.ServiceFabric.Actors.ActorId(id));
            var detail = await student.Details();
            return detail;
        }

        // POST api/values
        [HttpPost]
        public async Task<Guid> Post([FromBody]InitializeStudents value)
        {
            var id =  Guid.NewGuid();
            IStudent student = ActorProxy.Create<IStudent>(new Microsoft.ServiceFabric.Actors.ActorId(id));
            await student.Initilize(value.Name);
            return id;
        }

        
    }

    public class InitializeStudents
    {
        public string Name { get; set; }
    }
}
