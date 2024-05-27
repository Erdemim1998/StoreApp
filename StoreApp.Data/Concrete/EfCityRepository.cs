using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EfCityRepository : ICityRepository
    {
        StoreContext _context;

        public EfCityRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<City> Cities => _context.Cities;

        public async void CreateCity(City city)
        {
            await _context.AddAsync(city);
            _context.SaveChanges();
        }
    }
}
