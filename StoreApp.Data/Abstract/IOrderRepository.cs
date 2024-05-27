using StoreApp.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Abstract
{
    public interface IOrderRepository
    {
        IQueryable<OrderItem> Orders { get; }

        void CreateOrder(OrderItem item);
    }
}
