using GameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repositories
{
    public class UserRepository :  IUserRepository
    {
        private readonly GameDbContext _ctx;

        public UserRepository(GameDbContext ctx)
        {
            _ctx = ctx;
        }

        public User GetByEmail(string email)
        {
            return _ctx.Users.FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}
