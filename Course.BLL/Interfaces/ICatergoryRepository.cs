using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface ICatergoryRepository
    {
        Task<IEnumerable<Categorie>> GetAllCategoriesAsync();
    }
}
