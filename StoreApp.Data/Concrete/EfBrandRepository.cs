using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete
{
    public class EfBrandRepository : IBrandRepository
    {
        private readonly StoreContext _context;

        public EfBrandRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<Brand> Brands => _context.Brands;

        public async void CreateBrand(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            _context.SaveChanges();
        }
    }
}
