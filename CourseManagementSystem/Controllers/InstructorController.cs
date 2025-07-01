using AutoMapper;
using Course.BLL.Interfaces;
using Course.BLL.Repositories;
using Course.DAL.Models;
using CourseManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;

        public InstructorController(IInstructorRepository instructorRepository,IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string Search)
        {
            var Instructors = Enumerable.Empty<ApplicationUsers>();
            if (string.IsNullOrEmpty(Search))
                Instructors = await _instructorRepository.GetAllInstructorsAsync();
            else
                Instructors = await _instructorRepository.SearchByName(Search);
            var viewModel = _mapper.Map<IEnumerable<ApplicationUsers>, IEnumerable<InstructorViewModel>>(Instructors);
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            var student = await _instructorRepository.GetInstructorByIdAsync(id);
            if (student == null)
                return NotFound();
            var viewModel = _mapper.Map<ApplicationUsers, InstructorViewModel>(student);
            return View(viewModel);
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, InstructorViewModel model)
        {
            try
            {
                await _instructorRepository.DeleteInstructorAsync(id);
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
            var Student = await _instructorRepository.GetInstructorByIdAsync(id);
            if (Student is null)
                return NotFound();
            var viewModel = _mapper.Map<ApplicationUsers, InstructorViewModel>(Student);
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(InstructorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var student = _mapper.Map<InstructorViewModel, ApplicationUsers>(model);
                await _instructorRepository.UpdateInstructorAsync(student);
                TempData["Success"] = "Student updated successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
    }
}
