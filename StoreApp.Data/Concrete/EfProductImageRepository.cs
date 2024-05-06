using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EfProductImageRepository : IProductImageRepository
    {
        private readonly StoreContext _context;

        public EfProductImageRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<ProductImage> ProductImages => _context.ProductImages;

        public async void CreateProductImages(ProductImage productImage)
        {
            await _context.ProductImages.AddAsync(productImage);
            _context.SaveChanges();
        }
    }
}
