using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPI.Models;
using GameAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpPost]
        [Route("")]
        public dynamic Insert([FromBody] Game game)
        {
            _gameRepository.Insert(game);
            _gameRepository.Save();
            return new { sucess = true, id = game.Id };
        }

        [HttpPut]
        [Route("")]
        public dynamic Update([FromBody] Game game)
        {
            _gameRepository.Update(game);
            _gameRepository.Save();
            return new { sucess = true, id = game.Id };
        }


        [HttpDelete]
        [Route("")]
        public dynamic Delete([FromBody] Game game)
        {
            _gameRepository.Delete(game);
            _gameRepository.Save();
            return new { sucess = true };
        }

        [HttpGet]
        [Route("{id}")]
        public object Get(int id)
        {
            var game =  _gameRepository.Get(id);
            return new
            {
                game.Id,
                game.Title,
                game.Category,
                CurrentLoan = game.GameLoans.Where(gl => gl.Status == LentStatus.DELIVERED)
                    .Select(gl =>
                        new
                        {
                            gl.Status,
                            gl.DeliveredDate,
                            gl.ReceivedDate,
                            FriendName = gl.Friend.Name
                        })
                    .FirstOrDefault()
            };
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<object> GetAll()
        {
            var games = _gameRepository.GetAll();

            return games.Select(g =>
            new
            {
                g.Id,
                g.Title,
                g.Category,
                CurrentLoan = g.GameLoans.Where(gl => gl.Status == LentStatus.DELIVERED)
                    .Select(gl => 
                        new { gl.Status, 
                            gl.DeliveredDate, 
                            gl.ReceivedDate, 
                            FriendName = gl.Friend.Name })
                    .FirstOrDefault()
            }).ToList();
        }


    }
}
