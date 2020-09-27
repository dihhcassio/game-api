using System;
using System.Collections.Generic;

namespace GameAPI.Models
{
    public class Game : BaseModel
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public List<GameLoan> GameLoans { get; set; } = new List<GameLoan>();
    }
}
