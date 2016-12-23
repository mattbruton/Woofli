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
                UserName = userModel.UserName
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
    }
}