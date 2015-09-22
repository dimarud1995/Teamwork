using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<Order> Orders
        {
            get { return _db.Order; }
        }

        public void SaveOrder(Order order)
        {
            if (order.orderID == 0)
            {
                _db.Order.Add(order);
            }
            else
            {
                var dbEntry = _db.Order.Find(order.orderID);
                if (dbEntry != null)
                {
                    dbEntry.orderDate = order.orderDate;
                    dbEntry.finishDate = order.finishDate;
                    dbEntry.productID = order.productID;
                    dbEntry.userID = order.userID;
                    dbEntry.amount = order.amount;
                }
            }
            _db.SaveChanges();
        }

        public Order DeleteOrder(int id)
        {
            var dbEntry = _db.Order.Find(id);
            if (dbEntry != null)
            {
                _db.Order.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}