using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using woofli_be_v2._0.Models;

namespace woofli_be_v2._0.DAL
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _context;

        private UserManager<CustomUser> _userManager;

        public AuthRepository(AuthContext _ctx)
        {
            _context = _ctx;
            _userManager = new UserManager<CustomUser>(new UserStore<CustomUser>(_ctx));
        }
        public AuthRepository()
        {
            _context = new AuthContext();
            _userManager = new UserManager<CustomUser>(new UserStore<CustomUser>(_context));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            CustomUser user = new CustomUser
            {
                UserName = userModel.UserName,
                PhoneNumber = userModel.PhoneNumber,
                Email = userModel.Email,
                PrefersContactByPhone = userModel.PrefersContactByPhone
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public List<Petsitter> GetAllPetsittersForUser(string username)
        {
            CustomUser user = _context.Users.SingleOrDefault(u => u.UserName == username);
            return user.Petsitters;
        }

        public void AddPetsitterToUser(string username, Petsitter petsitter)
        {
            _context.Users.SingleOrDefault(u => u.UserName == username).Petsitters.Add(petsitter);
            _context.SaveChanges();
        }

        public async Task<CustomUser> FindUser(string userName, string password)
        {
            CustomUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _context.Dispose();
            _userManager.Dispose();

        }
        public List<Pet> GetAllPetsForUser(string username)
        {
            return _context.Pets.Where(p => p.Owner.UserName == username).ToList();
        }

        public Pet GetPetById(int _id)
        {
            return _context.Pets.SingleOrDefault(p => p.PetId == _id);
        }

        public Petsitter GetPetsitterById(int _id)
        {
            return _context.Petsitters.SingleOrDefault(p => p.PetsitterId == _id);
        }

        public void RemovePetsitterById(string username, int _id)
        {
            CustomUser user = _context.Users.FirstOrDefault(u => u.UserName == username);
            Petsitter sitter_to_remove = _context.Petsitters.FirstOrDefault(p => p.PetsitterId == _id);
            
            if (sitter_to_remove != null)
            {
                user.Petsitters.Remove(sitter_to_remove);
                _context.Petsitters.Remove(sitter_to_remove);
                _context.SaveChanges();
            }
        }

        public void RemovePetById(string username, int _id)
        {
            CustomUser user = _context.Users.FirstOrDefault(u => u.UserName == username);
            Pet pet_to_remove = _context.Pets.FirstOrDefault(p => p.PetId == _id);

            if (pet_to_remove != null)
            {
                user.Pets.Remove(pet_to_remove);
                _context.Pets.Remove(pet_to_remove);
                _context.SaveChanges();
            }
        }

        public void AddPetToUser(string username, Pet pet)
        {
            _context.Users.SingleOrDefault(u => u.UserName == username).Pets.Add(pet);
            _context.SaveChanges();
        }

        public Veterinarian GetVeterinarianByPetId(int pet_id)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.PetId == pet_id);
            return pet.PrimaryVet;
        }

        public void AddVeterinarianToPetByPetId(int pet_id, Veterinarian vet)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.PetId == pet_id);
            _context.Veterinarians.Add(vet);
            pet.PrimaryVet = vet;
            _context.SaveChanges();
        }

        public void RemoveVeterinarianFromPet(int pet_id)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.PetId == pet_id);
            _context.Veterinarians.Remove(pet.PrimaryVet);
            pet.PrimaryVet = null;
            _context.SaveChanges();
        }

        public List<Medicine> GetMedicinesByPet(int pet_id)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.PetId == pet_id);
            if (pet.Medications.Count > 0)
            {
                return pet.Medications;
            }
            else
            {
                return null;
            }
        }

        public void AddMedicationToPet(int pet_id, Medicine med)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.PetId == pet_id);
            pet.Medications.Add(med);
            _context.Medicines.Add(med);
            _context.SaveChanges();
        }

        public void RemoveMedicineFromPet(int med_id)
        {
            Medicine med = _context.Medicines.SingleOrDefault(m => m.MedicineId == med_id);
            
            _context.Medicines.Remove(med);
            _context.SaveChanges();
        }
    }
}