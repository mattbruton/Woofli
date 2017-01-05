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

        [Authorize]
        // POST api/<controller>
        public Dictionary<string, bool> Post([FromBody]AddMedicationViewModel value)
        {
            Dictionary<string, bool> answer = new Dictionary<string, bool>();

            if (ModelState.IsValid)
            {
                Medicine new_med = new Medicine
                {
                    Name = value.Name,
                    PrescriptionQuantity = value.PrescriptionQuantity,
                    DoesPrescriptionGetRefill = value.DoesPrescriptionGetRefill,
                    Dosage = value.Dosage,
                    DosageInterval = value.DosageInterval,
                    DosageIntervalUnit = value.DosageIntervalUnit,
                    DosageUnit = value.DosageUnit,
                    DosageTime = DateTime.Now
                };
                _repo.AddMedicationToPet(value.PetId, new_med);
                answer.Add("successful", true);
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
        }

        // DELETE api/<controller>/5
        [Authorize]
        [HttpDelete]
        public Dictionary<string, bool> Delete(int id)
        {
            Dictionary<string, bool> answer = new Dictionary<string, bool>();
            if (_repo.GetAllPetsForUser(FindActiveUserName()).Any(p => p.Medications.SingleOrDefault(m => m.MedicineId == id) != null))
            {
                _repo.RemoveMedicineFromPet(id);
                answer.Add("successful", true);
                return answer;
            }
            else
            {
                answer.Add("successful", false);
                return answer;
            }
        }
    }
}