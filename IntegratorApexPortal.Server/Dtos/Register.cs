using System.ComponentModel.DataAnnotations;

namespace IntegratorApexPortal.Dtos
{
    public class Register
    {
        [Required(ErrorMessage = "The first name field is required")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "The last name field is required")]
        public string Lastname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage ="The email field is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password field is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "The phone number filed is required")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "The institution field is required")]
        public int InstitutionID { get; set; }
    }
}
