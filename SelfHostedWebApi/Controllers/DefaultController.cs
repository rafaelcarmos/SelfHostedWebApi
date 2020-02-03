using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Default.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SelfHostedWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DefaultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Default
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var query = new DefaultGetQuery();
            return await _mediator.Send(query);
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
