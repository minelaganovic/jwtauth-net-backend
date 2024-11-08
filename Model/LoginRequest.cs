using System.ComponentModel.DataAnnotations;

namespace serverstudent.Model
{
    public class LoginRequest
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
