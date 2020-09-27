using GameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories
{
    public class GameLoanRepository : IGameLoanRepository
    {
        private readonly GameDbContext _ctx;

        public GameLoanRepository(GameDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Delete(GameLoan gameLent)
        {
            gameLent.Removed = true;
            Update(gameLent);
        }

        public GameLoan Get(int id)
        {
            return _ctx.GameLents
                .Include(g => g.Game)
                .Include(g => g.Friend)
                .Where(g => !g.Removed && g.Id == id).FirstOrDefault();
        }

        public IEnumerable<GameLoan> GetAll()
        {
            return _ctx.GameLents
                .Include(g => g.Game)
                .Include(g => g.Friend)
                .Where(g => !g.Removed).ToList();
        }

        public GameLoan GetDeliveredByGame(int gameId)
        {
           return _ctx.GameLents.Where(g => !g.Removed && g.GameId == gameId  && g.Status == LentStatus.DELIVERED).FirstOrDefault();
        }

        public GameLoan Insert(GameLoan gameLent)
        {
            gameLent.CreateAt = DateTime.Now;
            _ctx.GameLents.Add(gameLent);
            return gameLent;
        }

        public int Save()
        {
            return _ctx.SaveChanges();
        }

        public GameLoan Update(GameLoan gameLent)
        {
            gameLent.UpdateAt = DateTime.Now;
            _ctx.GameLents.Update(gameLent);
            return gameLent;
        }
    }
}
