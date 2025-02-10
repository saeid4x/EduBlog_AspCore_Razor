using System.ComponentModel.DataAnnotations;

namespace SokanAcademy.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string  Name { get; set; }

        [Required]
        public string  Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email   { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="password not match")]
        public string ConfirmPassword { get; set; }


    }
}
