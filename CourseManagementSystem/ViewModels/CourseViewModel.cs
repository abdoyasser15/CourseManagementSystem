using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.ViewModels
{
    public class CourseViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Hours { get; set; }

        [Required]
        [Range(0, 99999)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Instructor Name")]
        public string InstructorId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Instructors { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
