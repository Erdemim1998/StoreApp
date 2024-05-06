using StoreApp.Data.Concrete;
using System.Linq;

namespace StoreApp.Data.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void CreateProduct(Product product);
    }
}
