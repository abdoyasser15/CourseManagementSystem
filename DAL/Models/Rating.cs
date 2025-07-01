using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class Rating
    {
        public int ID { get; set; }
        //Foreign Keys
        public int Stars { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string StudentId { get; set; }
        public ApplicationUsers Student { get; set; }

        public int CourseId { get; set; }
        public Courses Course { get; set; }
    }
}
