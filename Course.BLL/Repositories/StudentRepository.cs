using Course.BLL.Interfaces;
using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly CourseManagementDbContext _dbContext;

        public StudentRepository(UserManager<ApplicationUsers> userManager , CourseManagementDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public async Task<bool> DeleteStudentAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        public async Task<IEnumerable<ApplicationUsers>> GetAllStudentsAsync()
        {
            var users = _userManager.Users.ToList();
            var students = new List<ApplicationUsers>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Student"))
                {
                    students.Add(user);
                }
            }

            return students;
        }
        public async Task<ApplicationUsers> GetStudentByIdAsync(string id)
        {
            return await _userManager.Users
                        .Include(s => s.Enrollments)
                        .ThenInclude(e => e.Course)
                        .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<IEnumerable<Courses>> GetStudentCoursesAsync(string Id)
        {
            return await _dbContext.Enrollments.
                Include(E => E.Course)
                .ThenInclude(I => I.Instructor)
                .Include(I => I.Course)
                    .ThenInclude(C => C.Categorie)
                .Where(E => E.StudentId == Id)
                .Select(E => E.Course)
                .ToListAsync();
        }
        public async Task<IEnumerable<Courses>> GetNotEnrollStudentCourseAsync(string Id)
        {
            var enrolledCourseIds = await _dbContext.Enrollments
                .Where(e => e.StudentId == Id)
                .Select(e => e.CourseId)
                .ToListAsync();

            return await _dbContext.Courses.
                            Include(C=>C.Categorie)
                            .Include(C=>C.Instructor)
                            .Where(C=> !enrolledCourseIds.Contains(C.ID))
                            .ToListAsync();
        } 
        public async Task<bool> UpdateStudentAsync(ApplicationUsers student)
        {
            var existingUser = await _userManager.FindByIdAsync(student.Id);
            if (existingUser == null) return false;

            existingUser.FirstName = student.FirstName;
            existingUser.LastName = student.LastName;
            existingUser.Email = student.Email;
            existingUser.Gender = student.Gender;
            return _dbContext.Users.Update(existingUser) != null && await _dbContext.SaveChangesAsync() > 0;
        }
        public Task<IQueryable<ApplicationUsers>> SearchByName(string name)
        {
            var students =  _userManager.Users.Where(u => u.Role == "Student"
            && (u.FirstName.ToLower().Contains(name) || u.LastName.ToLower().Contains(name)));
            return Task.FromResult(students);
        }
        public async Task<bool> IsStudentEnrolledAsync(string studentId, int courseId)
        {
            return await _dbContext.Enrollments.
                AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
        public async Task<bool> EnrolledStudentCoursesAsync(string studentId, int courseId)
        {
            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollDate = DateTime.Now
            };
            _dbContext.Enrollments.Add(enrollment);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
