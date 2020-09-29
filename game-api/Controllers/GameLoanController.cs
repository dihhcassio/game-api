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
    public class GameLoanController : Controller
    {
        private readonly IGameLoanRepository _gameLoanRepository;
        public GameLoanController(IGameLoanRepository gameLoanRepository)
        {
            _gameLoanRepository = gameLoanRepository;
        }

        [HttpPost]
        [Route("lend")]
        public dynamic Lend([FromBody] GameLoan gameLoan)
        {
            var currentLent = _gameLoanRepository.GetDeliveredByGame(gameLoan.GameId);
            if (currentLent == null)
            {
                gameLoan.Status = LentStatus.DELIVERED;
                gameLoan.DeliveredDate = DateTime.Now;

                _gameLoanRepository.Insert(gameLoan);
                _gameLoanRepository.Save();
                return new { sucess = true, id = gameLoan.Id };
            }
            return new { sucess = false, message = "Game já foi emprestado" };
        }

        [HttpPost]
        [Route("to-receive")]
        public dynamic ToReceive([FromBody] GameLoan gameLoan)
        {
            var currentLent = _gameLoanRepository.GetDeliveredByGame(gameLoan.GameId);
            if (currentLent != null)
            {
                currentLent.Status = LentStatus.RECEIVED;
                currentLent.ReceivedDate = DateTime.Now;

                _gameLoanRepository.Update(currentLent);
                _gameLoanRepository.Save();
                return new { sucess = true, id = currentLent.Id };
            }
            return new { sucess = false, message = "Game já foi devolvido" };
        }

        [HttpGet]
        [Route("{id}")]
        public object Get(int id)
        {
            var gameLoan = _gameLoanRepository.Get(id);
            if (gameLoan == null)
                return NotFound();

            return gameLoan;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<object> GetAll()
        {
            var gamesLoans =  _gameLoanRepository.GetAll();
            return gamesLoans.Select( gl =>
                new 
                {
                    gl.Id, 
                    gl.ReceivedDate, 
                    gl.DeliveredDate, 
                    gl.Status, 
                    Friend = new 
                    {
                        gl.Friend.Id, 
                        gl.Friend.Name, 
                        gl.Friend.Phone
                    }, 
                    Game = new {
                        gl.Game.Id,
                        gl.Game.Title, 
                        gl.Game.Category
                    }
                }                
            );
        }

    }
}
