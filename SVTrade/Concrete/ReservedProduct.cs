using System;
using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<ReservedProduct> ReservedProducts
        {
            get { return _db.ReservedProducts; }
        }

        public void SaveReservedProduct(ReservedProduct reservedProduct)
        {
            if (reservedProduct.reservID == 0)
            {
                _db.ReservedProducts.Add(reservedProduct);
            }
            else
            {
                var dbEntry = _db.ReservedProducts.Find(reservedProduct.reservID);
                if (dbEntry != null)
                {
                    dbEntry.userID = reservedProduct.userID;
                    dbEntry.date = reservedProduct.date;
                    dbEntry.orderID = reservedProduct.orderID;
                    dbEntry.productID = reservedProduct.productID;
                    dbEntry.amountOfProduct = reservedProduct.amountOfProduct;
                }
            }
            _db.SaveChanges();
        }

        public ReservedProduct DeleteReservedProduct(int id)
        {
            var dbEntry = _db.ReservedProducts.Find(id);
            if (dbEntry != null)
            {
                _db.ReservedProducts.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}