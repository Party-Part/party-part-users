using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyPartUsers.Models
{
    [Table("users")] 
    public class UserDTO
    {
        public long? user_id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public long? telegram_id { get; set; }
    }
}