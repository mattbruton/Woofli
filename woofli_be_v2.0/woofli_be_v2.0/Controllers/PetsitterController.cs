using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using woofli_be_v2._0.DAL;
using woofli_be_v2._0.Models;
using static woofli_be_v2._0.Models.WoofliViewModels;

namespace woofli_be_v2._0.Controllers
{
    public class PetsitterController : ApiController
    {
        private AuthRepository _repo = null;

        public PetsitterController()
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
        public IEnumerable<Petsitter> Get()
        {
            return _repo.GetAllPetsittersForUser(FindActiveUserName());
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        // POST api/<controller>
        public Dictionary<string, bool> Post([FromBody]PetsitterViewModel value)
        {
            Dictionary<string, bool> answer = new Dictionary<string, bool>();

            if (ModelState.IsValid)
            {
                string user_name = FindActiveUserName();

                if (user_name != null)
                {
                    Petsitter new_sitter = new Petsitter
                    {
                        FirstName = value.FirstName,
                        LastName = value.LastName,
                        Phone = value.Phone,
                        Email = value.Email
                    };
                    _repo.AddPetsitterToUser(user_name, new_sitter);
                    answer.Add("successful", true);
                }
                else
                {
                    answer.Add("successful", false);
                }
            }
            else
            {
                answer.Add("successful", false);
            }
            return answer;
        }

        // PUT api/<controller>/5
        public void Put([FromBody]PetsitterViewModel value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}