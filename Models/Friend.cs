using System.Collections.Generic;

namespace GameAPI.Models
{
    public class Friend : BaseModel
    {

        public string Name { get; set; }
        public string Phone { get; set; }

        public List<GameLoan> GameLoans { get; } = new List<GameLoan>();
        
         }
}
