using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EfOrderRepository : IOrderRepository
    {
        StoreContext _context;

        public EfOrderRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<OrderItem> Orders => _context.Orders;

        public async void CreateOrder(OrderItem item)
        {
            await _context.Orders.AddAsync(item);
            _context.SaveChanges();
        }
    }
}
