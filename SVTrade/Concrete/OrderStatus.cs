using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<OrderStatus> OrderStatuses
        {
            get { return _db.OrderStatuses; }
        }

        public void SaveOrderStatus(OrderStatus orderStatus)
        {
            if (orderStatus.statusID == 0)
            {
                _db.OrderStatuses.Add(orderStatus);
            }
            else
            {
                var dbEntry = _db.OrderStatuses.Find(orderStatus.statusID);
                if (dbEntry != null)
                {
                    dbEntry.name = orderStatus.name;
                }
            }
            _db.SaveChanges();
        }

        public OrderStatus DeleteOrderStatus(int id)
        {
            var dbEntry = _db.OrderStatuses.Find(id);
            if (dbEntry != null)
            {
                _db.OrderStatuses.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}