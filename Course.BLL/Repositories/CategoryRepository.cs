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
    public class CategoryRepository : ICatergoryRepository
    {
        private readonly CourseManagementDbContext _dbcontext;
        public CategoryRepository(CourseManagementDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IEnumerable<Categorie>> GetAllCategoriesAsync()
        {
             return  await _dbcontext.Categories.ToListAsync();
        }
    }
}
