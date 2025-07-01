using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class Enrollment
    {
        public int ID { get; set; }
        public DateTime EnrollDate { get; set; }
        //Foreign Keys
        public string StudentId { get; set; }
        public ApplicationUsers Student { get; set; }
        public int CourseId { get; set; }
        public Courses Course { get; set; }

    }
}
