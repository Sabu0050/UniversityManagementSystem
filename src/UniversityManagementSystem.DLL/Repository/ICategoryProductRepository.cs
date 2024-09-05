using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.DLL.Repository
{
    public interface ICategoryProductRepository : IRepositoryBase<CategoryProduct>
    {
    }

    public class CategoryProductRepository : RepositoryBase<CategoryProduct>, ICategoryProductRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryProductRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }
    }
}
