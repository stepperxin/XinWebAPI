using System.ComponentModel.DataAnnotations;

namespace XinWebAPI.Models.XinIdentity
{
    public class AuthenticationRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
