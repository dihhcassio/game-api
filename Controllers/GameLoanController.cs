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
        public GameLoan Get(int id)
        {
            return _gameLoanRepository.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<GameLoan> GetAll()
        {
            return _gameLoanRepository.GetAll();
        }

    }
}
