using BlogApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="You should use min {2} character for {0}",MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
    }
}
