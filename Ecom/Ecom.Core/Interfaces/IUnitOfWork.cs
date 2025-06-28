using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IGenericRepository<Product> GenericProductRepository { get; }
        public IGenericRepository<Category> GenericCategoryRepository { get; }
        public Task SaveAsync();
    }
}
