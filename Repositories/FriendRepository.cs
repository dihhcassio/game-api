using GameAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly GameDbContext _ctx;

        public FriendRepository(GameDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Delete(Friend friend)
        {
            friend.Removed = true;
            Update(friend);
        }

        public Friend Get(int id)
        {
            return _ctx.Friends
                .Include(f => f.GameLoans)
                    .ThenInclude(gl => gl.Game)
                .Where(f => !f.Removed && f.Id == id).FirstOrDefault();
        }

        public IEnumerable<Friend> GetAll()
        {
            return _ctx.Friends
                .Include(f => f.GameLoans)
                    .ThenInclude(gl => gl.Game)
                .Where(f => !f.Removed).ToList();
        }

        public Friend Insert(Friend friend)
        {
            friend.CreateAt = DateTime.Now;
            _ctx.Friends.Add(friend);
            return friend;
        }

        public int Save()
        {
            return _ctx.SaveChanges();
        }

        public Friend Update(Friend friend)
        {
            friend.UpdateAt = DateTime.Now;
            _ctx.Friends.Add(friend);
            return friend;
        }
    }
}
