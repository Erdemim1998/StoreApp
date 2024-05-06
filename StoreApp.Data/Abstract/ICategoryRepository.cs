using StoreApp.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        IQueryable<SubCategory> SubCategories { get; }

        void CreateCategory(Category category);

        void CreateSubCategory(SubCategory subCategory);
    }
}
