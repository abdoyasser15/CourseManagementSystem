using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<ApplicationUsers>> GetAllStudentsAsync();
        Task<ApplicationUsers> GetStudentByIdAsync(string id);
        Task<bool>UpdateStudentAsync(ApplicationUsers student);
        Task<bool> DeleteStudentAsync(string id);
        Task<IQueryable<ApplicationUsers>> SearchByName(string name);
        Task<IEnumerable<Courses>> GetStudentCoursesAsync(string Id);
        Task<IEnumerable<Courses>> GetNotEnrollStudentCourseAsync(string Id);
        Task<bool> IsStudentEnrolledAsync(string studentId, int courseId);
        Task<bool> EnrolledStudentCoursesAsync(string studentId, int courseId);
    }
}
