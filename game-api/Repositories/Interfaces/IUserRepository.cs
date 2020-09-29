using GameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repositories
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
    }
}
