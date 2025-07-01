using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Courses>> GetAllCoursesAsync();
        Task<Courses> GetCourseByIdAsync(int id);
        Task<bool> UpdateCourseAsync(Courses Course);
        Task<bool> DeleteCourseAsync(int id);
        Task<bool> AddCourseAsync(Courses Course);
        public Task<IEnumerable<Courses>> SearchByTitle(string Search);
    }
}
