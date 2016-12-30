using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using woofli_be_v2._0.DAL;
using woofli_be_v2._0.Models;
using static woofli_be_v2._0.Models.WoofliViewModels;

namespace woofli_be_v2._0.Controllers
{
    public class VeterinarianController : ApiController
    {
        private AuthRepository _repo = null;

        public VeterinarianController()
        {
            _repo = new AuthRepository();
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [Authorize]
        public Veterinarian Get(int id)
        {
            return _repo.GetVeterinarianByPetId(id);
        }

        // POST api/<controller>
        [Authorize]
        public Dictionary<string, bool> Post([FromBody]AddPrimaryVetViewModel value)
        {
            Dictionary<string, bool> answer = new Dictionary<string, bool>();

            if (ModelState.IsValid)
            {
                Veterinarian new_vet = new Veterinarian
                {
                    ClinicName = value.ClinicName,
                    Phone = value.Phone,
                    StreetAddress = value.StreetAddress,
                    City = value.City,
                    ZipCode = value.ZipCode,
                    State = value.State

                    };
                    _repo.AddVeterinarianToPetByPetId(value.PetId, new_vet);
                    answer.Add("successful", true);
                    return answer;
                }
            else
            {
                answer.Add("successful", false);
                return answer;
            }
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