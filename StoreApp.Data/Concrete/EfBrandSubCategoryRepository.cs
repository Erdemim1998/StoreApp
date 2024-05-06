using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EfBrandSubCategoryRepository : IBrandSubCategoryRepository
    {
        private readonly StoreContext _context;

        public EfBrandSubCategoryRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<BrandSubCategory> BrandSubCategories => _context.BrandSubCategories;

        public async void CreateBrand(BrandSubCategory brandSubCategory)
        {
            await _context.AddAsync(brandSubCategory);
            _context.SaveChanges();
        }
    }
}
