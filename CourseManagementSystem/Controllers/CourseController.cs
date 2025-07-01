using AutoMapper;
using Course.BLL.Interfaces;
using Course.DAL.Models;
using CourseManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ICatergoryRepository _catergoryRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IInstructorRepository instructorRepository
            ,ICatergoryRepository catergoryRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _catergoryRepository = catergoryRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string Search)
        {
            var Course = Enumerable.Empty<Courses>();
            if (string.IsNullOrEmpty(Search))
                Course = await _courseRepository.GetAllCoursesAsync();
            else
                Course = await _courseRepository.SearchByTitle(Search.ToLower());
            return View(Course);
        }
        public async Task<IActionResult> Create()
        {
            var instructors = await _instructorRepository.GetAllInstructorsAsync();
            var categories = await _catergoryRepository.GetAllCategoriesAsync();

            ViewBag.Instructors = instructors
                .Select(i => new SelectListItem
                {
                    Value = i.Id,
                    Text = i.FirstName + " " + i.LastName
                }).ToList();

            ViewBag.Categories = categories
                .Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var instructors = await _instructorRepository.GetAllInstructorsAsync();
                var categories = await _catergoryRepository.GetAllCategoriesAsync();
                ViewBag.Instructors = instructors
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id,
                        Text = i.FirstName + " " + i.LastName
                    }).ToList();

                ViewBag.Categories = categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = c.Name
                    }).ToList();
                return View(model);
            }
            var course = _mapper.Map<Courses>(model);
            await _courseRepository.AddCourseAsync(course);
            return RedirectToAction("Index","Admin");
        }
        
        public async Task<IActionResult> Details(string id , string ViewName = "Details")
        {
            int Id = int.Parse(id);
            var Course = await _courseRepository.GetCourseByIdAsync(Id);
            var MappedCourse = _mapper.Map<Courses, CourseViewModel>(Course);
            if (Course is null)
                return NotFound();
            return View(ViewName, MappedCourse);
        }
        public async Task<IActionResult> Delete(string id)
        {
            int Id = int.Parse(id);
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, CourseViewModel model)
        {
            if(id!=model.ID)
                return BadRequest();
            try
            {
               await _courseRepository.DeleteCourseAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            int Id = int.Parse (id);
            var course = await _courseRepository.GetCourseByIdAsync(Id);
            if (course == null)
                return NotFound();
            var model = _mapper.Map<CourseViewModel>(course);
            var instructors = await _instructorRepository.GetAllInstructorsAsync();
            var categories = await _catergoryRepository.GetAllCategoriesAsync();

            ViewBag.Instructors = instructors.Select(i => new SelectListItem
            {
                Value = i.Id,
                Text = i.FirstName + " " + i.LastName
            }).ToList();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id , CourseViewModel model)
        {
           if (id != model.ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Course = _mapper.Map<CourseViewModel,Courses>(model);
                    await _courseRepository.UpdateCourseAsync(Course);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty , ex.Message);
                }
            }
            return View(model);
        }
    }
}
