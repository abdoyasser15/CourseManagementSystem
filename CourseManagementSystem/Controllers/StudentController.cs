using AutoMapper;
using Course.BLL.Interfaces;
using Course.BLL.Repositories;
using Course.DAL.Models;
using CourseManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
namespace CourseManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository , IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string Search)
        {
            var students = Enumerable.Empty<ApplicationUsers>();
            if (string.IsNullOrEmpty(Search))
                students = await _studentRepository.GetAllStudentsAsync();
            else
                students = await _studentRepository.SearchByName(Search.ToLower());
           var viewModel = _mapper.Map<IEnumerable<ApplicationUsers>, IEnumerable<StudentViewModel>>(students);
           return View(students);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id , string ViewName = "Details")
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();
            var viewModel = _mapper.Map<ApplicationUsers, StudentViewModel>(student);
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, StudentViewModel model)
        {
            try
            {
                await _studentRepository.DeleteStudentAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var Student = await _studentRepository.GetStudentByIdAsync(id);
            if (Student is null)
                return NotFound();
            var viewModel = _mapper.Map<ApplicationUsers, StudentViewModel>(Student);
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(StudentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var student = _mapper.Map<StudentViewModel, ApplicationUsers>(model);
                await _studentRepository.UpdateStudentAsync(student);
                TempData["Success"] = "Student updated successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyCourses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var student = await _studentRepository.GetStudentByIdAsync(userId);
            if (student == null)
                return NotFound();

            var enrollCourses = await _studentRepository.GetStudentCoursesAsync(userId);
            return View(enrollCourses);
        }
        [Authorize(Roles="Student")]
        public async Task<IActionResult> Courses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = await _studentRepository.GetStudentByIdAsync(userId);
            if (student == null)
                return NotFound();
            var notEnrolled = await _studentRepository.GetNotEnrollStudentCourseAsync(userId);
            return View(notEnrolled);
        }
        [HttpPost]
        public async Task<IActionResult> Register(int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isAlreadyEnrolled = await _studentRepository.IsStudentEnrolledAsync(userId, courseId);

            if (!isAlreadyEnrolled)
            {
                var result = await _studentRepository.EnrolledStudentCoursesAsync(userId, courseId);

                if (result)
                {
                    TempData["Success"] = "You have been successfully enrolled in the course.";
                }
                else
                {
                    TempData["Error"] = "An error occurred while registering.";
                }
            }
            else
            {
                TempData["Warning"] = "You are already enrolled in this course.";
            }
            return RedirectToAction("Courses");
        }
    }
}
