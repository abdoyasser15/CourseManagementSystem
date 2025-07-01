using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<ApplicationUsers>> GetAllInstructorsAsync();
        Task<ApplicationUsers> GetInstructorByIdAsync(string id);
        Task<bool> UpdateInstructorAsync(ApplicationUsers student);
        Task<bool> DeleteInstructorAsync(string id);
        Task<IQueryable<ApplicationUsers>> SearchByName(string name);
    }
}
