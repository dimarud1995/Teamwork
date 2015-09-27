using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<ShowedProduct> ShowedProducts
        {
            get { return _db.ShowedProducts; }
        }

        public void SaveShowedProduct(ShowedProduct showedProduct)
        {
            if (showedProduct.showedProductID == 0)
            {
                _db.ShowedProducts.Add(showedProduct);
            }
            else
            {
                var dbEntry = _db.ShowedProducts.Find(showedProduct.showedProductID);
                if (dbEntry != null)
                {
                    dbEntry.productID = showedProduct.productID;
                    dbEntry.userID = showedProduct.userID;
                }
            }
            _db.SaveChanges();
        }

        public ShowedProduct DeleteShowedProduct(int id)
        {
            var dbEntry = _db.ShowedProducts.Find(id);
            if (dbEntry != null)
            {
                _db.ShowedProducts.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}