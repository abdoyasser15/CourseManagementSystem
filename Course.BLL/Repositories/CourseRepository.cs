using Course.BLL.Interfaces;
using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseManagementDbContext _dbContxt;

        public CourseRepository(CourseManagementDbContext dbContxt)
        {
            _dbContxt = dbContxt;
        }
        public async Task<bool> AddCourseAsync(Courses Course)
        {
            await _dbContxt.Courses.AddAsync(Course);
            return await _dbContxt.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var Course = await _dbContxt.Courses.FindAsync(id);
            if (Course is null)
                return false;
            _dbContxt.Courses.Remove(Course);
            return await _dbContxt.SaveChangesAsync()>0;
        }

        public async Task<IEnumerable<Courses>> GetAllCoursesAsync()
        {
            return await _dbContxt.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Categorie)
                .ToListAsync();
        }

        public async Task<Courses> GetCourseByIdAsync(int id)
        {
            return await _dbContxt.Courses.Include(C => C.Categorie).FirstOrDefaultAsync(C=>C.ID==id);
        }

        public async Task<bool> UpdateCourseAsync(Courses Course)
        {
            var existingCourse = await _dbContxt.Courses.FindAsync(Course.ID);
            if (existingCourse == null)
                return false;
            existingCourse.Title = Course.Title;
            existingCourse.Description = Course.Description;
            existingCourse.Hours = Course.Hours;
            existingCourse.Price = Course.Price;
            existingCourse.StartDate = Course.StartDate;
            existingCourse.EndDate = Course.EndDate;
            existingCourse.InstructorId = Course.InstructorId;
            existingCourse.CategoryID = Course.CategoryID;
            return await _dbContxt.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Courses>> SearchByTitle(string Search)
        {
           return await _dbContxt.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Categorie)
                .Where(c => c.Title != null && c.Title.ToLower().Contains(Search.ToLower()))
                .ToListAsync();
        }
    }
}
