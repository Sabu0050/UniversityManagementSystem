using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.DLL.DbContext;
using UniversityManagementSystem.DLL.Repository;

namespace UniversityManagementSystem.DLL.uow
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICategoryProductRepository CategoryProductRepository { get; }
        Task<bool> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ICategoryProductRepository _categoryProductRepository;

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

        public ICategoryProductRepository CategoryProductRepository => _categoryProductRepository ??= new CategoryProductRepository(_context);

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _context.Dispose();
            }
        }
    }
}
