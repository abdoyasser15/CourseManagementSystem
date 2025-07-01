using Course.BLL.Interfaces;
using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly CourseManagementDbContext _dbContext;
        public EnrollmentRepository(CourseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Enrollment>> GetCourseEnrollmentAsync()
        {
            return await _dbContext.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }
    }
}
