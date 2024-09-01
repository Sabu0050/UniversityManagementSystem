using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.DLL.Repository
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
       
    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository {

        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
