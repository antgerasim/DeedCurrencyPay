using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeedCurrencyPay.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeedCurrencyPay.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(IAccountService accountService)
        {

        }
        /*
                // GET: api/Account
                [HttpGet]
                public async Task<IActionResult> Get()
                {
                    return new string[] { "value1", "value2" };
                }




            */


        //https://stackoverflow.com/questions/50882489/have-a-webapi-controller-send-an-http-request-to-another-rest-service
        //todo make distinct apigetService for conversionrate and inject in controller

        // GET: api/Account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Account/5
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
