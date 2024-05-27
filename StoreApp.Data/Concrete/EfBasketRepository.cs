using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EfBasketRepository : IBasketRepository
    {
        private readonly StoreContext _context;

        public EfBasketRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<BasketItem> Baskets => _context.Baskets;

        public async void CreateBasket(BasketItem item)
        {
            await _context.Baskets.AddAsync(item);
            _context.SaveChanges();
        }
    }
}
