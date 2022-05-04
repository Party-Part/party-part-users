using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyPartUsers.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? user_id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? telegram_id { get; setu; }
    }
}