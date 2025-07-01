using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.ViewModels
{
    public class LogInViewModel
    {
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Must Have A Value")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length Is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
