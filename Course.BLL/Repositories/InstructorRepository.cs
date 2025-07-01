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
    public class InstructorRepository : IInstructorRepository
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly CourseManagementDbContext _dbContext;

        public InstructorRepository(UserManager<ApplicationUsers> userManager , CourseManagementDbContext DbContext)
        {
            _userManager = userManager;
            _dbContext = DbContext;
        }

        public async Task<IEnumerable<ApplicationUsers>> GetAllInstructorsAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var instructors = new List<ApplicationUsers>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Instructor"))
                {
                    instructors.Add(user);
                }
            }
            return instructors;
        }

        public async Task<ApplicationUsers> GetInstructorByIdAsync(string id)
        {
            var user =  await _userManager.FindByIdAsync(id);
            if(user is not null &&await _userManager.IsInRoleAsync(user,"Instructor"))
                return user;
            return null;
        }
        public async Task<IQueryable<ApplicationUsers>> SearchByName(string name)
        {
            var User = _userManager.Users.Where(u => u.Role == "Instructor"
            && (u.FirstName.ToLower().Contains(name) || u.LastName.ToLower().Contains(name)));
            return await Task.FromResult(User);
        }

        public async Task<bool> UpdateInstructorAsync(ApplicationUsers Instructor)
        {
            var existInstructor = await _userManager.FindByIdAsync(Instructor.Id);
            if (existInstructor is null)
                return false;
            existInstructor.FirstName = Instructor.FirstName;
            existInstructor.LastName = Instructor.LastName;
            existInstructor.Email = Instructor.Email;
            existInstructor.Gender = Instructor.Gender;
            return _dbContext.Users.Update(existInstructor) != null && await _dbContext.SaveChangesAsync() > 0; 
        }
        public async Task<bool> DeleteInstructorAsync(string id)
        {
            var User = await _userManager.FindByIdAsync(id);
            if (User is null)
                return false;
            var result = await _userManager.DeleteAsync(User);
            return result.Succeeded;
        }

    }
}
