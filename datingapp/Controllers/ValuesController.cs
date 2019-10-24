using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datingapp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace datingapp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase // Controller vs ControllerBase
    {
        private readonly DataContext _context; // --> naming convention. add underscore to private data.

        public ValuesController(DataContext context)
        { 
            this._context = context;
        }
        // GET api/values
        [HttpGet]
        /*  IEnumerable is a collection of a datatype. in this case; It's a collection of Strings
            ActionResult returns String as a response
            IActionResult returns O.K instead of string.
         */
        //public  ActionResult<IEnumerable<string>> Get()
        [AllowAnonymous]
        public async Task<IActionResult> GetValues() //Protected by authorization
        {
            var values = await _context.Values.ToListAsync(); 
            return Ok(values);

            /*  What this does is access database using entityframework using .Values method and convert
                to list using .ToList method. And then return to client using HTTP 200 OK response.
            */
        }

        [AllowAnonymous] //Everything beyond this can be accessed by "unauthorized" users.
        [HttpGet("{id}")]
        //getting a specific value
        public async Task<IActionResult> GetValue(int id)
        {
            /*  First vs FirstOrDefault:
                First: Returns First value. But if no values were found. it returns an exception
                        You don't want that because exceptions are costly.
                FirstOrDefault: If no value was found. it Returns NULL which is better than an exception.
            */
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
