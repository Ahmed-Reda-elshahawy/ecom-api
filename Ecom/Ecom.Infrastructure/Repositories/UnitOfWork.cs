using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IProductRepository? productRepository;
        private ICategoryRepository? categoryRepository;
        private IGenericRepository<Product>? genericProductRepository;
        private IGenericRepository<Category>? genericCategoryRepository;
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return productRepository ??= new ProductRepository(context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return categoryRepository ??= new CategoryRepository(context);
            }
        }

        public IGenericRepository<Product> GenericProductRepository
        {
            get
            {
                return genericProductRepository ??= new GenericRepository<Product>(context);
            }
        }

        public IGenericRepository<Category> GenericCategoryRepository
        {
            get
            {
                return genericCategoryRepository ??= new GenericRepository<Category>(context);
            }
        }

        // Implement IDisposable to clean up resources
        public void Dispose()
        {
            context.Dispose();
        }

    }
}
