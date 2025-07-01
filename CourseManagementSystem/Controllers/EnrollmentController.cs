using Course.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentController(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<IActionResult> Index(string Search)
        {
            var enrollment = await _enrollmentRepository.GetCourseEnrollmentAsync();
            if (!string.IsNullOrEmpty(Search))
            {
                Search = Search.ToLower();
                enrollment = enrollment.Where(e => e.Course.Title.ToLower()==Search)
                    .ToList();
            }
            return View(enrollment);
        }
    }
}
