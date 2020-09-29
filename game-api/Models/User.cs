
namespace GameAPI.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; }
        // Colocar um hash no lugar
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
