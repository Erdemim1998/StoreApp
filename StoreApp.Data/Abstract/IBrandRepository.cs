using StoreApp.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Abstract
{
    public interface IBrandRepository
    {
        IQueryable<Brand> Brands { get; }

        void CreateBrand(Brand brand);
    }
}
