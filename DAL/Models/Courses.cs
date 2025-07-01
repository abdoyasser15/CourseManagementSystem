using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class Courses
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Hours { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // Foreign Keys
        
        public string InstructorId { get; set; }
        public ApplicationUsers Instructor { get; set; }
        public int CategoryID { get; set; }
        public Categorie Categorie { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
