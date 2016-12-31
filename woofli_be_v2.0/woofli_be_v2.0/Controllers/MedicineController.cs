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
    public class MedicineController : ApiController
    {
        private AuthRepository _repo = null;

        public MedicineController()
        {
            _repo = new AuthRepository();
        }
        private string FindActiveUserName()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;
            return userName;
        }
        
        // GET api/<controller>/5
        [Authorize]
        public List<Medicine> Get(int id)
        {
            if (_repo.GetAllPetsForUser(FindActiveUserName()).Any(p => p.PetId == id))
            {
                return _repo.GetMedicinesByPet(id);
            }
            return null;
        }

        // GET api/<controller>/5
        [Authorize]
        [Route("api/medicine/{id}/{medicine_id}")]
        public Medicine Get(int id, int medicine_id)
        {
            if (_repo.GetAllPetsForUser(FindActiveUserName()).Any(p => p.PetId == id))
            {
                if (_repo.GetMedicinesByPet(id).Any(m => m.MedicineId == medicine_id))
                { 
                    return _repo.GetMedicinesByPet(id).SingleOrDefault(m => m.MedicineId == medicine_id);
                }
                else
                {
                    return null;
                }
            }
            return null;
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