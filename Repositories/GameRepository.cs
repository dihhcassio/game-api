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

        public IEnumerable<Game> GetAvailable(){
            var result = from game in _ctx.Games
                join gameLents in _ctx.GameLents on game.Id equals gameLents.GameId into Details
                from m in Details.DefaultIfEmpty()
                where m.Status == LentStatus.RECEIVED || game.GameLoans.Count == 0
                select new
                {
                    game.Id,
                    game.Title,
                    game.Category
                };
                
            return result.Select(r => new Game()
            {
                Id = r.Id, 
                Title = r.Title,
                Category = r.Category
            });
        }
    }
}
