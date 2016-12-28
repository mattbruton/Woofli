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
using static woofli_be_v2._0.Models.WoofliViewModels;

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
        [Authorize]
        public Pet Get(int id)
        {
            if (_repo.GetAllPetsForUser(FindActiveUserName()).Any(p => p.PetId == id))
            {
                return _repo.GetPetById(id);
            }
            return null;
        }
        [Authorize]
        // POST api/<controller>
        public Dictionary<string, bool> Post([FromBody]AddPetViewModel value)
        {
            Dictionary<string, bool> answer = new Dictionary<string, bool>();

            if (ModelState.IsValid)
            {
                string user_name = FindActiveUserName();

                if (user_name != null)
                {
                    Pet new_pet = new Pet
                    {
                        Name = value.Name,
                        IsCanine = value.IsCanine,
                        ImageUrl = value.ImageUrl,
                        BirthDate = DateTime.Now 
                    };
                    _repo.AddPetToUser(user_name, new_pet);
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
        public void Put(int id, [FromBody]string value)
        {
            //NYI
        }

        // DELETE api/<controller>/5
        [Authorize]
        public Dictionary<string, bool> Delete(int id)
        {
            Dictionary<string, bool> answer = new Dictionary<string, bool>();
            if (FindActiveUserName() != null)
            {
                _repo.RemovePetById(FindActiveUserName(), id);
                answer.Add("Removed Pet", true);
            }
            else
            {
                answer.Add("Removed Pet", false);
            }
            return answer;
        }
    }
}