using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyPartUsers.Models
{
    [Table("users")] 
    public class LoginPasswordDTO
    {
        public string login { get; set; }
        public string password { get; set; }
    }
}