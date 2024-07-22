using BlogApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name="Username")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name="Full Name")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="You should use min {2} character for {0}",MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Does not match password")]
        [Display(Name = "Password")]
        public string? ConfirmPassword { get; set; }
    }
}
