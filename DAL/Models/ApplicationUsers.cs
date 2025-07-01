using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public ICollection<Courses> Course { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Rating> Raitings { get; set; }
    }
}