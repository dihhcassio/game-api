using GameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repositories
{
    public interface IGameLoanRepository : IBaseRepository<GameLoan>
    {
        GameLoan GetDeliveredByGame(int gameId);
      
    }
}
