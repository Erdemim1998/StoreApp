using StoreApp.Data.Abstract;
using System.Linq;

namespace StoreApp.Data.Concrete
{
    public class EfProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public EfProductRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public async void CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
        }
    }
}
