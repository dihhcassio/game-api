using GameAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameDbContext _ctx;

        public GameRepository(GameDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Delete(Game game)
        {
            game.Removed = true;
            Update(game);
        }

        public Game Get(int id)
        {
            return _ctx.Games
                .Include(g => g.GameLoans)
                    .ThenInclude(gl => gl.Friend)
                .Where(g => !g.Removed && g.Id == id).FirstOrDefault();
        }

        public IEnumerable<Game> GetAll()
        {
            return _ctx.Games
                .Include(g => g.GameLoans)
                    .ThenInclude(gl => gl.Friend)
                .Where(f => !f.Removed).ToList();
        }

        public IEnumerable<object> GetLents()
        {
            var query = from g in _ctx.Set<Game>()
                        join gl in _ctx.Set<GameLoan>() on g.Id equals gl.GameId
                        where !g.Removed
                        select new { g };
            return query.ToList();
        }

        public IEnumerable<object> GetNotLents()
        {
            return null;
        }

        public Game Insert(Game game)
        {
            game.CreateAt = DateTime.Now;
            _ctx.Games.Add(game);
            return game;
        }

        public int Save()
        {
            return _ctx.SaveChanges();
        }

        public Game Update(Game game)
        {
            game.UpdateAt = DateTime.Now;
            _ctx.Games.Update(game);
            return game;
        }
    }
}
