using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="FirstName Must Have A Value")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="LastName Must Have A Value")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email Must Have A Value")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length Is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password Does Not Match Password")]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public string Roles { get; set; }
    }
}
