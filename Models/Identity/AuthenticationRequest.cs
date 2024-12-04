using System.ComponentModel.DataAnnotations;

namespace MySQLIdentityWebAPI.Models.Identity
{
    public class AuthenticationRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
