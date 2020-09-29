using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPI.Models
{
    public class GameLoan:BaseModel
    {
        [Required]
        public LentStatus Status { get; set; }
        public DateTime DeliveredDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        [Required]
        public int GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
        [Required]
        public int FriendId { get; set; }

        [JsonIgnore]
        public Friend Friend { get; set; }

    }

    public enum LentStatus { 
        DELIVERED = 1, 
        RECEIVED = 2
    }
}
