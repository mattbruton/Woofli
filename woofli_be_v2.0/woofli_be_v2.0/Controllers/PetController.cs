using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using woofli_be_v2._0.DAL;
using woofli_be_v2._0.Models;

namespace woofli_be_v2._0.Controllers
{
    public class PetController : ApiController
    {
        private AuthRepository _repo = null;

        public PetController()
        {
            _repo = new AuthRepository();
        }
        
        private string FindActiveUserName()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;
            return userName;
        }
        // GET api/<controller>
        [Authorize]
        public IEnumerable<Pet> Get()
        {
            return _repo.GetAllPetsForUser(FindActiveUserName()); 
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}