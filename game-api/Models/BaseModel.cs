using System.Text.Json;
using System.Text.Json.Serialization;
using System;

namespace GameAPI.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime CreateAt { get; set; }
        [JsonIgnore]
        public DateTime UpdateAt { get; set; }
        [JsonIgnore]
        public bool Removed { get; set; }
    }
}
