using System;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.ViewModels
{
    public class StudentViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTime? EnrollmentDate { get; set; }
    }
}
