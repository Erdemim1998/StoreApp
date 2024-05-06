using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private readonly StoreContext _context;

        public EfCategoryRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories;

        public IQueryable<SubCategory> SubCategories => _context.SubCategories;

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void CreateSubCategory(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            _context.SaveChanges();
        }
    }
}
